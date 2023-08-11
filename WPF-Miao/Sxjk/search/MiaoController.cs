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
            var scheduleList = JsonAnalysis.JsonToDicList("work_dateStr");
            if (!scheduleList.HasItem()) 
            {
                MainSession.PrintLogEvent.Publish(this, $"work_date is empty 没有库存了");
                return false;
            }

            var orderList = new List<Order>();

            foreach(var schedule in scheduleList)
            {
                var order = BuildOrder(schedule, date);
                order.Vaccine_code = vaccine_code;
                orderList.Add(order);
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

        private Order BuildOrder(Dictionary<string, object> schedule, string date)
        {
            var hosId = MainSession.PlatformSession.GetString(Constants.HospitalId);
            var hosName = MainSession.PlatformSession.GetString(Constants.HospitalName);
            var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);

            var dateLoacal = DateTimeUtil.GetDateTime(date, "D");

            var startTime = schedule.GetString("start_time");
            var endTime = schedule.GetString("end_time");
            var time = $"{dateLoacal} {startTime}-{endTime}";


            var order = new Order
            {

            };

            return order;
        }
    }
}
