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
        public string Date { get; private set; }

        public MiaoController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void SearchMiaoAsync(Order scheduleOrder)
        {
            Task.Factory.StartNew(() => SearchMiao(scheduleOrder));
        }

        public bool SearchMiao(Order scheduleOrder)
        {
            try
            {
                var content = new MiaoContent(defaultUser);
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
                var scheduleList = data.GetProperty("scheduleList");


                return CheckSaveSchedule(scheduleList);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到苗 - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }

        private bool CheckSaveSchedule(JsonElement scheduleListData)
        {
            var scheduleList = JsonAnalysis.JsonToDicList(scheduleListData);
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

        private Order BuildOneOrder(Dictionary<string, object> resource)
        {
            var hospitalId = MainSession.PlatformSession.GetString(Constants.HospitalId);
            var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);
            var docId = MainSession.PlatformSession.GetString(Constants.DoctorId);

            var scheduleDate = resource.GetString("scheduleDate");

            return new Order
            {
                DeptId = deptId,
                DoctorId = docId,
                HisId = hospitalId,
                PlatformId = hospitalId,
                PlatformSource = "3",
                ScheduleDate = scheduleDate,
                SubSource = "1",
            };
        }
    }
}
