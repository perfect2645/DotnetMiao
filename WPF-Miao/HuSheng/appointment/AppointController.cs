using HttpProcessor.Client;
using HttpProcessor.Content;
using HttpProcessor.HtmlAnalysis;
using HuSheng.session;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Utils.datetime;

namespace HuSheng.appointment
{
    internal class AppointController : HttpClientBase
    {
        public AppointController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void AppointAsync()
        {
            Task.Factory.StartNew(() => Appoint());
        }

        public void Appoint()
        {
            var url = "http://app.whkfqws.com/wx-mobile/Reservations/vaccinavaccina_save.do";

            try
            {
                var formDoc = (HushengSession.MiaoSession[Constants.MiaoHtml] as HtmlDoc)!;
                var content = new AppointContent(url, formDoc);
                content.AddHeader("Cookie", HushengSession.Cookie);

                content.BuildDefaultHeaders(Client);

                HttpDicResponse response = PostStringAsync(content, ContentType.String).Result;
                if (response == null)
                {
                    HushengSession.PrintLogEvent.Publish(this, $"Appoint response is null");
                }

                HushengSession.PrintLogEvent.Publish(this, $"预约完成");
                var result = response.JsonBody.RootElement.GetProperty("res").ToString();
                if (string.IsNullOrEmpty(result))
                {
                    HushengSession.PrintLogEvent.Publish(this, $"result:预约申请提交成功");
                    return;
                }
                HushengSession.PrintLogEvent.Publish(this, $"result:{result}");
            }
            catch (Exception ex)
            {
                HushengSession.PrintLogEvent.Publish(this, $"预约异常{ex.Message}");
            }

        }
    }
}
