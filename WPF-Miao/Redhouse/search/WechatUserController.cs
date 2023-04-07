using HttpProcessor.Client;
using Redhouse.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;
using Utils.stringBuilder;

namespace Redhouse.search
{
    internal class WechatUserController : HttpClientBase
    {
        public WechatUserController(HttpClient httpClient) : base(httpClient)
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

            var certificationNo = defaultUser.GetString("certificationNo");

            var userInfo = MainSession.PlatformSession["user"] as Dictionary<string, object>;
            userInfo.AddOrUpdate("certificationNo", certificationNo);
        }
    }
}
