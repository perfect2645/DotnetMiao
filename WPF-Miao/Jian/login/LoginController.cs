using HttpProcessor.Client;
using Jian.session;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Jian.login
{
    internal class LoginController : HttpClientBase
    {
        public LoginController(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task LoginAsync(JianLogin user)
        {
            return Task.Factory.StartNew(() => Login(user));
        }

        private void Login(JianLogin user)
        {
            try
            {
                var content = new LoginContent(user);
                content.AddHeader("uid", string.Empty);
                content.BuildDefaultHeaders(Client);
                var response = PostStringAsync(content, HttpProcessor.Content.ContentType.Json).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"Login - {response?.Message},请检查参数");
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
                SaveLogin(data, user);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"Login异常{ex.Message}");
            }
        }

        private void SaveLogin(JsonElement data, JianLogin user)
        {
            var authorization = data.GetString();
            if (string.IsNullOrEmpty(authorization))
            {
                MainSession.PrintLogEvent.Publish(this, $"authorization is empty, Login失败");
                return;
            }

            user.Authorization = authorization;

            MainSession.PrintLogEvent.Publish(this, authorization);
        }
    }
}
