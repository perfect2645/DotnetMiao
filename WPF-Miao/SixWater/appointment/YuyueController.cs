using HttpProcessor.Client;
using HttpProcessor.Container;
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
    public class YuyueController : HttpClientBase
    {
        public bool IsSuccess { get; set; }
        private bool IsHeaderBuilt { get; set; }
        public YuyueController(HttpClient httpClient) : base(httpClient)
        {
            IsSuccess = false;
        }

        public bool YuyueAsync(Order order)
        {
            if (IsSuccess)
            {
                return IsSuccess;
            }

            MainSession.PrintLogEvent.Publish(null, $"开始预约：{order.ToLogString()}");
            var content = new YuyueContent(order);
            return Yuyue(content);
        }

        internal bool Yuyue(YuyueContent content)
        {
            try
            {
                if (IsSuccess)
                {
                    return IsSuccess;
                }
                
                if (!IsHeaderBuilt)
                {
                    content.BuildDefaultHeaders(Client);
                    IsHeaderBuilt = true;
                }
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
                    MainSession.PrintLogEvent.Publish(this, $"预约失败: code={code}, msg={msg}");
                    return false;
                }

                var data = root.GetProperty("data");
                if (data.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"预约失败: results is empty");
                    return false;
                }
                MainSession.PrintLogEvent.Publish(this, $"msg={msg}");
                return CheckPayment(data, content.Order);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"预约异常{ex.Message}");
                return false;
            }
        }

        private bool CheckPayment(JsonElement data, Order order)
        {
            try
            {
                var orderData = JsonAnalysis.JsonToDic(data);

                var orderId = orderData.GetString("id");

                if (string.IsNullOrEmpty(orderId))
                {
                    return false;
                }

                order.OrderId = orderId;

                MainSession.PrintLogEvent.Publish(null, $"开始payment：{order.ToLogString()}");
                var paymentController = HttpServiceController.GetService<PaymentController>();

                var isSuccess = paymentController.PaymentAsync(order);

                return isSuccess;
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"解析预约结果异常{ex.Message}");

                return false;
            }
        }
    }
}
