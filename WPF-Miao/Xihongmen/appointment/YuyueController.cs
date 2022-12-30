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

        public void PreOrderAsync(YuyueContent content)
        {
            Task.Factory.StartNew(() =>
            {
                PreOrder(content);
            });
        }

        public void PreOrder(YuyueContent content)
        {
            try
            {
                count++;

                if (count % 100 == 1)
                {
                    Log($"尝试预约第{count}次");
                }

                Log($"开始预约");
                HttpDicResponse response = PostStringAsync(content, ContentType.String, false).Result;
                if (response?.JsonBody?.RootElement == null)
                {
                    Log($"没查到苗{response?.Message}");
                    return;
                }

                var root = response.JsonBody.RootElement;

                JsonElement messageElement;
                root.TryGetProperty("message", out messageElement);
                var message = messageElement.NotNullString();
                if (message.Contains("JWT expired"))
                {
                    Log($"登录过期，重新登录{response?.Message}");
                    MainSession.ReSessionEvent.Publish(this, new ResessionEventArgs());
                    return;
                }

                JsonElement errorElement;
                root.TryGetProperty("error", out errorElement);
                var error = errorElement.NotNullString();
                if (!string.IsNullOrEmpty(error))
                {
                    Log($"error : {error} - message : {message}");
                    return;
                }
                var code = root.GetProperty("code").NotNullString();
                var msg = root.GetProperty("msg").NotNullString();
                if ("0".Equals(code))
                {
                    var miaoInfo = root.GetProperty("obj");
                    AnalysisResult(miaoInfo, content.Order);
                }
                else
                {
                    Log($"预约结果 - {msg}");
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
            if (!string.IsNullOrEmpty(order.OrderId))
            {
                MainSession.SetStatus(MiaoProgress.AppointEnd);
            }
            MainSession.PrintLogEvent.Publish(this, order.ToLogString());
        }


        #region 转号

        public void Exchange(YuyueContent contnet)
        {
            PreOrder(contnet);
            contnet.Order.IntervalOnTime.StartIntervalOntime(() =>
            {
                PreOrder(contnet);
            });
        }

        #endregion 转号
    }
}
