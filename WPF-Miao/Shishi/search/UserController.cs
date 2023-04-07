using HttpProcessor.Client;
using Shishi.common;
using Shishi.session;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Interop;
using Utils;
using Utils.json;
using Utils.stringBuilder;
using Shishi.login;
using System;

namespace Shishi.search
{
    internal class UserController : HttpClientBase
    {
        public UserController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void GetUserAsync(ShishiLogin user)
        {
            Task.Factory.StartNew(() => GetUser(user));
        }

        private void GetUser(ShishiLogin user)
        {
            try
            {
                var url = $"https://ldsq.ldrmyy120.com/rest/v1/patient/list/?limit=100&offset=0";
                var content = new ShishiContent(url, user);
                content.BuildDefaultHeaders(Client);
                var response = GetStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetUser - {response?.Message},请检查参数");
                    return;
                }
                var root = response.JsonBody.RootElement;

                var results = root.GetProperty("results");
                if (results.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败: results is empty");
                    return;
                }
                SaveUser(results, user);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"获取用户信息异常{ex.Message}");
            }
        }

        private void SaveUser(JsonElement data, ShishiLogin user)
        {
            var familyMembers = JsonAnalysis.JsonToDicList(data);
            if (!familyMembers.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败");
                return;
            }

            var defaultUser = familyMembers.FirstOrDefault(x => x["name"].NotNullString() == user.UserName);
            if (defaultUser == null)
            {
                defaultUser = familyMembers.FirstOrDefault();
            }
            var userName = defaultUser.GetString("name");
            var userId = defaultUser.GetString("id");
            var idcard = defaultUser.GetString("idno");
            var phone = defaultUser.GetString("mobile");

            user.UserId = userId;
            user.UserName = userName;
            user.IdCard = idcard;
            user.Phone = phone;

            MainSession.PrintLogEvent.Publish(this, defaultUser);
        }
    }
}
