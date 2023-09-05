using HttpProcessor.Client;
using Kuerle.common;
using Kuerle.session;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Interop;
using Utils;
using Utils.json;
using Utils.stringBuilder;
using Kuerle.login;
using System;

namespace Kuerle.login
{
    internal class UserController : HttpClientBase
    {
        public UserController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void GetUserAsync(KuerleLogin user)
        {
            Task.Factory.StartNew(() => GetUser(user));
        }

        private void GetUser(KuerleLogin user)
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

                var code = root.GetProperty("ErrCode").GetInt32();
                if (code != 0)
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败: code={code}");
                    return;
                }

                SaveUser(root, user);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"获取用户信息异常{ex.Message}");
            }
        }

        private void SaveUser(JsonElement data, KuerleLogin user)
        {
            var userInfo = JsonAnalysis.JsonToDic(data);
            if (!userInfo.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败");
                return;
            }

            var userName = userInfo.GetString("name");

            user.UserName = userName;

            MainSession.PrintLogEvent.Publish(this, userInfo);
        }
    }
}
