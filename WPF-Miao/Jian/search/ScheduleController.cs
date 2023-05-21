using Jian.appointment;
using Jian.session;
using HttpProcessor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;
using Utils.number;
using Utils.stringBuilder;
using Utils.datetime;

namespace Jian.search
{
    internal class ScheduleController : HttpClientBase
    {
        public string Date { get; private set; }

        public ScheduleController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void SearchScheduleAsync(string date)
        {
            Date = date;
            Task.Factory.StartNew(() => SearchSchedule(date));
        }

        public (bool, List<Order>) SearchSchedule(string date)
        {
            Date = date;
            try
            {
                var defaultUser = MainSession.Users.FirstOrDefault();
                var content = new ScheduleContent(defaultUser, date);
                content.BuildDefaultHeaders(Client);
                var response = GetStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchSchedule - {response?.Message},请检查参数");
                    return (false, new List<Order>());
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("resultCode").GetInt16();
                var success = root.GetProperty("success").GetBoolean();
                if (code != 20000 || !success)
                {
                    MainSession.PrintLogEvent.Publish(this, $"查苗失败: code={code}");
                    return (false, new List<Order>());
                }

                var data = root.GetProperty("data");
                if (data.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"查苗失败: results is empty");
                    return (false, new List<Order>());
                }

                return CheckSaveSchedule(data);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到苗 - {ex.Message} - {ex.StackTrace}");
                return (false, new List<Order>());
            }
        }

        private (bool, List<Order>) CheckSaveSchedule(JsonElement scheduleListData)
        {
            var scheduleList = JsonAnalysis.JsonToDicList(scheduleListData);
            if (!scheduleList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"获取Miao信息失败");
                return (false, new List<Order>());
            }

            var docId = MainSession.PlatformSession.GetString(Constants.DoctorId);
            var availableSchedules = scheduleList.Where(r => r.GetString("IsAvailable") == "1"
                && r.GetString("DocCode") == docId).ToList();

            if (!availableSchedules.HasItem())
            {
                availableSchedules = scheduleList.Where(r => r.GetString("IsAvailable") == "1").ToList();
            }

            if (!availableSchedules.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"没有可用苗");
                return (false, new List<Order>());
            }

            var orderList = BuildOrderList(availableSchedules);

            return (true, orderList);
        }

        private List<Order> BuildOrderList(List<Dictionary<string, object>> schedules)
        {
            var orderList = new List<Order>();

            foreach (var schedule in schedules)
            {
                var scheduleOrders = BuildOrdersFromSchedule(schedule);
                if (!scheduleOrders.HasItem())
                {
                    continue;
                }
                orderList.AddRange(scheduleOrders);
            }
            orderList = orderList.DisorderItems();

            var orderArgs = new OrderEventArgs
            {
                OrderList = orderList,
            };

            MainSession.OrderEvent.Publish(this, orderArgs);

            return orderList;
        }

        private List<Order> BuildOrdersFromSchedule(Dictionary<string, object> schedule)
        {
            var orders = new List<Order>();

            var scheduleInfos = schedule.GetString("SchedualInfos");
            var scheduleInfoList = scheduleInfos.ToObjDicList();
            if (!scheduleInfoList.HasItem())
            {
                return orders;
            }

            var amount = schedule.GetString("Amount");
            var dept2 = MainSession.PlatformSession.GetString(Constants.DeptId);
            var docId = MainSession.PlatformSession.GetString(Constants.DoctorId);

            var targetDay = schedule.GetString("Day");
            var dateRange = DateTimeUtil.GetDateRange(targetDay, DateTime.Today.ToString());
            var extCol = (dateRange.Count - 1).ToString();

            foreach (var scheduleInfo in scheduleInfoList)
            {
                var startTime = scheduleInfo.GetString("TimeStart");
                var endTime = scheduleInfo.GetString("TimeEnd");
                var seeADoctorTime = $"{startTime}--{endTime}";

                var order = new Order
                {
                    Amount = amount,
                    Dept2Code = dept2,
                    DoctorId = docId,
                    ExtCol = extCol,
                    ReservationTime = targetDay,
                    SeeADoctorTime = seeADoctorTime,
                };

                orders.Add(order);
            }

            return orders;
        }
    }
}
