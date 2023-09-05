using HttpProcessor.Client;
using HttpProcessor.Content;
using SixWater.session;
using System;
using System.Net.Http;
using System.Text.Json;
using Utils;
using Utils.json;
using Utils.stringBuilder;

namespace SixWater.appointment
{
    public class PaymentController : HttpClientBase
    {
        public PaymentController(HttpClient httpClient) : base(httpClient)
        {
        }

        public bool PaymentAsync(Order order)
        {
            MainSession.PrintLogEvent.Publish(null, $"开始预约：{order.ToLogString()}");
            var content = new PaymentContent(order);
            content.BuildDefaultHeaders(Client);
            return Payment(content);
        }

        internal bool Payment(PaymentContent content)
        {
            try
            {
                HttpDicResponse response = PostStringAsync(content).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"Payment - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("code").GetInt32();
                var msg = root.GetProperty("message").GetString();
                if (code != 0)
                {
                    MainSession.PrintLogEvent.Publish(this, $"Payment: code={code}, message={msg}");
                    return false;
                }

                var data = root.GetProperty("data");
                if (data.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"Payment失败: data is empty");
                    return false;
                }
                MainSession.PrintLogEvent.Publish(this, $"msg={msg}");
                return true;
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"预约异常{ex.Message}");
                return false;
            }
        }

        private bool SaveOrderResult(JsonElement data)
        {
            try
            {
                var orderData = JsonAnalysis.JsonToDic(data);

                MainSession.PrintLogEvent.Publish(this, orderData);

                if (!string.IsNullOrEmpty(orderData.GetString("tranNo")))
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"解析预约结果异常{ex.Message}");

                return false;
            }
        }
    }
}
