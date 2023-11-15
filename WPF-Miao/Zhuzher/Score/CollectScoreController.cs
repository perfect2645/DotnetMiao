using HttpProcessor.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Zhuzher.search;
using Zhuzher.session;

namespace Zhuzher.Score
{
    internal class CollectScoreController : HttpClientBase
    {
        public CollectScoreController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void CollectScoreAsync()
        {
            foreach (var user in MainSession.UserProjectList.UserProjects)
            {
                Task.Factory.StartNew(() => CollectScore(user));
            }
        }

        public bool CollectScore(UserProject user)
        {
            try
            {
                var content = new CollectScoreContent(user);
                content.BuildDefaultHeaders(Client);
                var response = PostStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"CollectScore - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("code").GetUInt32();
                var msg = root.GetProperty("message").GetString();
                if (code != 200)
                {
                    MainSession.PrintLogEvent.Publish(this, $"CollectScore失败: code={code}, message={msg}");
                    return false;
                }

                var score = root.GetProperty("result").GetInt32();

                MainSession.PrintLogEvent.Publish(this, $"收集积分成功[{user.UserName}] - 还有积分:{score}");

                return true;
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到苗 - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }
    }
}
