using HttpProcessor.Client;
using HttpProcessor.Content;
using Jkchegu.session;
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

            var content = new AppointContent(url);
            content.AddHeader("Cookie", "JSESSIONID=A4BD0E1ABA6C5ED6C3ADAB40BD93F7A6");

            content.BuildDefaultHeaders(Client);

            HttpDicResponse response = PostStringAsync(content, ContentType.String).Result;
            if (response == null)
            {
                JkSession.PrintLogEvent.Publish(this, $"登录过期了");
            }

            var result = response.JsonBody.RootElement.GetProperty("doccustom");
            if (result.ValueKind == JsonValueKind.Null)
            {
                JkSession.PrintLogEvent.Publish(this, $"未查到苗 - {DateTimeUtil.GetNow()}");
            }
        }
    }
}
