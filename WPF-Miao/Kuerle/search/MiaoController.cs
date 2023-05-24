using HttpProcessor.Client;
using HttpProcessor.Container;
using Kuerle.appointment;
using Kuerle.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;

namespace Kuerle.search
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
                var response = PostStringAsync(content, HttpProcessor.Content.ContentType.Json).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"SearchMiao - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                return CheckBuildOrder(root);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到苗 - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }

        private bool CheckBuildOrder(JsonElement dataElement)
        {
            var miaoList = JsonAnalysis.JsonToDicList(dataElement);
            if (!miaoList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"获取Miao信息失败");
                return false;
            }

            var deptName = MainSession.PlatformSession.GetString(Constants.DeptName);
            var targetMiao = miaoList.FirstOrDefault(x => x.GetString("label").Contains(deptName));
            if (targetMiao == null)
            {
                MainSession.PrintLogEvent.Publish(this, $"查到苗信息，但是疫苗名称没有对上{deptName}");
                return false;
            }

            MainSession.PrintLogEvent.Publish(this, $"开始了");
            return BuildOrderList(targetMiao);
        }

        private bool BuildOrderList(Dictionary<string, object> miaoInfo)
        {
            var orderList = new List<Order>();

            var pid = miaoInfo.GetString("value");

            var vidController = HttpServiceController.GetService<VidController>();
            var vid = vidController.GetVid(pid);
            if (string.IsNullOrEmpty(vid))
            {
                return false;
            }

            orderList.Add(new Order
            {
                PID = pid,
                VID = vid,
                Date = "45071",
            });

            orderList.Add(new Order
            {
                PID = pid,
                VID = vid,
                Date = "45072",
            });

            var orderArgs = new OrderEventArgs
            {
                OrderList = orderList,
            };

            MainSession.OrderEvent.Publish(this, orderArgs);

            return true;
        }
    }
}
