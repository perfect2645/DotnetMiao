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

        public Task SendYzmAsync()
        {
            return Task.Factory.StartNew(() => SendYzm());
        }

        private void SendYzm()
        {
            var url = "https://appoint.yihu.com/appoint/do/registerAuth/sendYzm";
            var content = new SendYzmContent(url);
            content.AddHeader("Cookie", BaoheSession.Cookie);
            content.AddHeader("Referer", content.BuildReferer());

            content.BuildDefaultHeaders(Client);

            HttpDicResponse response = PostStringAsync(content).Result;
            var code = response.Body.FirstOrDefault(x => x.Key == Constant.StatusCode).Value?.ToString();
            if (code == null || code != "10000")
            {
                throw new HttpException($"{Constant.ProjectName}: {response.Body["Message"]}", "Send Yzm");
            }

            BaoheSession.PrintLogEvent.Publish(this, $"验证码发送成功 Tel={content.Tel}, Time = {DateTimeUtil.GetNow()}");
        }
    }
}
