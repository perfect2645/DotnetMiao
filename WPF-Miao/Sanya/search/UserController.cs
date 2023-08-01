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
using Sanya.common;

namespace Sanya.search
{
    internal class UserController : HttpClientBase
    {
        public UserController(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task GetUserAsync(SanyaLogin user)
        {
            return Task.Factory.StartNew(() => GetUser(user));
        }

        private void GetUser(SanyaLogin user)
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
                
                if (code != 0)
                {
                    var message = root.GetProperty("message").GetString();
                    MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败[{user.UserName}]: code={code}, message = {message}");
                    return;
                }

                var data = root.GetProperty("data");
                if (data.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败: results is empty");
                    return;
                }
                SaveUser(data.GetString(), user);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"获取用户信息异常{ex.Message}");
            }
        }

        private void SaveUser(string dataEncode, SanyaLogin user)
        {
            var dataDecode = JsReader.DecodeAesCbc(dataEncode);
            var userInfoList = JsonAnalysis.JsonToDicList(dataEncode);
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
