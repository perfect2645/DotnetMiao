using Sanya.login;
using Sanya.session;
using HttpProcessor.Client;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;

namespace Sanya.login
{
    internal class LoginController : HttpClientBase
    {
        public LoginController(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task LoginAsync(SanyaLogin user)
        {
            return Task.Factory.StartNew(() => Login(user));
        }

        private void Login(SanyaLogin user)
        {
            try
            {
                var content = new LoginContent(user);
                content.BuildDefaultHeaders(Client);
                var response = GetStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetUser - {response?.Message},请检查参数");
                    return;
                }
                var root = response.JsonBody.RootElement;

                var success = root.GetProperty("success").GetBoolean();
                var msg = root.GetProperty("msg").GetString();
                if (!success)
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败: success={success}, msg = {msg}");
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

        private void SaveUser(JsonElement data, SanyaLogin user)
        {
            var userInfoList = JsonAnalysis.JsonToDicList(data);
            if (!userInfoList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败");
                return;
            }

            var targetUser = userInfoList.FirstOrDefault(x => x.GetString("name") == user.UserName);
            if (targetUser == null)
            {
                targetUser = userInfoList.FirstOrDefault();
            }

            var userName = targetUser.GetString("name");
            var familyId = targetUser.GetString("family_id");

            user.FamilyId = familyId;
            user.UserName = userName;
            user.Phone = targetUser.GetString("tel");
            user.Idcard = targetUser.GetString("idcard");
            user.OpenId = targetUser.GetString("openid");

            MainSession.PrintLogEvent.Publish(this, targetUser);
        }
    }
}
