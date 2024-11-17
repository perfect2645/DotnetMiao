using Base.Events;
using HttpProcessor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Utils;
using Utils.json;
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
            var recordsElement = result.GetProperty("records");
            var recordsDic = JsonAnalysis.JsonToDicList(recordsElement);

            var articleList = new List<Article>();

            Task.Factory.StartNew(() =>
            {
                foreach (var record in recordsDic)
                {
                    var article = BuildNewArticleAsync(user, record);
                    articleList.Add(article.Result);
                }
            });

            if (!articleList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"{user.UserName} not article found");
            }

            var uiEventArgs = new UiEventArgs();
            uiEventArgs.Field = "articles";
            uiEventArgs.Value = articleList;

            MainSession.UpdateUiEvent.Publish(this, uiEventArgs);
            MainSession.PrintLogEvent.Publish(this, $"{user.UserName} articleList publicshed count={articleList.Count}");
        }

        private async Task<Article> BuildNewArticleAsync(UserProject user, Dictionary<string, object> record)
        {
            var activityId = record.GetInt("activityId");
            var articleId = record.GetInt("id");
            var title = record.GetString("title");
            var nickName = record.GetString("nickname");
            var coverUrl = record.GetString("coverUrl");

            var article = new Article
            {
                Id = activityId,
                Title = title,
                ActivityId = activityId,
                User = user,
                NickName = nickName,
                CoverUrl = coverUrl
            };

            await article.BuildImgAsync();

            return article;
        }
    }
}
