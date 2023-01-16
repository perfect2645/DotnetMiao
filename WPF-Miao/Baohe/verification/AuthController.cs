using Baohe.constants;
using Baohe.session;
using HttpProcessor.Client;
using HttpProcessor.ExceptionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Baohe.verification
{
    internal class AuthController : HttpClientBase
    {
        public AuthController(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task CheckAuthAsync()
        {
            await Task.Factory.StartNew(() =>
            {
                CheckAuth();
            });
        }

        private void CheckAuth()
        {
            var url = "https://appoint.yihu.com/appoint/do/registerAuth/idCardAuth";
            var content = new AuthContent(url);
            content.AddHeader("Cookie", MainSession.Cookie);
            content.AddHeader("Referer", content.BuildReferer());

            content.BuildDefaultHeaders(Client);

            HttpDicResponse response = PostStringAsync(content, HttpProcessor.Content.ContentType.String).Result;
            var code = response.Body.FirstOrDefault(x => x.Key == Constant.StatusCode).Value?.ToString();
            if (code == null || code != "10000")
            {
                throw new HttpException($"{Constant.ProjectName}: {response.Body["Message"]}", "check Yzm");
            }
            var message = response.Body.FirstOrDefault(x => x.Key == "Message").Value?.ToString();

            MainSession.PrintLogEvent.Publish(this, $"用户实名验证 - {message}");
        }
    }
}
