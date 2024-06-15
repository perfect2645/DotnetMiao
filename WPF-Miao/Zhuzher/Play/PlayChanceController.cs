using HttpProcessor.Client;
using HttpProcessor.Container;
using HttpProcessor.Content;
using Microsoft.Windows.Themes;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Utils;
using Utils.datetime;
using Utils.json;
using Utils.number;
using Zhuzher.Play;
using Zhuzher.search;
using Zhuzher.session;

namespace Zhuzher.Play
{
    internal class PlayChanceController : HttpClientBase
    {
        #region Properties

        #endregion Properties
        public PlayChanceController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void GetChanceAsync()
        {
            Task.Factory.StartNew(() =>
            {
                foreach (var user in MainSession.UserProjectList.UserProjects)
                {
                    Thread.Sleep(500);
                    Task.Factory.StartNew(() =>
                    {
                        GetChance(user);
                    });
                }
            });
        }

        public int GetChance(UserProject user)
        {
            var content = new PlayChanceContent(user);
            content.BuildDefaultHeaders(Client);

            HttpDicResponse response = GetStringAsync(content).Result;
            if (response == null)
            {
                MainSession.PrintLogEvent.Publish(this, $"{user.UserName}登录过期了");
                return -1;
            }
            if (response.Body == null)
            {
                MainSession.PrintLogEvent.Publish(this, $"{user.UserName}登录过期了");
                return -1;
            }
            var code = response.Body.FirstOrDefault(x => x.Key == "code").Value?.ToString();
            var msg = response.Body.FirstOrDefault(x => x.Key == "message").Value?.ToString();
            if (code != "200" || msg != "success")
            {
                MainSession.PrintLogEvent.Publish(this, $"{user.UserName} - 获取金币失败 - {code}:{msg}");
            }
            var root = response.JsonBody.RootElement;
            var result = root.GetProperty("result");
            return GetChangeNumber(user, result);
        }

        private int GetChangeNumber(UserProject user, JsonElement result)
        {
            var chanceList = JsonAnalysis.JsonToDicList(result);
            foreach (var chance in chanceList)
            {
                var gameId = chance.GetString("activityGameId");
                var count = chance.GetString("chanceCount");
                MainSession.PrintLogEvent.Publish(null, $"[{user.UserName}]GameId[{gameId}]还有chance[{count}]");
            }
            return 0;
        }
    }
}
