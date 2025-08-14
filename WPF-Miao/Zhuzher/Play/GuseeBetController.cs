using HttpProcessor.Client;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Zhuzher.search;
using Zhuzher.session;

namespace Zhuzher.Play
{
    internal class GuseeBetController : HttpClientBase
    {
        public GuseeBetController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void GuessBetAsync(GuessBet guessBet)
        {
            Task.Factory.StartNew(() =>
            {
                foreach (var user in MainSession.UserProjectList.UserProjects)
                {
                    Thread.Sleep(500);
                    Task.Factory.StartNew(() => GuessBet(user, guessBet));
                }
            });

        }

        public bool GuessBet(UserProject user, GuessBet GuessBet)
        {
            try
            {
                var content = new GuessBetContent(user, GuessBet);
                content.BuildDefaultHeaders(Client);
                var response = PostStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]GuessBet - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("code").GetUInt32();
                var msg = root.GetProperty("message").GetString();
                if (code != 200)
                {
                    MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]GuessBet失败: code={code}, message={msg}");
                    return false;
                }

                var result = root.GetProperty("result");
                SaveBatResult(user, result);
                return true;
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]GuessBet失败 - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }

        private void SaveBatResult(UserProject user, JsonElement result)
        {
            MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}] - GuessBet");
        }
    }
}
