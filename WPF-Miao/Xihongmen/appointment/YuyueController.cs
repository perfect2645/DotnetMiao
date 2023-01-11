using Base.viewmodel.status;
using HttpProcessor.Client;
using HttpProcessor.Content;
using System;
using System.Net.Http;
using System.Text.Json;
using Utils.stringBuilder;
using Xihongmen.session;

namespace Xihongmen.appointment
{
    internal class YuyueController : HttpClientBase
    {
        private bool IsHeaderBuilt { get; set; }
        public YuyueController(HttpClient httpClient) : base(httpClient)
        {
        }

        public bool YuyueAsync(Order order)
        {
            MainSession.PrintLogEvent.Publish(null, $"开始预约：{order.ToLogString()}");
            var content = new YuyueContent(order);
            return Yuyue(content);
        }

        internal bool Yuyue(YuyueContent content)
        {
            try
            {
                if (!IsHeaderBuilt)
                {
                    content.BuildDefaultHeaders(Client);
                    IsHeaderBuilt = true;
                }
                HttpDicResponse response = PostStringAsync(content, ContentType.Json, false).Result;
                if (response?.JsonBody?.RootElement == null)
                {
                    Log($"没查到苗{response?.Message}");
                    return false;
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
                        return false;
                    }
                }

                if ("OK".Equals(msg, StringComparison.OrdinalIgnoreCase))
                {
                    var data = root.GetProperty("data");
                    AnalysisResult(data, content.Order);
                    return true;
                }
                else
                {
                    Log($"预约失败 - {msg}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"预约异常{ex.Message}");
                return false;
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
    }
}
