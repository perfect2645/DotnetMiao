using Base.viewmodel.status;
using HttpProcessor.Client;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
using HttpProcessor.HtmlAnalysis;
using HttpProcessor.Response;
using HuSheng.analysis;
using HuSheng.appointment;
using HuSheng.session;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Utils;
using Utils.timerUtil;

namespace HuSheng.search
{
    internal class VaccPlanController : HttpClientBase
    {
        private IntervalOnTime SearchInterval { get; set; }
        private bool isGetMiao = false;

        public VaccPlanController(HttpClient httpClient) : base(httpClient)
        {
        }

        public async void SearchAsync()
        {
            try
            {
                isGetMiao = await Task.Factory.StartNew(() => Search());
                if (!isGetMiao)
                {
                    return;
                }
            }
            finally
            {
                MainSession.SetStatus(MiaoProgress.Searchend);
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
            //var url = "http://hoosn.cn/newyy/listVaccPlan?subId=8D4AAA5FA2C04B8E971C89FCA2A4D4F4&openid=oSfkt5jTELgDNfJnxR_HjyF5Ardo&appointmentDate=2022-10-10&appointmentTime=15:01~15:30&appointmentTimeId=2068&configValue=1&schemeId=2159";


            var url = "http://hoosn.cn/newyy/listVaccPlan?subId=8D4AAA5FA2C04B8E971C89FCA2A4D4F4&openid=oSfkt5jTELgDNfJnxR_HjyF5Ardo&appointmentDate=2022-11-14&appointmentTime=08:00~08:30&appointmentTimeId=2057&configValue=1&schemeId=2159";

            try
            {
                HtmlResponse response = SearchHtml(url).Result;
                if (response == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"Search - response == null");
                    return false;
                }

                return AnalysisResult(response.Body);
            }
            catch (HttpException ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"查苗异常 - {ex.Message} - {ex.StackTrace}");
                return false;
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"查苗异常 - {ex.Message} - {ex.StackTrace}");
                return false;
            }
        }

        private bool AnalysisResult(HtmlDoc body)
        {
            try
            {
                var has9 = HasNineJiaFirstDose(body);
                if (!has9)
                {
                    return false;
                }

                var miaohtml = GetForm(body);
                MainSession.PlatformSession.AddOrUpdate(Constants.MiaoHtml, miaohtml);

                return true;
            }
            catch(Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"AnalysisResult异常 - {ex.Message}");
                return false;
            }
        }

        private HtmlDoc GetForm(HtmlDoc body)
        {
            var xpath = "//*[@id=\"saveForm\"]";
            return body.GetInnerDoc(xpath);
        }

        private bool HasNineJiaFirstDose(HtmlDoc body)
        {
            var vassNameNodes = body.SearchNodes("//*[@class='vassName']/text()");
            if (vassNameNodes == null)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到苗 - no vassNames");
                return false;
            }

            var vassNames = vassNameNodes.Select(x => x.InnerText);
            foreach (var name in vassNames)
            {
                MainSession.PrintLogEvent.Publish(this, $"查到苗 - {name}");
                if (name.IsNine() && name.IsDoseOne())
                {
                    return true;
                }
            }

            return false;
        }
    }
}
