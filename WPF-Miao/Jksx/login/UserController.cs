using HttpProcessor.Client;
using Jksx.common;
using Jksx.session;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;
using Utils.stringBuilder;

namespace Jksx.login
{
    internal class UserController : HttpClientBase
    {
        public UserController(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task GetUserAsync(JksxLogin user)
        {
            return Task.Factory.StartNew(() => GetUser(user));
        }

        private void GetUser(JksxLogin user)
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
                if (code != 1)
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败: code={code} : {msg}");
                    return;
                }

                var data = root.GetProperty("result");
                if (data.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败: results is empty");
                    return;
                }
                
                var userDataEncode = data.GetString();

                SaveUser(userDataEncode, user);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"获取用户信息异常{ex.Message}");
            }
        }

        private void SaveUser(string userDataEncode, JksxLogin user)
        {
            var userList = Encotor.DecodeToDicList(userDataEncode);
            if (!userList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败");
                return;
            }

            var defaultUser = userList.FirstOrDefault(x => x["username"].NotNullString() == user.UserName);
            if (defaultUser == null)
            {
                defaultUser = userList.FirstOrDefault();
            }
            var userName = defaultUser.GetString("username");
            var userId = defaultUser.GetString("userid");

            user.UserId = userId;
            user.UserName = userName;

            MainSession.PrintLogEvent.Publish(this, defaultUser);
        }
    }
}
