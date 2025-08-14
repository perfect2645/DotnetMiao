using HttpProcessor.Client;
using HttpProcessor.Request;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Utils;
using Zhuzher.Common;
using Zhuzher.Exchange;
using Zhuzher.search;
using Zhuzher.session;

namespace Zhuzher.Post
{
    internal class LikeController : HttpClientBase
    {
        private ApiVersion _version = ApiVersion.V1;
        private string _postId = "";
        public LikeController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void LikeAsync(string postId, ApiVersion version)
        {
            _version = version;
            _postId = postId;
            Task.Factory.StartNew(() =>
            {
                foreach (var user in MainSession.UserProjectList.UserProjects)
                {
                    Thread.Sleep(500);
                    Task.Factory.StartNew(() =>
                    {
                        if (version == ApiVersion.V1)
                        {
                            Like(user, postId);
                        }
                        else if (version == ApiVersion.V2)
                        {
                            LikeV2(user, postId);
                        }
                    });
                }
            });
        }

        public void LikeAsync(string postId, UserProject user, ApiVersion version)
        {
            _version = version;
            _postId = postId;
            Task.Factory.StartNew(() =>
            {
                if (version == ApiVersion.V1)
                {
                    Like(user, postId);
                }
                else if (version == ApiVersion.V2)
                {
                    LikeV2(user, postId);
                }
            });
        }

        public void Like(UserProject user, string postId)
        {
            try
            {
                var content = new LikeContent(user, postId);
                content.BuildDefaultHeaders(Client);
                var response = Client.PutJsonAsync(content).Result;
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
                SummarizeLikeInfo(result, user);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]点赞失败 - {ex.Message} - {ex.StackTrace}");
            }
        }
        public void LikeV2(UserProject user, string postId)
        {
            try
            {
                var content = new LikeV2Content(user, postId.ToInt());
                content.BuildDefaultHeaders(Client);
                var response = Client.PostJsonAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]点赞失败 - {response?.Message},请检查参数");
                    return;
                }
                var root = response.JsonBody.RootElement;
                var code = root.GetProperty("code").GetUInt32();

                if (code != 200)
                {
                    var msg = root.GetProperty("message").GetString();
                    MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]点赞失败: code={code}, message={msg}");
                    return;
                }

                var result = root.GetProperty("result");
                SummarizeLikeInfoV2(result, user);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]点赞失败 - {ex.Message} - {ex.StackTrace}");
            }
        }

        private void SummarizeLikeInfo(JsonElement result, UserProject user)
        {
            var postId = result.GetProperty("post_id").GetInt32();
            var upCount = result.GetProperty("up_count").GetInt16();
            MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]点赞成功, postId={postId}, upCount={upCount}");
        }

        private void SummarizeLikeInfoV2(JsonElement result, UserProject user)
        {
            var isSuccess = result.GetBoolean();
            MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]点赞, postId={_postId}, isSuccess={isSuccess}");
        }
    }
}