using Base.viewmodel.status;
using Sanya.appointment;
using Sanya.session;
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
using Utils.datetime;

namespace Sanya.search
{
    internal class MiaoController : HttpClientBase
    {
        public MiaoController(HttpClient httpClient) : base(httpClient)
        {
        }

        public bool SearchMiao()
        {
            try
            {
                var defaultUser = MainSession.Users.FirstOrDefault();
                var content = new MiaoContent(defaultUser);
                content.BuildDefaultHeaders(Client);
                var response = GetStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchVaccine - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("code").GetInt32();
                var message = root.GetProperty("message").GetString();
                if (code != 0)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchVaccine失败: code={code}, message = {message}");
                    return false;
                }

                var data = root.GetProperty("data");
                if (data.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchVaccine失败: results is empty");
                    return false;
                }

                return CheckSaveSchedule(data);
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
                MainSession.PrintLogEvent.Publish(this, $"没有库存了");
                return false;
            }

            var orderList = new List<Order>();

            foreach(var schedule in scheduleList)
            {
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
