using Baohe.constants;
using Baohe.session;
using HttpProcessor.Client;
using HttpProcessor.ExceptionManager;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Utils.datetime;

namespace Baohe.verification
{
    internal class YzmController : HttpClientBase
    {
        public YzmController(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task<bool> SendYzmAsync(string userName, string phone = "")
        {
            return Task.Factory.StartNew(() => SendYzm(userName, phone));
        }

        public Task CheckYzmAsync(string yzm, string userName, string phone)
        {
            return Task.Factory.StartNew(() => CheckYzm(yzm, userName, phone));
        }

        private bool SendYzm(string userName, string phone = "")
        {
            var url = "https://appoint.yihu.com/appoint/do/registerAuth/sendYzm";
            var content = new SendYzmContent(url, userName, phone);
            content.AddHeader("Cookie", MainSession.Cookie);
            content.AddHeader("Referer", content.BuildReferer());

            content.BuildDefaultHeaders(Client);

            HttpDicResponse response = PostStringAsync(content, HttpProcessor.Content.ContentType.String).Result;
            var code = response.Body.FirstOrDefault(x => x.Key == Constant.StatusCode).Value?.ToString();
            if (code == null || code != "10000")
            {
                throw new HttpException($"{Constant.ProjectName}: {response.Body["Message"]}", "Send Yzm");
            }

            MainSession.PrintLogEvent.Publish(this, $"验证码发送成功 Tel={content.Tel}");
            return true;
        }

        private void CheckYzm(string yzm, string userName, string phone)
        {
            var url = "https://appoint.yihu.com/appoint/do/registerAuth/checkYzm";
            var content = new CheckYzmContent(url, yzm, userName, phone);
            content.AddHeader("Cookie", MainSession.Cookie);
            content.AddHeader("Referer", content.BuildReferer());

            content.BuildDefaultHeaders(Client);

            HttpDicResponse response = PostStringAsync(content, HttpProcessor.Content.ContentType.String).Result;
            var code = response.Body.FirstOrDefault(x => x.Key == Constant.StatusCode).Value?.ToString();
            if (code == null || code != "10000")
            {
                throw new HttpException($"{Constant.ProjectName}: {response.Body["Message"]}", "check Yzm");
            }

            MainSession.PrintLogEvent.Publish(this, $"验证码验证成功 Tel={content.Tel}");
            MainSession.IsYzmChecked = true;
        }
    }
}
