using HttpProcessor.Client;
using Huaxi.session;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;
using Utils.stringBuilder;

namespace Huaxi.login
{
    internal class UserController : HttpClientBase
    {
        public UserController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void GetUserAsync(HuaxiLogin user)
        {
            Task.Factory.StartNew(() => GetUser(user));
        }

        private void GetUser(HuaxiLogin user)
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

                var code = root.GetProperty("code").GetString();
                var msg = root.GetProperty("msg").GetString();
                if (code != "1")
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败: code={code}, msg={msg}");
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

        private void SaveUser(JsonElement data, HuaxiLogin user)
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
            var userId = defaultUser.GetString("userId");
            var cardId = defaultUser.GetString("cardId");

            user.UserId = userId;
            user.UserName = userName;
            user.CardId = cardId;

            MainSession.PrintLogEvent.Publish(this, defaultUser);
        }
    }
}
