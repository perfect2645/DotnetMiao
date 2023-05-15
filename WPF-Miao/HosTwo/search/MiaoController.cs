using HosTwo.appointment;
using HosTwo.session;
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

namespace HosTwo.search
{
    internal class MiaoController : HttpClientBase
    {
        public Order ScheduleOrder { get; private set; }

        public MiaoController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void SearchMiaoAsync(Order scheduleOrder)
        {
            ScheduleOrder = scheduleOrder;
            Task.Factory.StartNew(() => SearchMiao(scheduleOrder));
        }

        public bool SearchMiao(Order scheduleOrder)
        {
            try
            {
                var defaultUser = MainSession.Users.FirstOrDefault();
                var content = new MiaoContent(defaultUser, scheduleOrder);
                content.BuildDefaultHeaders(Client);
                var response = PostStringAsync(content, HttpProcessor.Content.ContentType.String).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchMiao - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("code").GetInt16();
                if (code != 0)
                {
                    MainSession.PrintLogEvent.Publish(this, $"查苗失败: code={code}");
                    return false;
                }

                var data = root.GetProperty("data");
                if (data.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"查苗失败: results is empty");
                    return false;
                }
                var itemList = data.GetProperty("itemList");

                return CheckSaveSchedule(itemList);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到苗 - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }

        private bool CheckSaveSchedule(JsonElement itemListData)
        {
            var scheduleList = JsonAnalysis.JsonToDicList(itemListData);
            if (!scheduleList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"获取Miao信息失败");
                return false;
            }

            var availableSchedules = scheduleList.Where(r => r.GetString("status") == "1").ToList();
            if (!availableSchedules.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"没有可用苗");
                return false;
            }

            BuildOrderList(availableSchedules);

            return true;
        }

        private void BuildOrderList(List<Dictionary<string, object>> schedules)
        {
            var orderList = new List<Order>();

            foreach (var schedule in schedules)
            {
                Order orderWithTime = BuildOneOrder(schedule);
                orderList.Add(orderWithTime);
            }
            orderList = orderList.DisorderItems();

            var orderArgs = new OrderEventArgs
            {
                OrderList = orderList,
            };

            MainSession.OrderEvent.Publish(this, orderArgs);
        }

        private Order BuildOneOrder(Dictionary<string, object> schedule)
        {
            var beginTime = schedule.GetString("visitBeginTime");
            var endTime = schedule.GetString("visitEndTime");

            return new Order
            {
                DeptId = ScheduleOrder.DeptId,
                DoctorId = ScheduleOrder.DoctorId,
                HisId = ScheduleOrder.HisId,
                PlatformId = ScheduleOrder.PlatformId,
                PlatformSource = ScheduleOrder.PlatformSource,
                ScheduleDate = ScheduleOrder.ScheduleDate,
                SubSource = ScheduleOrder.SubSource,
                SearchMonth = ScheduleOrder.SearchMonth,
                VisitBeginTime = beginTime,
                VisitEndTime = endTime,
                VisitPeriod = schedule.GetString("visitPeriod")
            };
        }
    }
}
