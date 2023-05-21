using Jian.login;
using Jian.session;
using HttpProcessor.Client;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;
using System.Linq;

namespace Jian.search
{
    internal class UserController : HttpClientBase
    {
        public UserController(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task GetUserAsync(JianLogin user)
        {
            return Task.Factory.StartNew(() => GetUser(user));
        }

        private void GetUser(JianLogin user)
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

                var code = root.GetProperty("resultCode").GetInt16();
                var success = root.GetProperty("success").GetBoolean();
                if (code != 20000 || !success)
                {
                    MainSession.PrintLogEvent.Publish(this, $"Login失败: code={code}");
                    return;
                }

                var data = root.GetProperty("data");
                if (data.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"Login失败: results is empty");
                    return;
                }
                SaveUser(data, user);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"获取用户信息异常{ex.Message}");
            }
        }

        private void SaveUser(JsonElement data, JianLogin user)
        {
            var userInfoList = JsonAnalysis.JsonToDicList(data);
            if (!userInfoList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败");
                return;
            }

            var defaultUser = userInfoList.FirstOrDefault(u => u.GetString("Name") == user.UserName);
            if (defaultUser == null)
            {
                defaultUser = userInfoList.FirstOrDefault();
            }

            var userName = defaultUser.GetString("Name");
            var idCardNo = defaultUser.GetString("IdCardNo");
            var mzNo = defaultUser.GetString("MzNo");

            user.UserName = userName;
            user.IdCardNo = idCardNo;
            user.MzNo = mzNo;

            MainSession.PrintLogEvent.Publish(this, userInfoList);
        }
    }
}
