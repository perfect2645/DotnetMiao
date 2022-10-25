using Base.model;
using HttpProcessor.Client;
using HttpProcessor.Container;
using HttpProcessor.Content;
using Jkchegu.appointment;
using Jkchegu.search.yzm;
using Jkchegu.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Utils;
using Utils.json;
using Utils.stringBuilder;
using Utils.timerUtil;

namespace Jkchegu.search
{
    internal class SearchController : HttpClientBase
    {
        private List<SearchProcessor> SearchProcessorList = new List<SearchProcessor>();

        private readonly object OrderLock = new object();

        private bool isGetMiao = false;
        private bool isGetYzm = false;

        public SearchController(HttpClient httpClient) : base(httpClient)
        {
            BuildSearchList();
        }

        private void BuildSearchList()
        {
            var startTime = JkSession.MiaoSession["StartTime"] as DateTime?;
            var preSetDateList = JkSession.PlatformSession["PreDateList"] as List<DspVal>;
            foreach(var preSetDate in preSetDateList)
            {
                var date = preSetDate.Value;
                var searchInterval = new IntervalOnTime(async () => await SearchByDateAsync(date), date, startTime ?? DateTime.Now, 200);
                SearchProcessorList.Add(new SearchProcessor
                {
                    Date = date,
                    SearchInterval = searchInterval
                });
                Thread.Sleep(50);
            }
            JkSession.PrintLogEvent.Publish(this, $"程序已启动 开始时间：{startTime}");
        }

        public async Task SearchByDateAsync(string date)
        {
            await Task.Factory.StartNew(() => SearchBydate(date));
        }

        private async Task Yuyue()
        {
            if (!isGetYzm)
            {
                isGetYzm = true;
                await GetYzmAsync();
            }

            var appointController = HttpServiceController.GetService<AppointController>();
            appointController.AppointAsync();
        }

        private async Task GetYzmAsync()
        {
            var yzmController = HttpServiceController.GetService<YzmController>();
            await yzmController.GetYzmAsync();
        }

        public void SearchBydate(string date)
        {
            var url = "http://app.whkfqws.com/wx-mobile/Reservations/vaccinavaccina_DateCount.do";

            var content = new SearchContent(url);
            content.AddHeader("Cookie", JkSession.Cookie);
            content.BuildDefaultHeaders(Client);
            content.AddContent("yyDate", date);

            try
            {
                HttpDicResponse response = PostStringAsync(content, ContentType.String).Result;
                if (response == null)
                {
                    JkSession.PrintLogEvent.Publish(this, $"Search - response == null");
                    return;
                }

                var result = response.JsonBody.RootElement;
                var hasMiao = response.JsonBody.RootElement.GetProperty("doccustom");
                if (hasMiao.ValueKind == JsonValueKind.Null)
                {
                    return;
                }
                StopInterval(date);
                AnalysisResult(result, date);
            }
            catch (Exception ex)
            {
                JkSession.PrintLogEvent.Publish(this, $"未查到苗 - {ex.Message} - {ex.StackTrace}");
            }
        }

        private void StopInterval(string date)
        {
            foreach(var interval in SearchProcessorList)
            {
                interval[date].StopInterval();
            }
        }

        private void AnalysisResult(JsonElement jsonElement, string date)
        {
            lock (OrderLock)
            {
                if (JkSession.MiaoSession.ContainsKey(date) && JkSession.MiaoSession[date] != null)
                {
                    return;
                }
                lock (OrderLock)
                {
                    SaveMiaoInfo(jsonElement, date);
                }
            }
        }

        private void SaveMiaoInfo(JsonElement jsonElement, string date)
        {
            var pdList = jsonElement.GetProperty("pdList");
            var dateDic = JsonAnalysis.JsonToDicList(pdList);
            var dateList = dateDic.Select(x => x.Values.FirstOrDefault()).ToList();

            if (!JkSession.PlatformSession.ContainsKey("DateList"))
            {
                JkSession.PlatformSession.AddOrUpdate("DateList", dateList);
            }

            if (!dateList.Contains(date))
            {
                return;
            }

            var doccustom = jsonElement.GetProperty("doccustom");
            var allTime = JsonAnalysis.JsonToDic(doccustom);
            var availableTime = allTime.Where(pair => 
                pair.Key.StartsWith("DATE", StringComparison.OrdinalIgnoreCase) 
                && pair.Value.NotNullString() != "0").ToList();
            JkSession.MiaoSession.AddOrUpdate(date, availableTime);

            JkSession.PrintLogEvent.Publish(this, $"查到苗{date}");

        }
    }
}
