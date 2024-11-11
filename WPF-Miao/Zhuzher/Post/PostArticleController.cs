using HttpProcessor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Zhuzher.Play;
using Zhuzher.search;
using Zhuzher.session;

namespace Zhuzher.Post
{
    internal class PostArticleController : HttpClientBase
    {
        public PostArticleController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void GetArticleAsync(UserProject user, int articleId)
        {
            Task.Factory.StartNew(() => GetArticle(user, articleId));
        }

        public void GetArticle(UserProject user, int articleId)
        {
            try
            {
                var content = new PostArticleContent(user, articleId);
                content.BuildDefaultHeaders(Client);
                var response = GetStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]FragmentExchange - {response?.Message},请检查参数");
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("code").GetUInt32();
                var msg = root.GetProperty("message").GetString();
                if (code != 200)
                {
                    MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]FragmentExchange失败: code={code}, message={msg}");
                }

                var result = root.GetProperty("result");
                SaveArticleResult(user, result);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}]FragmentExchange失败 - {ex.Message} - {ex.StackTrace}");
            }
        }

        private void SaveArticleResult(UserProject user, JsonElement result)
        {
        }
    }
}
