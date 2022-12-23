using HttpProcessor.Client;
using Jingyang.common;
using Jingyang.session;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Jingyang.appointment
{
    internal class TokenController : HttpClientBase
    {
        public TokenController(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<string> GetTokenAsync(string cookie)
        {
            return await Task.Factory.StartNew(() => GetToken(cookie));
        }

        private string GetToken(string cookie)
        {
            try
            {
                var url = $"http://cnfw.mailiku.com/index/api/getToken.html";
                var content = new JingyangContent(url, cookie);
                content.BuildDefaultHeaders(Client);
                var response = GetStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetUser - {response?.Message},请检查参数");
                    return string.Empty;
                }

                return "";
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"GetUser异常{ex.Message}");
                return null;
            }
        }
    }
}
