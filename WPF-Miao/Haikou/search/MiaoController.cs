using Haikou.appointment;
using Haikou.login;
using Haikou.session;
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

namespace Haikou.search
{
    internal class MiaoController : HttpClientBase
    {

        public MiaoController(HttpClient httpClient) : base(httpClient)
        {
        }

        public bool SearchMiao(List<string> dateList)
        {
            try
            {
                var randomDate = dateList.DisorderItems().FirstOrDefault();
                var user = MainSession.Users.FirstOrDefault();
                var content = new MiaoContent(user, randomDate);
                content.BuildDefaultHeaders(Client);
                var response = PostStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetUser - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("code").GetInt32();
                var msg = root.GetProperty("msg").GetString();
                if (code != 0 || msg != "success")
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetUser失败:{user.UserName} code={code}, msg={msg}");
                    return false;
                }

                var list = root.GetProperty("list");
                if (list.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchMiao失败: list is empty");
                    return false;
                }

                return CheckSaveResource(list);
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

            var availableSchedule = scheduleList.Where(s => s.GetString("num").ToInt() > 0).ToList();

            return BuildOrderList(scheduleList);
        }

        private bool BuildOrderList(List<Dictionary<string, object>> scheduleList)
        {
            var orderList = new List<Order>();

            foreach (var vaccineDay in scheduleList)
            {
                var timeListStr = vaccineDay.GetString("list");
                var timeList = timeListStr.ToObjDicList();
                var availableTimeList = timeList.Where(t => t.GetString("appointmentState") == "0"
                && t.GetString("state") == "0").ToList();
                if (!availableTimeList.HasItem())
                {
                    continue;
                }

                var appId = MainSession.PlatformSession.GetString(Constants.Appid);
                var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);
                var deptName = MainSession.PlatformSession.GetString(Constants.DeptName);
                var serviceCode = MainSession.PlatformSession.GetString(Constants.ServiceCode);
                var areaCode = MainSession.PlatformSession.GetString(Constants.HospitalId);

                foreach (var timeItem in availableTimeList)
                {
                    var uuid = timeItem.GetString("uuid");
                    var date = timeItem.GetString("serviceDate");
                    var time = timeItem.GetString("sessionName");
                    var order = new Order
                    {
                        Appid = appId,
                        DateTime = $"{date} - {time}",
                        DeptName = deptName,
                        ScheduleId = uuid,
                    };
                    orderList.Add(order);
                }
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
