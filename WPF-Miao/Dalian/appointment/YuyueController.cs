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
                var root = response.JsonBody.RootElement;

                var status = root.GetProperty("status").GetInt32();
                var msg = root.GetProperty("msg").GetString();
                if (msg == "存在未支付的订单") 
                {
                    MainSession.PrintLogEvent.Publish(this, $"预约成功: 存在未支付的订单");
                    return true;
                }

                if (status != 0)
                {
                    MainSession.PrintLogEvent.Publish(this, $"预约失败: status={status}, msg={msg}");
                    return false;
                }

                var data = root.GetProperty("data");
                if (data.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"预约失败: results is empty");
                    return false;
                }

                MainSession.PrintLogEvent.Publish(this, $"{content.User.UserName} : 预约成功");

                return CheckOrder(data, content.Order);
            }
            catch (Exception ex)
            {
                Logging.GLog.Logger.Error(ex);
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
