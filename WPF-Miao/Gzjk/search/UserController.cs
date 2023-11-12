using Gzjk.login;
using Gzjk.session;
using HttpProcessor.Client;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;

namespace Gzjk.search
{
    internal class UserController : HttpClientBase
    {
        public UserController(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task GetUserAsync(GzjkLogin user)
        {
            return Task.Factory.StartNew(() => GetUser(user));
        }

        private void GetUser(GzjkLogin user)
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

                var status = root.GetProperty("status").GetInt32();
                if (status != 200)
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败: status={status}");
                    return;
                }

                var userElement = root.GetProperty("user");
                if (userElement.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败: results is empty");
                    return;
                }
                SaveUser(userElement, user);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"获取用户信息异常{ex.Message}");
            }
        }

        private void SaveUser(JsonElement userElement, GzjkLogin user)
        {
            var userInfo = JsonAnalysis.JsonToDic(userElement);
            if (!userInfo.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败");
                return;
            }

            var birthday = userInfo.GetString("birthday");
            user.Birthday = birthday;
            user.Phone = userInfo.GetString("tel");
            user.Idcard = userInfo.GetString("idcard");
            user.UserName = userInfo.GetString("cname");
            user.DocType = userInfo.GetInt("doctype");

            MainSession.PrintLogEvent.Publish(this, userInfo);
        }
    }
}
