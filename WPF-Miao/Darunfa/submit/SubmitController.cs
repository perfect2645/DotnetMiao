using Darunfa.session;
using HttpProcessor.Client;
using HttpProcessor.Content;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Utils.datetime;

namespace Darunfa.submit
{
    internal class SubmitController : HttpClientBase
    {
        public SubmitController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void AppointAsync()
        {
            Task.Factory.StartNew(() => Appoint());
        }

        public void Appoint()
        {
            var url = "https://yx.feiniu.com/cart-yxapp/account/createOrder/t141";

            try
            {
                var content = new SubmitContent(url);
                content.BuildDefaultHeaders(Client);

                HttpDicResponse response = PostStringAsync(content, ContentType.Json).Result;
                if (response == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"Appoint response is null");
                    return;
                }
                ValidateRes(response);

            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"预约异常{ex.Message}-{DateTimeUtil.GetNow()}");
            }

        }

        private void ValidateRes(HttpDicResponse response)
        {
            MainSession.PrintLogEvent.Publish(this, $"抢购完成时间 - {DateTimeUtil.GetNow()}");
            var errorDesc = response.JsonBody.RootElement.GetProperty("errorDesc").GetString();
            if (!string.IsNullOrEmpty(errorDesc))
            {
                MainSession.PrintLogEvent.Publish(this, $"抢购失败 - 原因：{errorDesc}");
                var logCode = response.JsonBody.RootElement.GetProperty("logCode").GetString();
                MainSession.PrintLogEvent.Publish(this, $"抢购失败 - LogCode：{logCode}");
                return;
            }
            var body = response.JsonBody.RootElement.GetProperty("body").ToString();
            if (!string.IsNullOrEmpty(body))
            {
                MainSession.PrintLogEvent.Publish(this, $"抢购失败 - 原因：body is empty");
                return;
            }
            MainSession.PrintLogEvent.Publish(this, $"result:{body}");
        }
    }
}
