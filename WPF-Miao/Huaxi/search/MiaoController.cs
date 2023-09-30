using Huaxi.appointment;
using Huaxi.login;
using Huaxi.session;
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
using Newtonsoft.Json;
using Utils.stringBuilder;

namespace Huaxi.search
{
    internal class MiaoController : HttpClientBase
    {

        public string Date { get; private set; }

        public MiaoController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void SearchMiaoAsync()
        {
            Task.Factory.StartNew(() => SearchMiao());
        }

        public bool SearchMiao()
        {
            try
            {
                var defaultUser = MainSession.Users.FirstOrDefault();
                var content = new MiaoContent(defaultUser);
                content.BuildDefaultHeaders(Client);
                var response = PostStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetUser - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("code").GetString();
                var msg = root.GetProperty("msg").GetString();
                if (code != "1")
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchMiao失败: code={code}, msg={msg}");
                    return false;
                }

                var data = root.GetProperty("data");
                if (data.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchMiao失败: data is empty");
                    return false;
                }

                var vaccineDayList = data.GetProperty("scheduleRespVo").GetProperty("scheduleRespVos");

                return CheckSaveResource(vaccineDayList);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到苗 - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }

        private bool CheckSaveResource(JsonElement dataElement)
        {
            var vaccineDayList = JsonAnalysis.JsonToDicList(dataElement);
            if (!vaccineDayList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"获取Miao信息失败");
                return false;
            }

            var availableScheduleList = vaccineDayList.Where(x => x.GetString("statusName") == "有号").ToList();
            if (!availableScheduleList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"没有可用号源");
                return false;
            }

            return BuildOrderList(availableScheduleList);
        }

        private bool BuildOrderList(List<Dictionary<string, object>> vaccineDayList)
        {
            var orderList = new List<Order>();
            var organCode = MainSession.PlatformSession.GetString(Constants.HospitalId);

            foreach (var vaccineDay in vaccineDayList)
            {
                var sysScheduleId = vaccineDay.GetString("sysScheduleId");
                orderList.Add(new Order
                {
                    OrganCode = organCode,
                    ScheduleId = sysScheduleId
                });
            }

            if(!orderList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"没有可用苗");
                return false;
            }

            orderList = orderList.DisorderItems();

            var orderArgs = new OrderEventArgs
            {
                OrderList = orderList,
            };

            MainSession.OrderEvent.Publish(this, orderArgs);

            return true;
        }
    }
}
