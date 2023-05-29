using Dxm.login;
using Dxm.session;
using HttpProcessor.Client;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;

namespace Dxm.login
{
    internal class LoginController : HttpClientBase
    {
        public LoginController(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task LoginAsync(DxmLogin user)
        {
            return Task.Factory.StartNew(() => Login(user));
        }

        private void Login(DxmLogin user)
        {
            try
            {
                var content = new LoginContent(user);
                content.AddHeader("uid", string.Empty);
                content.BuildDefaultHeaders(Client);
                var response = PostStringAsync(content, HttpProcessor.Content.ContentType.String).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"Login - {response?.Message},请检查参数");
                    return;
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("code").GetInt16();
                if (code != 0)
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
                SaveLogin(data, user);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"Login异常{ex.Message}");
            }
        }

        private void SaveLogin(JsonElement data, DxmLogin user)
        {
            var userInfo = JsonAnalysis.JsonToDic(data);
            if (!userInfo.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"Login失败");
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
