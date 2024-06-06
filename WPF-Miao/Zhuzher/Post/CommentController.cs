using HttpProcessor.Client;
using HttpProcessor.Request;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Zhuzher.Common;
using Zhuzher.search;
using Zhuzher.session;

namespace Zhuzher.Post
{
    internal class CommentController : HttpClientBase
    {
        private ApiVersion _version = ApiVersion.V1;
        public CommentController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void CommentAsync(string postId, string postContent, ApiVersion version)
        {
            _version = version;
            Task.Factory.StartNew(() =>
            {
                foreach (var user in MainSession.UserProjectList.UserProjects)
                {
                    Thread.Sleep(500);
                    Task.Factory.StartNew(() => 
                    {
                        if (version== ApiVersion.V1)
                        {
                            Comment(user, postId, postContent);
                        } 
                        else if (version == ApiVersion.V2)
                        {
                            CommentV2(user, postId, postContent);
                        }
                    });
                }
            });
        }

        public void Comment(UserProject user, string postId, string postContent)
        {
            try
            {
                var content = new CommentContent(user, postId, postContent);
                content.BuildDefaultHeaders(Client);
                var response = Client.PostJsonAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]评论失败 - {response?.Message},请检查参数");
                    return;
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("code").GetUInt32();

                if (code != 0)
                {
                    var msg = root.GetProperty("message").GetString();
                    MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]评论失败失败: code={code}, message={msg}");
                    return;
                }

                var result = root.GetProperty("result");
                SummarizeCommentInfo(result, user);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]评论失败失败 - {ex.Message} - {ex.StackTrace}");
            }
        }

        public void CommentV2(UserProject user, string postId, string postContent)
        {
            try
            {
                var content = new CommentV2Content(user, postId, postContent);
                content.BuildDefaultHeaders(Client);
                var response = Client.PostJsonAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]评论失败 - {response?.Message},请检查参数");
                    return;
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("code").GetUInt32();

                if (code != 200)
                {
                    var msg = root.GetProperty("message").GetString();
                    MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]评论失败失败: code={code}, message={msg}");
                    return;
                }

                var result = root.GetProperty("result");
                SummarizeCommentInfo(result, user);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]评论失败失败 - {ex.Message} - {ex.StackTrace}");
            }
        }

        private void SummarizeCommentInfo(JsonElement result, UserProject user)
        {
            MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]评论成功");
        }
    }
}

