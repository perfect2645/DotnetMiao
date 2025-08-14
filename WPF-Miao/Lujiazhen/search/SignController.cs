using HttpProcessor.Client;
using Lujiazhen.login;
using Lujiazhen.session;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;
using Utils.stringBuilder;

namespace Lujiazhen.search
{
    internal class SignController : HttpClientBase
    {
        public SignController(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<string> GetSignAsync(LujiazhenLogin user)
        {
            var sign = await Task.Factory.StartNew(() => GetSign(user));

            return sign;
        }

        private string GetSign(LujiazhenLogin user)
        {
            try
            {
                var content = new SignContent(user);
                content.BuildDefaultHeaders(Client);
                var response = PostStringAsync(content, HttpProcessor.Content.ContentType.String).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetUser - {response?.Message},请检查参数");
                    return string.Empty;
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("errorCode").GetString();
                if (code != "0000")
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败: code={code}");
                    return string.Empty;
                }

                var data = root.GetProperty("data");
                if (data.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"获取用户信息失败: results is empty");
                    return string.Empty;
                }
                var sign = SaveSign(data, user);

                return sign;
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"获取用户信息异常{ex.Message}");
                return null;
            }
        }

        private string SaveSign(JsonElement data, LujiazhenLogin user)
        {
            var sign = data.GetString();


            return sign;
        }
    }
}