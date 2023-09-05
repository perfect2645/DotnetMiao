using HttpProcessor.Client;
using Anze.common;
using Anze.session;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Interop;
using Utils;
using Utils.json;
using Utils.stringBuilder;
using Anze.login;
using System;

namespace Anze.login
{
    internal class UserController : HttpClientBase
    {
        public UserController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void GetUserAsync(AnzeLogin user)
        {
            Task.Factory.StartNew(() => GetUser(user));
        }

        private void GetUser(AnzeLogin user)
        {
            try
            {
                var content = new UserContent(user);
                content.BuildDefaultHeaders(Client);
                var response = PostStringAsync(content, HttpProcessor.Content.ContentType.Json).Result;
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

        private void SaveUser(JsonElement data, AnzeLogin user)
        {
            var userInfo = JsonAnalysis.JsonToDic(data);
            if (!userInfo.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败");
                return;
            }

            var userName = userInfo.GetString("truename");
            //var phoneNumber = userInfo.GetString("phoneNumber");
            //var idcard = userInfo.GetString("idcard");

            user.UserName = userName;

            MainSession.PrintLogEvent.Publish(this, userInfo);
        }
    }
}
