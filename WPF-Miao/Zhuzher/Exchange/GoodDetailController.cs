using HttpProcessor.Client;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Zhuzher.search;
using Zhuzher.session;

namespace Zhuzher.Exchange
{
    internal class GoodDetailController : HttpClientBase
    {
        public GoodDetailController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void GoodAvailableAsync(UserProject user, MiaoshaItem good)
        {
            Task.Factory.StartNew(() =>
            {
                GoodAvailable(user, good);
            });

        }

        public bool GoodAvailable(UserProject user, MiaoshaItem good)
        {
            try
            {
                var content = new GoodDetailContent(user, good);
                content.BuildDefaultHeaders(Client);
                var response = PostStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]ScoreBet - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("code").GetUInt32();
                var msg = root.GetProperty("message").GetString();
                if (code != 200)
                {
                    MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]ScoreBet失败: code={code}, message={msg}");
                    return false;
                }

                var result = root.GetProperty("result");
                return CheckGoodResult(user, result);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]ScoreBet失败 - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }

        private bool CheckGoodResult(UserProject user, JsonElement result)
        {
            var goodName = result.GetProperty("goodName").GetString();
            var surplusCount = result.GetProperty("surplusCount").GetInt32();
            MainSession.PrintLogEvent.Publish(this, $"[{goodName}]状态 - surplusCount:{surplusCount}");
            if (surplusCount == 0)
            {
                return false;
            }

            return true;
        }
    }
}