using Sxjk.common;
using Sxjk.login;
using Sxjk.session;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Utils;
using Utils.json;

namespace Sxjk.appointment
{
    internal class OrderHistoryController : SxjkController
    {
        public OrderHistoryController(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task GetOrderHistoryAsync(SxjkLogin user)
        {
            return Task.Factory.StartNew(() => GetOrderHistory(user));
        }

        private void GetOrderHistory(SxjkLogin user)
        {
            try
            {
                var content = new OrderHistoryContent(user);
                content.BuildDefaultHeaders(Client);
                var response = GetStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetOrderHistory - {response?.Message},请检查参数");
                    return;
                }
                var root = response.JsonBody.RootElement;

                var resultDic = CheckResult(root);
                if (!resultDic.HasItem())
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetOrderHistory失败 - {response?.Message}");
                    return;
                }

                var success = resultDic.GetString("success");
                if ("False".Equals(success, StringComparison.OrdinalIgnoreCase))
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetOrderHistory失败 - {resultDic.GetString("message")}");
                    return;
                }

                var data = resultDic.GetString("data");

                SaveUser(data);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"GetOrderHistory异常{ex.Message}");
            }
        }

        private void SaveUser(string data)
        {
            var orderList = JsonAnalysis.JsonToDicList(data);
            if (!orderList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"获取GetOrderHistory失败");
                return;
            }

            MainSession.PrintLogEvent.Publish(this, orderList);
        }
    }
}
