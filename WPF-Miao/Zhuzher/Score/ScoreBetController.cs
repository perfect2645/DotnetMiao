using HttpProcessor.Client;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Zhuzher.search;
using Zhuzher.session;


namespace Zhuzher.Score
{
    internal class ScoreBetController : HttpClientBase
    {
        public ScoreBetController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void ScoreBetAsync()
        {
            var scoreBetList = new ScoreBetList();
            Task.Factory.StartNew(() =>
            {
                foreach (var user in MainSession.UserProjectList.UserProjects)
                {
                    foreach(var scorebet in scoreBetList.ScoreBets)
                    {
                        Thread.Sleep(500);
                        Task.Factory.StartNew(() => ScoreBet(user, scorebet));
                    }
                }
            });

        }

        public bool ScoreBet(UserProject user, ScoreBet scoreBet)
        {
            try
            {
                var content = new ScoreBetContent(user, scoreBet);
                content.BuildDefaultHeaders(Client);
                var response = PostStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]CollectScore - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("code").GetUInt32();
                var msg = root.GetProperty("message").GetString();
                if (code != 200)
                {
                    MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]CollectScore失败: code={code}, message={msg}");
                    return false;
                }

                var score = root.GetProperty("result").GetRawText();

                MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]收集积分成功 - 还有积分:{score}");

                return true;
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]CollectScore失败 - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }
    }
}