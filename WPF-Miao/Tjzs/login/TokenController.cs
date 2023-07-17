using HttpProcessor.Client;
using HttpProcessor.Content;
using Tjzs.session;
using System;
using System.Net.Http;
using System.Text.Json;
using Utils;
using Utils.json;
using Utils.stringBuilder;
using Tjzs.appointment;

namespace Tjzs.login
{
    internal class TokenController : HttpClientBase
    {
        public TokenController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void RefreshToken(Order order)
        {
            try
            {
                var tokenContent = new TokenContent(order);
                tokenContent.BuildDefaultHeaders(Client);
                HttpDicResponse response = PostStringAsync(tokenContent, ContentType.Json, false).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"RefreshToken failed - {response?.Message},请检查参数");
                    return;
                }

                var root = response.JsonBody.RootElement;
                var code = root.GetProperty("result_code").NotNullString();
                var msg = root.GetProperty("result_message").NotNullString();
                if (code != "0" || msg != "成功")
                {
                    MainSession.PrintLogEvent.Publish(this, $"RefreshToken:code={code}, message: {msg}");
                    return;
                }

                SaveToken(root, order);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"RefreshToken {ex.Message}");
            }
        }

        private void SaveToken(JsonElement root, Order order)
        {
            var authToken = root.GetProperty("auth_token").GetString();
            var auth_refresh_token = root.GetProperty("auth_refresh_token").GetString();

            order.RefreshToken = auth_refresh_token;
            order.Authorization = authToken;
        }
    }
}
