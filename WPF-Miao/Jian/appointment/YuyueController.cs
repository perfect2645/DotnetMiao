using HttpProcessor.Client;
using HttpProcessor.Content;
using Jian.session;
using System;
using System.Net.Http;
using System.Threading;
using Utils.stringBuilder;
using System.Text.Json;
using System.Collections.Generic;

namespace Jian.appointment
{
    public class YuyueController : HttpClientBase
    {
        public bool IsSuccess { get; set; }
        private bool IsHeaderBuilt { get; set; }
        public YuyueController(HttpClient httpClient) : base(httpClient)
        {
        }

        public bool YuyueAsync(Order order)
        {
            if (IsSuccess)
            {
                return IsSuccess;
            }

            MainSession.PrintLogEvent.Publish(null, $"开始预约：{order.ToLogString()}");
            var content = new YuyueContent(order, order.User);
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
                HttpDicResponse response = PostStringAsync(content, ContentType.Json, false).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"Yuyue - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("resultCode").GetInt16();
                var success = root.GetProperty("success").GetBoolean();
                var message = root.GetProperty("message").GetString();
                if (code != 20000 || !success)
                {
                    MainSession.PrintLogEvent.Publish(this, $"预约失败: code={code}, message={message}");
                    return false;
                }

                var data = root.GetProperty("data");
                if (data.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"预约失败: results is empty");
                    return false;
                }

                MainSession.PrintLogEvent.Publish(this, $"预约成功: message = {message}");

                CheckOrder(data, content.Order);

                return true;
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"预约异常{ex.Message}");
                return false;
            }
        }

        private void CheckOrder(JsonElement bookingResult, Order order)
        {
            var orderId = bookingResult.GetString();

            if (!string.IsNullOrEmpty(orderId))
            {
                order.OrderId = orderId;
            }

            MainSession.PrintLogEvent.Publish(this, order.ToLogString());
        }
    }
}
