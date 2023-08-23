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

        public string Date { get; private set; }

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
                var response = PostStringAsync(content, HttpProcessor.Content.ContentType.String).Result;
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

            return BuildOrderList(scheduleList);
        }

        private bool BuildOrderList(List<Dictionary<string, object>> vaccineDayList)
        {
            var orderList = new List<Order>();

            foreach (var vaccineDay in vaccineDayList)
            {
                var dayId = vaccineDay.GetString("id");
                var timeList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(vaccineDay.GetString("vaccineDayNum"));
                foreach (var timeItem in timeList)
                {
                    var forbidden = timeItem.GetString("forbidden");
                    if (forbidden.NotNullString() == "1")
                    {
                        continue;
                    }
                    var timeId = timeItem.GetString("id");
                    var orderWithTime = BuildOneOrder(dayId, timeId);
                    orderList.Add(orderWithTime);
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

        private Order BuildOneOrder(string dayId, string timeId)
        {
            var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);

            return new Order
            {
            };
        }
    }
}
