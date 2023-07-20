using HttpProcessor.Client;
using SixWater.common;
using SixWater.session;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Interop;
using Utils;
using Utils.json;
using Utils.stringBuilder;
using SixWater.login;
using System;

namespace SixWater.login
{
    internal class UserController : HttpClientBase
    {
        public UserController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void GetUserAsync(SixWaterLogin user)
        {
            Task.Factory.StartNew(() => GetUser(user));
        }

        private void GetUser(SixWaterLogin user)
        {
            try
            {
                var content = new UserContent(user);
                content.BuildDefaultHeaders(Client);
                var response = PostStringAsync(content, HttpProcessor.Content.ContentType.String).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetUser - {response?.Message},请检查参数");
                    return;
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("errorCode").GetString();
                if (code != "0000")
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败: code={code}");
                    return;
                }

                var data = root.GetProperty("data");
                if (data.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败: results is empty");
                    return;
                }
                SaveUser(data, user);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"获取用户信息异常{ex.Message}");
            }
        }

        private void SaveUser(JsonElement data, SixWaterLogin user)
        {
            var userList = JsonAnalysis.JsonToDicList(data);
            if (!userList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败");
                return;
            }

            var defaultUser = userList.FirstOrDefault(x => x["name"].NotNullString() == user.UserName);
            if (defaultUser == null)
            {
                defaultUser = userList.FirstOrDefault();
            }
            var userName = defaultUser.GetString("name");
            var userId = defaultUser.GetString("id");

            user.UserId = userId;
            user.UserName = userName;

            MainSession.PrintLogEvent.Publish(this, defaultUser);
        }
    }
}
