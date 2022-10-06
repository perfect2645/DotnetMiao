using HttpProcessor.Client;
using HttpProcessor.Container;
using HttpProcessor.Content;
using Jkchegu.appointment;
using Jkchegu.session;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.datetime;
using Utils.json;
using Utils.timerUtil;

namespace Jkchegu.search
{
    internal class SearchController : HttpClientBase
    {
        private IntervalOnTime SearchInterval { get; set; }

        public SearchController(HttpClient httpClient) : base(httpClient)
        {
            var startTime = JkSession.MiaoSession["StartTime"] as DateTime?;
            SearchInterval = new IntervalOnTime(async () => await SearchAsync(), "SearchInterval", startTime ?? DateTime.Now, 5000);
        }

        private async Task SearchAsync()
        {
            var isGetMiao = await Task.Factory.StartNew(() => Search());
            if (!isGetMiao)
            {
                return;
            }
            SearchInterval.StopInterval();

            var appointController = HttpServiceController.GetService<AppointController>();
            appointController.AppointAsync();
        }

        public bool Search()
        {
            var url = "http://app.whkfqws.com/wx-mobile/Reservations/vaccinavaccina_DateCount.do";

            var content = new SearchContent(url);
            content.AddHeader("Cookie", JkSession.Cookie);

            content.BuildDefaultHeaders(Client);

            try
            {
                HttpDicResponse response = PostStringAsync(content, ContentType.String).Result;
                if (response == null)
                {
                    JkSession.PrintLogEvent.Publish(this, $"Search - response == null");
                    return false;
                }

                var result = response.JsonBody.RootElement.GetProperty("doccustom");
                if (result.ValueKind == JsonValueKind.Null)
                {
                    JkSession.PrintLogEvent.Publish(this, $"未查到苗 - {DateTimeUtil.GetNow()}");
                    return false;
                }
                AnalizeResult(result);
            }
            catch (Exception ex)
            {
                JkSession.PrintLogEvent.Publish(this, $"未查到苗 - {ex.Message} - {ex.StackTrace}");
                return false;
            }

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
