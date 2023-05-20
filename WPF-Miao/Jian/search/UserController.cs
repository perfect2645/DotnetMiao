using Jian.login;
using Jian.session;
using HttpProcessor.Client;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;

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

                var code = root.GetProperty("code").GetInt16();
                if (code != 0)
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败: code={code}");
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

        private void SaveUser(JsonElement data, JianLogin user)
        {
            var userInfo = JsonAnalysis.JsonToDic(data);
            if (!userInfo.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败");
                return;
            }

            var userName = userInfo.GetString("patientName");
            var patientId = userInfo.GetString("patientId");
            var birthday = userInfo.GetString("birthday");
            var address = userInfo.GetString("patientAddress");

            user.PatientId = patientId;
            user.UserName = userName;
            user.Birthday = birthday;
            user.Address = address;

            MainSession.PrintLogEvent.Publish(this, userInfo);
        }
    }
}
