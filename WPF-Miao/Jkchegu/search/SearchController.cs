using HttpProcessor.Client;
using HttpProcessor.Content;
using HttpProcessor.ExceptionManager;
using Jkchegu.session;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.datetime;
using Utils.json;

namespace Jkchegu.search
{
    internal class SearchController : HttpClientBase
    {
        public SearchController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void SearchAsync()
        {
            Task.Factory.StartNew(() => Search());
        }

        private void Search()
        {
            var url = "http://app.whkfqws.com/wx-mobile/Reservations/vaccinavaccina_DateCount.do";

            var content = new SearchContent(url);
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
                return;
            }
            AnalizeResult(result);
        }

        private void AnalizeResult(JsonElement jsonElement)
        {
            var dicResult = JsonAnalysis.JsonToDic(jsonElement);

            JkSession.ClearMiaoSession();
            JkSession.MiaoSession.AddOrUpdate(dicResult);
            JkSession.PrintLogEvent.Publish(this, dicResult, $"查到苗 - {DateTimeUtil.GetNow()}");
        }
    }
}
