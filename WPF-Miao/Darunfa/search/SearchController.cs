using Darunfa.session;
using Darunfa.submit;
using HttpProcessor.Client;
using HttpProcessor.Container;
using HttpProcessor.Content;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils.datetime;
using Utils.json;
using Utils.timerUtil;

namespace Darunfa.search
{
    internal class SearchController : HttpClientBase
    {
        private IntervalOnTime SearchInterval { get; set; }
        private bool isGetMiao = false;
        private bool isGetYzm = false;

        public SearchController(HttpClient httpClient) : base(httpClient)
        {
            //var startTime = MainSession.MiaoSession["StartTime"] as DateTime?;
            //SearchInterval = new IntervalOnTime(async () => await SearchAsync(), "SearchInterval", startTime ?? DateTime.Now, 2000);
        }

        public async Task SearchAsync()
        {
            isGetMiao = await Task.Factory.StartNew(() => Search());
            if (!isGetMiao)
            {
                return;
            }
            SearchInterval.StopInterval();

            await Yuyue();
        }

        private async Task Yuyue()
        {
            if (!isGetYzm)
            {
                isGetYzm = true;
                await GetYzmAsync();
            }

            var appointController = HttpServiceController.GetService<SubmitController>();
            appointController.AppointAsync();
        }

        private async Task GetYzmAsync()
        {
            //var yzmController = HttpServiceController.GetService<YzmController>();
            //await yzmController.GetYzmAsync();
        }

        public bool Search()
        {
            var url = "http://app.whkfqws.com/wx-mobile/Reservations/vaccinavaccina_DateCount.do";

            var content = new SearchContent(url);
            content.AddHeader("Cookie", MainSession.Cookie);

            content.BuildDefaultHeaders(Client);

            try
            {
                HttpDicResponse response = PostStringAsync(content, ContentType.String).Result;
                if (response == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"Search - response == null");
                    return false;
                }

                var result = response.JsonBody.RootElement.GetProperty("doccustom");
                if (result.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"未查到苗");
                    return false;
                }
                AnalysisResult(result);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到苗 - {ex.Message} - {ex.StackTrace}");
                return false;
            }

            return true;
        }

        private void AnalysisResult(JsonElement jsonElement)
        {
            var dicResult = JsonAnalysis.JsonToDic(jsonElement);

            //MainSession.MiaoSession.AddOrUpdate(dicResult);
            MainSession.PrintLogEvent.Publish(this, dicResult, $"查到苗");
        }
    }
}
