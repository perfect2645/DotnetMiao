using Dalian.session;
using HttpProcessor.Client;
using HttpProcessor.Content;
using System;
using System.Net.Http;
using System.Text.Json;

namespace Dalian.appointment
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
                    MainSession.PrintLogEvent.Publish(this, $"GetUser - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                var message = root.GetProperty("msg").GetString() ?? string.Empty;
                if (message.Contains("已存在相同订单"))
                {
                    MainSession.PrintLogEvent.Publish(this, $"预约成功: msg = {message}");
                    return true;
                }

                var code = root.GetProperty("resCode").GetInt32();
                if (code != 0)
                {
                    MainSession.PrintLogEvent.Publish(this, $"预约失败: code={code}, msg = {message}");
                    return false;
                }

                var data = root.GetProperty("data");
                if (data.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"预约失败: results is empty");
                    return false;
                }

                MainSession.PrintLogEvent.Publish(this, $"{content.User.UserName} : 预约成功: msg = {message}");

                return CheckOrder(data, content.Order);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"预约异常{ex.Message}");
                return false;
            }
        }

        private bool CheckOrder(JsonElement bookingResult, Order order)
        {
            var orderNo = bookingResult.GetProperty("orderNo").GetString();

            if (string.IsNullOrEmpty(orderNo))
            {
                return false;
            }

            order.OrderNo = orderNo;
            MainSession.PrintLogEvent.Publish(this, order.ToLogString());

            return true;
        }
    }
}
