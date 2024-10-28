using HttpProcessor.Client;
using HttpProcessor.JsonFactory;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Zhuzher.search;
using Zhuzher.session;

namespace Zhuzher.Play
{
    internal class PuliDrawPrizeController : HttpClientBase
    {
        public PuliDrawPrizeController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void GoodGetAsync()
        {
            Task.Factory.StartNew(() =>
            {
                foreach (var user in MainSession.UserProjectList.UserProjects)
                {
                    Thread.Sleep(500);
                    Task.Factory.StartNew(() => GoodGet(user));
                }
            });

        }

        public bool GoodGet(UserProject user)
        {
            try
            {
                var content = new PuliDrawPrizeContent(user);
                content.BuildDefaultHeaders(Client);
                var response = PostStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]GoodGet - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("code").GetUInt32();
                var msg = root.GetProperty("message").GetString();
                if (code != 200)
                {
                    MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]GoodGet失败: code={code}, message={msg}");
                    return false;
                }

                var result = root.GetProperty("result");
                SavePrizeResult(user, result);
                return true;
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]GoodGet失败 - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }

        private void SavePrizeResult(UserProject user, JsonElement result)
        {
            var message = result.GetStringValue("message");
            if (string.IsNullOrEmpty(message)) 
            {
                message = "开奖成功";
            }
            MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}] - GoodGet:{message}");
        }
    }
}
