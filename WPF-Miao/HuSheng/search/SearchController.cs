using HttpProcessor.Client;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
using HttpProcessor.HtmlAnalysis;
using HttpProcessor.Response;
using HuSheng.appointment;
using HuSheng.session;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Utils.timerUtil;

namespace HuSheng.search
{
    internal class SearchController : HttpClientBase
    {
        private IntervalOnTime SearchInterval { get; set; }
        private bool isGetMiao = false;

        public SearchController(HttpClient httpClient) : base(httpClient)
        {
            var startTime = HushengSession.MiaoSession["StartTime"] as DateTime?;
            //SearchInterval = new IntervalOnTime(async () => await SearchIntervalAsync(), "SearchInterval", startTime ?? DateTime.Now, 2000);
        }

        public async void SearchAsync()
        {
            isGetMiao = await Task.Factory.StartNew(() => Search());
            if (!isGetMiao)
            {
                return;
            }
        }

        public async Task SearchIntervalAsync()
        {
            isGetMiao = await Task.Factory.StartNew(() => Search());
            if (!isGetMiao)
            {
                return;
            }
            SearchInterval.StopInterval();

            //await Yuyue();
        }

        private async Task Yuyue()
        {
            var appointController = HttpServiceController.GetService<AppointController>();
            appointController.AppointAsync();
        }

        private bool Search()
        {
            var url = "http://hoosn.cn/newyy/listVaccPlan?subId=8D4AAA5FA2C04B8E971C89FCA2A4D4F4&openid=oSfkt5jTELgDNfJnxR_HjyF5Ardo&appointmentDate=2022-10-10&appointmentTime=15:01~15:30&appointmentTimeId=2068&configValue=1&schemeId=2158";

            try
            {
                HtmlResponse response = SearchHtml(url).Result;
                if (response == null)
                {
                    HushengSession.PrintLogEvent.Publish(this, $"Search - response == null");
                    return false;
                }

                AnalizeResult(response.Body);
            }
            catch (HttpException ex)
            {
                HushengSession.PrintLogEvent.Publish(this, $"查苗异常 - {ex.Message} - {ex.StackTrace}");
                return false;
            }
            catch (Exception ex)
            {
                HushengSession.PrintLogEvent.Publish(this, $"查苗异常 - {ex.Message} - {ex.StackTrace}");
                return false;
            }

            return true;
        }

        private void AnalizeResult(HtmlDoc body)
        {
            var vassNameNodes = body.SearchNodes("//*[@class='vassName']/text()");
            if (vassNameNodes == null)
            {
                HushengSession.PrintLogEvent.Publish(this, $"未查到苗 - no vassNames");
                return;
            }

            var vassNames = vassNameNodes.Select(x => x.InnerText);
            foreach (var name in vassNames)
            {
                HushengSession.PrintLogEvent.Publish(this, $"查到苗 - {name}");
            }
        }
    }
}
