using HttpProcessor.Client;
using SixWater.appointment;
using SixWater.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;

namespace SixWater.search
{
    internal class MiaoController : HttpClientBase
    {
        public string Date { get; private set; }

        public MiaoController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void SearchMiaoAsync(string date, string scheduleId)
        {
            Task.Factory.StartNew(() => SearchMiao(date, scheduleId));
        }

        public bool SearchMiao(string date, string scheduleId)
        {
            try
            {
                Date = date;
                var defaultUser = MainSession.Users.FirstOrDefault();
                var content = new MiaoContent(defaultUser, date, scheduleId);
                content.BuildDefaultHeaders(Client);
                var response = GetStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchMiao - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("code").GetInt32();
                var msg = root.GetProperty("message").GetString();
                if (code != 0)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchMiao: code={code}, message={msg}");
                    return false;
                }

                var data = root.GetProperty("data");
                if (data.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchMiao失败: data is empty");
                    return false;
                }

                return CheckSaveOrder(data);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到苗 - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }

        private bool CheckSaveOrder(JsonElement dataElement)
        {
            var scheduleList = JsonAnalysis.JsonToDicList(dataElement);
            if (!scheduleList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"获取Miao信息失败");
                return false;
            }

            return BuildOrderList(scheduleList);
        }

        private bool BuildOrderList(List<Dictionary<string, object>> scheduleList)
        {
            var orderList = new List<Order>();

            var deptName = MainSession.PlatformSession.GetString(Constants.DeptName);
            var targetSchedule = scheduleList.FirstOrDefault(s => s.GetString("deptName").Contains(deptName));
            if (targetSchedule == null)
            {
                targetSchedule = scheduleList.FirstOrDefault(s => s.GetString("deptName").Contains("九价"));
            }

            if (targetSchedule == null)
            {
                targetSchedule = scheduleList.LastOrDefault();
            }

            var hosId = MainSession.PlatformSession.GetString(Constants.HospitalId);

            var defaultOrder = new Order
            {
                BeginTime = targetSchedule.GetString("beginTime"),
                EndTime = targetSchedule.GetString("endTime"),
                DeptId = targetSchedule.GetString("deptId"),
                DoctorId = targetSchedule.GetString("doctorId"),
                DoctorScheduleId = targetSchedule.GetString("doctorScheduleId"),
                Emergency = targetSchedule.GetString("emergency"),
                OrgId = hosId,
                RegisterTypeId = targetSchedule.GetString("registerTypeId"),
                ScheduleDate = Date,
                TotalFee = targetSchedule.GetString("totalFee"),
            };

            orderList.Add(defaultOrder);

            var orderArgs = new OrderEventArgs
            {
                OrderList = orderList,
            };

            MainSession.OrderEvent.Publish(this, orderArgs);

            return true;
        }
    }
}
