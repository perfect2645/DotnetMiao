using HtmlAgilityPack;
using HttpProcessor.Client;
using HttpProcessor.ExceptionManager;
using HttpProcessor.HtmlAnalysis;
using HttpProcessor.Response;
using hys020.session;
using System;
using System.Net.Http;
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
            var wechatId = MainSession.PlatformSession[Constants.WechatId].NotNullString();
            var openId = MainSession.PlatformSession[Constants.OpenId].NotNullString();
            var timestamp = MainSession.PlatformSession[Constants.TimeStamp].NotNullString();
            var url = $"http://www.hys020.com/home/reserveMsgMobile_{order.OrgId}_{order.AttId}_1_0?regPhase={order.TimeRangeEncode}&wechatid={wechatId}&openid={openId}&Timestamp={timestamp}";

            try
            {
                HtmlResponse response = SearchHtml(url).Result;
                if (response == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"PreviewAppoint - response == null");
                    return false;
                }

                return AnalysisResult(response.Body, order);
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

        private bool AnalysisResult(HtmlDoc body, Order order)
        {
            try
            {
                var patBindId = GetPatBindId(body);
                order.PatBindId = patBindId;
                return true;
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"AnalysisResult异常 - {ex.Message}");
                return false;
            }
        }

        private string GetPatBindId(HtmlDoc body)
        {
            var xpath = "//*[@id=\"txtPatBindId\"]";
            var tarNode = body.GetDefaultNode(xpath);
            var patBindId = body.GetFormValue(tarNode);
            return patBindId;
        }
    }
}
