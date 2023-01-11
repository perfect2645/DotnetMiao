using Base.viewmodel.status;
using Xihongmen.session;
using HttpProcessor.Client;
using HttpProcessor.Content;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Utils.stringBuilder;
using System.Net.Http.Headers;

namespace Xihongmen.appointment
{
    internal class YuyueController : HttpClientBase
    {
        private int count = 0;
        public YuyueController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void BuildHeaders(YuyueContent content)
        {
            BuildClientHeaders(content);
        }

        public void AppointAsync(YuyueContent content)
        {
            Task.Factory.StartNew(() =>
            {
                Appoint(content);
            });
        }

        public void Appoint(YuyueContent content)
        {
            try
            {
                if (MainSession.GetStatus() == MiaoProgress.AppointEnd)
                {
                    content.Order.IntervalOnTime.StopInterval();
                    return;
                }
                MainSession.PrintLogEvent.Publish(this, content.Order.ToLogString());

                HttpDicResponse response = PostStringAsync(content, ContentType.String, false).Result;
                if (response?.JsonBody?.RootElement == null)
                {
                    Log($"没查到苗{response?.Message}");
                    return;
                }

                var root = response.JsonBody.RootElement;
                var msg = root.GetProperty("msg").NotNullString();
                if (msg.Contains("请重试"))
                {
                    object cookie = string.Empty;
                    if (response.Headers.TryGetValue("Set-Cookie", out cookie!))
                    {
                        content.Order.User.Cookie = cookie.NotNullString();
                        MainSession.PrintLogEvent.Publish(this, $"Cookie get for {content.Order.User.UserName}: {content.Order.User.Cookie}");
                        return;
                    }
                }

                if (MainSession.GetStatus() == MiaoProgress.AppointEnd)
                {
                    content.Order.IntervalOnTime.StopInterval();
                    return;
                }
                if ("OK".Equals(msg, StringComparison.OrdinalIgnoreCase))
                {
                    var data = root.GetProperty("data");
                    AnalysisResult(data, content.Order);
                }
                else
                {
                    Log($"预约失败 - {msg}");
                }
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到苗 - {ex.Message} - {ex.StackTrace}");
            }
        }

        private void AnalysisResult(JsonElement miaoInfo, Order order)
        {
            if (miaoInfo.ValueKind == JsonValueKind.Null)
            {
                return;
            }
            order.IntervalOnTime?.StopInterval();
            MainSession.PrintLogEvent.Publish(this, $"result:预约申请提交成功");
            order.OrderId = miaoInfo.GetProperty("id").NotNullString();
            order.User.OrderId = order.OrderId;
            if (!string.IsNullOrEmpty(order.OrderId))
            {
                MainSession.SetStatus(MiaoProgress.AppointEnd);
            }
            MainSession.PrintLogEvent.Publish(this, order.ToLogString());
        }

        #region 转号

        public void Exchange(YuyueContent contnet)
        {
            Appoint(contnet);
            contnet.Order.IntervalOnTime.StartIntervalOntime(() =>
            {
                Appoint(contnet);
            });
        }

        #endregion 转号
    }
}
