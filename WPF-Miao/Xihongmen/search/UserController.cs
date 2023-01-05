using HttpProcessor.Client;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;
using Utils.stringBuilder;
using Xihongmen.session;

namespace Xihongmen.search
{
    internal class UserController : HttpClientBase
    {
        public UserController(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task GetUserAsync()
        {
            await Task.Factory.StartNew(() => GetUser());
        }

        public void GetUser()
        {
            try
            {
                var content = new UserContent();
                content.BuildDefaultHeaders(Client);
                var response = PostStringAsync(content, HttpProcessor.Content.ContentType.String).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取用户失败 - {response?.Message},请检查参数");
                }
                var code = response.JsonBody.RootElement.GetProperty("code").NotNullString();
                var msg = response.JsonBody.RootElement.GetProperty("msg").NotNullString();
                MainSession.PrintLogEvent.Publish(this, $"GetUser - {msg}");
                if ("200".Equals(code) && "OK".Equals(msg))
                {
                    var data = response.JsonBody.RootElement.GetProperty("data");
                    SaveUser(data);
                }
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"获取用户异常{ex.Message}");
            }
        }

        private void SaveUser(JsonElement data)
        {
            var familyMembers = JsonAnalysis.JsonToDicList(data);
            if (!familyMembers.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败");
                return;
            }

            var defaultUser = familyMembers.FirstOrDefault();
            var userId = defaultUser.GetString("id");
            var userName = defaultUser.GetString("child_name");
            var idcard = defaultUser.GetString("child_ID_number");

            MainSession.PlatformSession.AddOrUpdate(Constants.UserId, userId);
            MainSession.PlatformSession.AddOrUpdate("user", defaultUser);

            MainSession.PrintLogEvent.Publish(this, defaultUser);
        }
    }
}
