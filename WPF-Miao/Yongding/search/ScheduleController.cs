using Base.model;
using HttpProcessor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;
using Yongding.appointment;
using Yongding.session;

namespace Yongding.search
{
    internal class ScheduleController : HttpClientBase
    {
        public DspVal Date { get; private set; }

        public ScheduleController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void SearchScheduleAsync(DspVal date)
        {
            Task.Factory.StartNew(() => SearchSchedule(date));
        }

        public bool SearchSchedule(DspVal date)
        {
            Date = date;
            try
            {
                var defaultUser = MainSession.Users.FirstOrDefault();
                var content = new ScheduleContent(defaultUser, Date);
                content.BuildDefaultHeaders(Client);
                var response = GetStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchSchedule - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("code").GetInt32();
                var message = root.GetProperty("message").GetString();
                if (code != 200)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchSchedule失败: code={code}, message={message}");
                    return false;
                }

                var data = root.GetProperty("data");
                if (data.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchSchedule失败: data is empty");
                    return false;
                }

                return CheckSaveResource(data);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到苗 - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }

        private bool CheckSaveResource(JsonElement dataElement)
        {
            var scheduleList = JsonAnalysis.JsonToDicList(dataElement);
            if (!scheduleList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"获取Miao信息失败");
                return false;
            }

            var doctorName = MainSession.PlatformSession.GetString(Constants.DoctorName);
            var targetSchedule = scheduleList.FirstOrDefault(x => x.GetString("title").Contains(doctorName));
            if (targetSchedule == null)
            {
                MainSession.PrintLogEvent.Publish(this, $"没有匹配的科室-{doctorName}，自动匹配9");
                targetSchedule = scheduleList.FirstOrDefault(x => x.GetString("title").Contains("九") || x.GetString("title").Contains("9"));
            }

            if (targetSchedule == null)
            {
                MainSession.PrintLogEvent.Publish(this, $"没有匹配的科室-{doctorName}");
                return false;
            }

            return BuildOrder(targetSchedule);
        }

        private bool BuildOrder(Dictionary<string, object> targetSchedule)
        {
            var orderList = new List<Order>();

            var scheduleOrder = new Order
            {
                Id = targetSchedule.GetString("list_id"),
                Riqi = Date.Display,
                Time = targetSchedule.GetString("t_val"),
                Timeid = targetSchedule.GetString("id"),
            };

            orderList.Add(scheduleOrder);

            var orderArgs = new OrderEventArgs
            {
                OrderList = orderList,
            };

            MainSession.OrderEvent.Publish(this, orderArgs);

            return true;
        }
    }
}
