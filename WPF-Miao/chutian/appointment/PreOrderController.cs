using chutian.session;
using HttpProcessor.Client;
using HttpProcessor.Content;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils.stringBuilder;

namespace chutian.appointment
{
    internal class PreOrderController : HttpClientBase
    {
        private int count = 0;
        public PreOrderController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void BuildHeaders(PreOrderContent content)
        {
            BuildClientHeaders(content);
        }

        public void PreOrderAsync(PreOrderContent content)
        {
            Task.Factory.StartNew(() =>
            {
                PreOrder(content);
            });
        }

        public void PreOrder(PreOrderContent content)
        {
            try
            {
                count++;
                if (count % 100 == 1)
                {
                    Log($"尝试预约第{count}次");
                }

                Log($"开始预约");
                HttpDicResponse response = PostStringAsync(content, ContentType.String).Result;
                if (response?.JsonBody?.RootElement == null)
                {
                    if (response?.Message != null && response.Message.Contains("Response status code does not indicate success: 500 ()."))
                    {
                        Log($"登录过期，重新登录{response?.Message}");
                        content.Order.IntervalOnTime?.StopInterval();
                        MainSession.ReSessionEvent.Publish(this, new ResessionEventArgs());
                        return;
                    }
                    Log($"没查到苗{response?.Message}");
                    return;
                }

                var root = response.JsonBody.RootElement;
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
            order.MiaoId = miaoInfo.GetProperty("id").NotNullString();
            MainSession.PrintLogEvent.Publish(this, order.ToLogString());
        }
    }
}
