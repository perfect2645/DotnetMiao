using Haikou.session;
using HttpProcessor.Client;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;
using Utils.stringBuilder;

namespace Haikou.login
{
    internal class UserController : HttpClientBase
    {
        public UserController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void GetUserAsync(HaikouLogin user)
        {
            Task.Factory.StartNew(() => GetUser(user));
        }

        private void GetUser(HaikouLogin user)
        {
            try
            {
                var content = new UserContent(user);
                content.BuildDefaultHeaders(Client);
                var response = PostStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetUser - {response?.Message},请检查参数");
                    return;
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("code").GetInt32();
                var msg = root.GetProperty("msg").GetString();
                if (code != 0 || msg != "success")
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败:{user.UserName} code={code}, msg={msg}");
                    return;
                }

                var data = root.GetProperty("healthCardList");
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

        private void SaveUser(JsonElement data, HaikouLogin user)
        {
            var userList = JsonAnalysis.JsonToDicList(data);
            if (!userList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败");
                return;
            }

            var defaultUser = userList.FirstOrDefault(x => x["patientName"].NotNullString() == user.UserName);
            if (defaultUser == null)
            {
                defaultUser = userList.FirstOrDefault();
            }
            var userName = defaultUser.GetString("patientName");
            var userId = defaultUser.GetString("patientId");

            user.UserId = userId;
            user.UserName = userName;

            MainSession.PrintLogEvent.Publish(this, defaultUser);
        }
    }
}
