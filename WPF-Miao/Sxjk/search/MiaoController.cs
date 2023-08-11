using Base.viewmodel.status;
using Sxjk.appointment;
using Sxjk.common;
using Sxjk.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Utils;
using Utils.datetime;
using Utils.json;
using Utils.number;
using Utils.stringBuilder;

namespace Sxjk.search
{
    internal class MiaoController : SxjkController
    {
        public MiaoController(HttpClient httpClient) : base(httpClient)
        {
        }

        public bool SearchMiao(string date)
        {
            try
            {
                var defaultUser = MainSession.Users.FirstOrDefault();
                var content = new MiaoContent(defaultUser, date);
                content.BuildDefaultHeaders(Client);
                var response = GetStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchMiao - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                var resultDic = CheckResult(root);
                if (!resultDic.HasItem())
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchMiao失败 - {response?.Message}");
                    return false;
                }

                var success = resultDic.GetString("success");
                if ("False".Equals(success, StringComparison.OrdinalIgnoreCase))
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchMiao失败 - {resultDic.GetString("message")}");
                    return false;
                }

                var data = resultDic.GetString("data");

                return CheckSaveSchedule(data, date);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到苗 - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }

        private bool CheckSaveSchedule(string scheduleListData, string date)
        {
            var vaccineData = JsonAnalysis.JsonToDic(scheduleListData);
            if (!vaccineData.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"没有库存了");
                return false;
            }

            var vaccine_code = vaccineData.GetString("vaccine_code");
            var work_dateStr = vaccineData.GetString("work_date");
            var scheduleList = JsonAnalysis.JsonToDicList(work_dateStr);
            if (!scheduleList.HasItem()) 
            {
                MainSession.PrintLogEvent.Publish(this, $"work_date is empty 没有库存了");
                return false;
            }

            var orderList = new List<Order>();

            foreach(var schedule in scheduleList)
            {
                var orders = BuildOrders(schedule, vaccine_code);
                orderList.AddRange(orders);
            }
            
            if (!orderList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"没有可用苗");
                return false;
            }

            MainSession.SetStatus(MiaoProgress.MiaoDetailGet);

            orderList = orderList.DisorderItems();

            var orderArgs = new OrderEventArgs
            {
                OrderList = orderList,
            };

            MainSession.OrderEvent.Publish(this, orderArgs);

            return true;
        }

        private List<Order> BuildOrders(Dictionary<string, object> schedule, string vaccine_code)
        {
            var date = schedule.GetString("date_day");
            var timeListStr = schedule.GetString("work_time_list");
            var timeList = timeListStr.ToObjDicList();

            var citiCode = MainSession.PlatformSession.GetString(Constants.CityCode);
            var stationCode = MainSession.PlatformSession.GetString(Constants.StationCode);
            var priceId = MainSession.PlatformSession.GetString(Constants.PriceId);

            var orders = new List<Order>();

            foreach (var timeItem in timeList)
            {
                var orderTemplate = new Order
                {
                    City_code = citiCode,
                    Price_id = priceId,
                    Reservation_begin_time = timeItem.GetString("work_begin_time"),
                    Reservation_date = date,
                    Reservation_end_time = timeItem.GetString("work_end_time"),
                    Station_code = stationCode,
                    Vaccine_code = vaccine_code
                };

                orders.Add(orderTemplate);
            }

            return orders;
        }
    }
}
