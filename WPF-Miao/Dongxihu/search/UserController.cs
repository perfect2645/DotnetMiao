using HttpProcessor.Client;
using Dongxihu.common;
using Dongxihu.session;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Interop;
using Utils;
using Utils.json;
using Utils.stringBuilder;
using Dongxihu.login;
using System;

namespace Dongxihu.search
{
    internal class UserController : HttpClientBase
    {
        public UserController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void GetUserAsync(DongxihuLogin user)
        {
            Task.Factory.StartNew(() => GetUser(user));
        }

        private void GetUser(DongxihuLogin user)
        {
            try
            {
                var content = new UserContent(user);
                content.BuildDefaultHeaders(Client);
                var response = PostStringAsync(content, HttpProcessor.Content.ContentType.String).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetUser - {response?.Message},请检查参数");
                    return;
                }
                var root = response.JsonBody.RootElement;

                var responseResult = root.GetProperty("responseResult");
                if (responseResult.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败: results is empty");
                    return;
                }
                var isSuccess = responseResult.GetProperty("isSuccess").GetString();
                if (isSuccess != "1")
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败: isSuccess = {isSuccess}");
                    return;
                }

                var bindCardList = root.GetProperty("bindCardList");

                SaveUser(bindCardList, user);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"获取用户信息异常{ex.Message}");
            }
        }

        private void SaveUser(JsonElement data, DongxihuLogin user)
        {
            var familyMembers = JsonAnalysis.JsonToDicList(data);
            if (!familyMembers.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败");
                return;
            }

            var defaultUser = familyMembers.FirstOrDefault(x => x["patientName_Sort"].NotNullString() == user.UserName);
            if (defaultUser == null)
            {
                defaultUser = familyMembers.FirstOrDefault();
            }
            var userName = defaultUser.GetString("patientName_Sort");
            var userId = defaultUser.GetString("hospitalUserID");

            user.UserId = userId;
            user.UserName = userName;

            MainSession.PrintLogEvent.Publish(this, defaultUser);
        }
    }
}
