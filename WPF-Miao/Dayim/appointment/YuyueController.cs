using Dayim.session;
using HttpProcessor.Client;
using HttpProcessor.Content;
using System;
using System.Net.Http;
using System.Text.Json;

namespace Dayim.appointment
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

                var message = root.GetProperty("message").GetString();
                if (message.Contains("重复预约"))
                {
                    MainSession.PrintLogEvent.Publish(this, $"预约成功: msg = {message}");
                    return true;
                }

                var code = root.GetProperty("code").GetInt32();
                if (code != 200)
                {
                    MainSession.PrintLogEvent.Publish(this, $"预约失败: code={code}");
                    return false;
                }

                var result = root.GetProperty("result");
                if (result.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"预约失败: results is empty");
                    return false;
                }

                MainSession.PrintLogEvent.Publish(this, $"{content.User.UserName} : 预约成功: msg = {message}");

                return CheckOrder(result, content.Order);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"预约异常{ex.Message}");
                return false;
            }
        }

        private bool CheckOrder(JsonElement bookingResult, Order order)
        {
            var address = bookingResult.GetProperty("address").GetString();

            if (string.IsNullOrEmpty(address))
            {
                return false;
            }

            order.Address = address;
            MainSession.PrintLogEvent.Publish(this, order.ToLogString());

            return true;
        }
    }
}
