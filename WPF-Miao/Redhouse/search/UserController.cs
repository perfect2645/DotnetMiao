using HttpProcessor.Client;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;
using Utils.stringBuilder;
using Redhouse.session;
using HttpProcessor.Container;

namespace Redhouse.search
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

        private void GetUser()
        {
            try
            {
                var content = new UserContent();
                content.BuildDefaultHeaders(Client);
                content.BuildContent();
                HttpDicResponse response = PostStringAsync(content, HttpProcessor.Content.ContentType.Json).Result;
                if (response?.JsonBody?.RootElement == null)
                {
                    Log($"获取用户信息失败{response?.Message}");
                    return;
                }

                var root = response.JsonBody.RootElement;
                var code = root.GetProperty("code").NotNullString();
                var message = root.GetProperty("message").NotNullString();
                if (!"200".Equals(code))
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败:code={code}, message={message}");
                    return;
                }
                var data = root.GetProperty("data");
                if (data.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败:code={code}, message={message}");
                    return;
                }
                SaveUserInfo(data);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败 - {ex.Message} - {ex.StackTrace}");
            }
        }

        private void SaveUserInfo(JsonElement data)
        {
            var userList = JsonAnalysis.JsonToDicList(data);
            if (!userList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败: userList为空");
                return;
            }

            var defaultUser = userList.FirstOrDefault();

            var defaultUserId = defaultUser.GetString("id");
            var defaultUserName = defaultUser.GetString("name");
            MainSession.PlatformSession.AddOrUpdate(Constants.UserId, defaultUserId);
            MainSession.PlatformSession.AddOrUpdate(Constants.UserName, defaultUserName);
            MainSession.PlatformSession.AddOrUpdate("user", defaultUser);

            GetSaveCardId();

            MainSession.PrintLogEvent.Publish(this, defaultUser);
        }

        private void GetSaveCardId()
        {
            var wechatUserController = HttpServiceController.GetService<WechatUserController>();
            wechatUserController.GetUser();
        }
    }
}
