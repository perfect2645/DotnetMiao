using HttpProcessor.Client;
using HttpProcessor.Request;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Zhuzher.search;
using Zhuzher.session;

namespace Zhuzher.Post
{
    internal class LikeController : HttpClientBase
    {
        public LikeController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void LikeAsync(string postId)
        {
            Task.Factory.StartNew(() =>
            {
                foreach (var user in MainSession.UserProjectList.UserProjects)
                {
                    Thread.Sleep(500);
                    Task.Factory.StartNew(() => Like(user, postId));
                }
            });
        }

        public void Like(UserProject user, string postId)
        {
            try
            {
                var content = new LikeContent(user, postId);
                content.BuildDefaultHeaders(Client);
                var response = Client.PostJsonAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]点赞失败 - {response?.Message},请检查参数");
                    return;
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("code").GetUInt32();

                if (code != 0)
                {
                    var msg = root.GetProperty("message").GetString();
                    MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]点赞失败: code={code}, message={msg}");
                    return;
                }

                var result = root.GetProperty("result");
                SummarizeScoreInfo(result, user);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]点赞失败 - {ex.Message} - {ex.StackTrace}");
            }
        }

        private void SummarizeScoreInfo(JsonElement result, UserProject user)
        {
            MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]点赞成功");
        }
    }
}