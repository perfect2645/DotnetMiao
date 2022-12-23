using HttpProcessor.Client;
using Jingyang.common;
using Jingyang.session;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Utils;

namespace Jingyang.appointment
{
    internal class TokenController : HttpClientBase
    {
        private JingyangContent TokenContent;

        private bool IsHeaderBuilt;

        public TokenController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void BuildContent(string cookie)
        {
            if (!IsHeaderBuilt)
            {
                var url = $"http://cnfw.mailiku.com/index/api/getToken.html";
                TokenContent = new JingyangContent(url, cookie);
                TokenContent.BuildDefaultHeaders(Client);

                IsHeaderBuilt = true;
            }
        }

        public async Task<string> GetTokenAsync(string cookie)
        {
            return await Task.Factory.StartNew(() => GetToken(cookie));
        }

        public string GetToken(string cookie)
        {
            try
            {
                var response = GetStringAsync(TokenContent).Result;

                var headers = response?.Headers;
                if (headers == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetToken - {response?.Message},请检查参数");
                    return string.Empty;
                }

                var root = response.JsonBody?.RootElement;
                if (root == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetToken - token is empty");
                    return string.Empty;
                }
                var token = root.GetValueOrDefault().ToString();

                return token;
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"GetToken异常{ex.Message}");
                return null;
            }
        }
    }
}
