using HttpProcessor.Client;
using HttpProcessor.ExceptionManager;
using HttpProcessor.HtmlAnalysis;
using HttpProcessor.Response;
using hys020.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Utils;
using Utils.stringBuilder;

namespace hys020.appointment
{
    internal class PreviewAppointController : HttpClientBase
    {
        public PreviewAppointController(HttpClient httpClient) : base(httpClient)
        {
        }

        public bool PreviewAppoint(Order order)
        {
            var wechatId = MainSession.PlatformSession[Constants.Location].NotNullString();
            var url = $"http://www.hys020.com/home/reserveMsgMobile_{order.OrgId}_{order.AttId}_1_0?regPhase={order.TimeRangeEncode}&wechatid={wechatId}&openid=o1_Liw34_q5dnOFrOCRDK7jQn5E0&Timestamp=iS3Q9CBrbexu5o3vYhWcnGf9Qn0wciu+";

            try
            {
                HtmlResponse response = SearchHtml(url).Result;
                if (response == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"PreviewAppoint - response == null");
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
                //var has9 = HasNineJiaFirstDose(body);
                //if (!has9)
                //{
                //    return false;
                //}

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
    }
}
