using HttpProcessor.Client;
using HttpProcessor.ExceptionManager;
using HttpProcessor.HtmlAnalysis;
using HttpProcessor.Response;
using hys020.session;
using System;
using System.Linq;
using System.Net.Http;
using Utils;

namespace hys020.search
{
    internal class ConfirmOrderController : HttpClientBase
    {
        public ConfirmOrderController(HttpClient httpClient) : base(httpClient)
        {
        }

        private bool Search()
        {
            var url = "http://www.hys020.com/home/reserveMsgMobile_63AF0B5D02504957A1173AB9B9C231E0_D4BEB657DDCD46F585DB5E7046996B3B_1_0?regPhase=10:30%20-%2011:00&wechatid=gh_868741944de3&openid=o1_Liw34_q5dnOFrOCRDK7jQn5E0&Timestamp=iS3Q9CBrbexu5o3vYhWcnGf9Qn0wciu+";

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
                MainSession.MiaoSession.AddOrUpdate(Constants.MiaoHtml, miaohtml);

                return true;
            }
            catch (Exception ex)
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
                //if (name.IsNine() && name.IsDoseOne())
                //{
                //    return true;
                //}
            }

            return false;
        }
    }
}
