using HttpProcessor.Client;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;
using Utils.stringBuilder;
using Xihongmen.login;
using Xihongmen.session;

namespace Xihongmen.search
{
    internal class UserController : HttpClientBase
    {
        public UserController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void GetUserAsync(XhmLogin user)
        {
            Task.Factory.StartNew(() => GetUser(user));
        }

        public void GetUser(XhmLogin user)
        {
            try
            {
                var content = new UserContent(user);
                content.BuildDefaultHeaders(Client);
                var response = PostStringAsync(content, HttpProcessor.Content.ContentType.String).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取用户失败 - {response?.Message},请检查参数");
                }
                var code = response.JsonBody.RootElement.GetProperty("code").NotNullString();
                var msg = response.JsonBody.RootElement.GetProperty("msg").NotNullString();
                MainSession.PrintLogEvent.Publish(this, $"GetUser - {msg}");
                if ("200".Equals(code) && "OK".Equals(msg))
                {
                    var data = response.JsonBody.RootElement.GetProperty("data");
                    SaveUsers(data, user);
                }
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"获取用户异常{ex.Message}");
            }
        }

        private void SaveUsers(JsonElement data, XhmLogin user)
        {
            var familyMembers = JsonAnalysis.JsonToDicList(data);
            if (!familyMembers.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败");
                return;
            }
            var defaultUser = familyMembers.FirstOrDefault(x => x["child_name"].NotNullString() == user.UserName);
            if (defaultUser == null)
            {
                defaultUser = familyMembers.FirstOrDefault();
            }
            user.UserId = defaultUser.GetString("id");
            user.UserName = defaultUser.GetString("child_name");
            user.IdCard = defaultUser.GetString("child_ID_number");

            MainSession.PrintLogEvent.Publish(this, defaultUser);
        }
    }
}
