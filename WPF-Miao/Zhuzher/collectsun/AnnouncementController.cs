using HttpProcessor.Client;
using System;
using System.Linq;
using System.Net.Http;
using System.Security.RightsManagement;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Utils;
using Utils.json;
using Utils.stringBuilder;
using Zhuzher.Post;
using Zhuzher.Score;
using Zhuzher.search;
using Zhuzher.session;

namespace Zhuzher.collectsun
{
    internal class AnnouncementController : HttpClientBase
    {
        private readonly UserProjectList UserProjectList = new UserProjectList();
        public AnnouncementController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void CollectSunAsync()
        {
            Task.Factory.StartNew(() =>
            {
                foreach (var user in UserProjectList.UserProjects)
                {
                    Task.Factory.StartNew(() => CollectSun(user));
                    Thread.Sleep(1000);
                }
            });
        }

        private void CollectSun(UserProject user)
        {
            var content = new AnnouncementContent(user);
            GetAnnouncement(content);
            if (string.IsNullOrEmpty(content.AnnouncementId))
            {
                return;
            }
            Thread.Sleep(1000);
            ReadAnnouncement(content);
            Thread.Sleep(1000);
            CommentAnnouncement(content);
        }

        private void GetAnnouncement(AnnouncementContent content)
        {
            try
            {
                content.BuildDefaultHeaders(Client);
                var response = GetStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"[{content.User.UserName}]获取物业公告失败 - {response?.Message},请检查参数");
                    return;
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("code").GetUInt32();
                var msg = root.GetProperty("message").GetString();
                if (code != 200)
                {
                    MainSession.PrintLogEvent.Publish(this, $"[{content.User.UserName}]获取物业公告失败: code={code}, message={msg}");
                    return;
                }

                var result = root.GetProperty("result");
                var ann =  ResolveDefaultAnnouncement(result, content.User);
                content.AnnouncementId = ann.Item1;
                content.Title = ann.Item2;
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"[{content.User.UserName}]获取物业公告失败 - {ex.Message} - {ex.StackTrace}");
            }
        }

        private (string, string) ResolveDefaultAnnouncement(JsonElement result, UserProject user)
        {
            var items = JsonAnalysis.JsonToDicList(result.GetProperty("items"));

            //var notCommentItems = items.Where(x => x.GetString("comments").ToObjDic().GetString("count") == "0").ToList();

            //var notCommentItem = notCommentItems?.FirstOrDefault();
            //if (notCommentItem == null)
            //{
            //    MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}] - 没有未读公告");
            //    return (string.Empty, string.Empty);
            //}
            var notReadItem = items.FirstOrDefault(x => x.GetString("is_read") == "False");
            if (notReadItem == null)
            {
                MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}] - 没有未读公告");
                return (string.Empty, string.Empty);
            }
            var id = notReadItem.GetString("id");
            var title = $"{notReadItem.GetString("title")}-{notReadItem.GetString("abstract")}";

            return (id, title);
        }

        private void ReadAnnouncement(AnnouncementContent content)
        {
            try
            {
                var bottomUrl = "https://chaos.4009515151.com/fd/api/announcements/";
                content.RequestUrl = $"{bottomUrl}{content.AnnouncementId}/bottom";
                var response = GetStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"[{content.User.UserName}]ReadAnnouncement失败 - {response?.Message},请检查参数");
                    return;
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("code").GetUInt32();
                var msg = root.GetProperty("message").GetString();
                if (code != 200)
                {
                    MainSession.PrintLogEvent.Publish(this, $"[{content.User.UserName}]ReadAnnouncement失败: code={code}, message={msg}");
                    return;
                }

                MainSession.PrintLogEvent.Publish(this, $"[{content.User.UserName}]-读取公告【{content.Title}】: code={code}, message={msg}");
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"[{content.User.UserName}]ReadAnnouncement失败 - {ex.Message} - {ex.StackTrace}");
            }
        }

        private void CommentAnnouncement(AnnouncementContent content)
        {
            try
            {
                var bottomUrl = "https://chaos.4009515151.com/fd/api/announcements/";
                content.RequestUrl = $"{bottomUrl}{content.AnnouncementId}/comments";
                var response = PostStringAsync(content, HttpProcessor.Content.ContentType.Json, false).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"[{content.User.UserName}]公告评论失败 - {response?.Message},请检查参数");
                    return;
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("code").GetUInt32();
                var msg = root.GetProperty("message").GetString();
                if (code != 200)
                {
                    MainSession.PrintLogEvent.Publish(this, $"[{content.User.UserName}]公告评论失败: code={code}, message={msg}");
                    return;
                }

                var result = root.GetProperty("result");
                ResolveConnmentResult(result, content);

            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"[{content.User.UserName}]公告评论失败 - {ex.Message} - {ex.StackTrace}");
            }
        }

        private void ResolveConnmentResult(JsonElement result, AnnouncementContent content)
        {
            var contentMsg = result.GetProperty("content").GetString();
            MainSession.PrintLogEvent.Publish(this, $"[{content.User.UserName}]-评论公告【{content.Title}】: content={contentMsg}");
        }
    }
}
