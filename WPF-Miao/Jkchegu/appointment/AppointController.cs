using HttpProcessor.Client;
using HttpProcessor.Content;
using Jkchegu.session;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils.datetime;

namespace Jkchegu.appointment
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
                var content = new AppointContent(url);
                content.AddHeader("Cookie", JkSession.Cookie);

                content.BuildDefaultHeaders(Client);

                HttpDicResponse response = PostStringAsync(content, ContentType.String).Result;
                if (response == null)
                {
                    JkSession.PrintLogEvent.Publish(this, $"Appoint response is null");
                }

                JkSession.PrintLogEvent.Publish(this, $"预约完成-{DateTimeUtil.GetNow()}");
                var result = response.JsonBody.RootElement.GetProperty("res").ToString();
                if (string.IsNullOrEmpty(result))
                {
                    JkSession.PrintLogEvent.Publish(this, $"result:预约申请提交成功");
                    return;
                }
                JkSession.PrintLogEvent.Publish(this, $"result:{result}");
            }
            catch(Exception ex)
            {
                JkSession.PrintLogEvent.Publish(this, $"预约异常{ex.Message}-{DateTimeUtil.GetNow()}");
            }
            
        }
    }
}
