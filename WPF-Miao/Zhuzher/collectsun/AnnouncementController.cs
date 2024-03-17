using HttpProcessor.Client;
using System;
using System.Linq;
using System.Net.Http;
using System.Security.RightsManagement;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;
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
            foreach (var user in UserProjectList.UserProjects)
            {
                Task.Factory.StartNew(() => CollectSun(user));
            }
        }

        private void CollectSun(UserProject user)
        {
            var content = new AnnouncementContent(user);
            for (int i = 0; i < 3; i++) 
            {
                var ann = GetAnnouncement(content);
                ReadAnnouncement();
                CommentAnnouncement();
            }
        }

        private (string, string) GetAnnouncement(AnnouncementContent content)
        {
            try
            {
                content.BuildDefaultHeaders(Client);
                var response = GetStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"[{content.User.UserName}]获取物业公告失败 - {response?.Message},请检查参数");
                    return (string.Empty, string.Empty);
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("code").GetUInt32();
                var msg = root.GetProperty("message").GetString();
                if (code != 200)
                {
                    MainSession.PrintLogEvent.Publish(this, $"[{content.User.UserName}]获取物业公告失败: code={code}, message={msg}");
                    return (string.Empty, string.Empty);
                }

                var result = root.GetProperty("result");
                return ResolveDefaultAnnouncement(result, content.User);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"[{content.User.UserName}]获取物业公告失败 - {ex.Message} - {ex.StackTrace}");
                return (string.Empty, string.Empty);
            }
        }

        private (string, string) ResolveDefaultAnnouncement(JsonElement result, UserProject user)
        {
            var items = JsonAnalysis.JsonToDicList(result.GetProperty("items"));

            var isReadItem = items.FirstOrDefault(x => x.GetString("is_read") == "False");
            if (isReadItem == null)
            {
                MainSession.PrintLogEvent.Publish(this, $"[{user.UserName}] - 没有未读公告");
                return (string.Empty, string.Empty);
            }

            var id = isReadItem.GetString("id");
            var title = $"{isReadItem.GetString("title")}-{isReadItem.GetString("abstract")}";

            return (id, title);
        }

        private void ReadAnnouncement()
        {
            
        }

        private void CommentAnnouncement()
        {
        }
    }
}
