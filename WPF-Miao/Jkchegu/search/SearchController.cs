using HttpProcessor.Client;
using HttpProcessor.Container;
using HttpProcessor.Content;
using Jkchegu.appointment;
using Jkchegu.session;
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

        public async Task SearchAsync()
        {
            var isGetMiao = await Task.Factory.StartNew(() => Search());
            if (!isGetMiao)
            {
                return;
            }

            var appointController = HttpServiceController.GetService<AppointController>();
            appointController.AppointAsync();
        }

        private bool Search()
        {
            var url = "http://app.whkfqws.com/wx-mobile/Reservations/vaccinavaccina_DateCount.do";

            var content = new SearchContent(url);
            content.AddHeader("Cookie", JkSession.Cookie);

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
                return false;
            }
            AnalizeResult(result);

            return true;
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
