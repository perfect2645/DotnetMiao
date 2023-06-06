using B114.login;
using B114.session;
using HttpProcessor.Client;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;

namespace B114.search
{
    internal class UserController : HttpClientBase
    {
        public UserController(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task GetUserAsync(B114Login user)
        {
            return Task.Factory.StartNew(() => GetUser(user));
        }

        private void GetUser(B114Login user)
        {
            try
            {
                var content = new UserContent(user);
                content.BuildDefaultHeaders(Client);
                var response = GetStringAsync(content).Result;
                 if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetUser - {response?.Message},请检查参数");
                    return;
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("code").GetInt32();
                if (code != 200)
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败: code={code}");
                    return;
                }

                var result = root.GetProperty("result");
                if (result.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败: results is empty");
                    return;
                }
                SaveUser(result, user);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"获取用户信息异常{ex.Message}");
            }
        }

        private void SaveUser(JsonElement data, B114Login user)
        {
            var userInfoList = JsonAnalysis.JsonToDicList(data);
            if (!userInfoList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败");
                return;
            }


            var targetUser = userInfoList.FirstOrDefault(x => x.GetString("booName") == user.UserName);
            if (targetUser == null)
            {
                targetUser = userInfoList.FirstOrDefault();
            }

            var userName = targetUser.GetString("booName");
            var patientId = targetUser.GetString("id");

            user.UserId = patientId;
            user.UserName = userName;

            MainSession.PrintLogEvent.Publish(this, targetUser);
        }
    }
}
