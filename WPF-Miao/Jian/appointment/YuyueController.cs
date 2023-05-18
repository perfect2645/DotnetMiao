using HttpProcessor.Client;
using HttpProcessor.Content;
using Jian.session;
using System;
using System.Net.Http;
using System.Threading;
using Utils.stringBuilder;
using System.Text.Json;

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
                HttpDicResponse response = PostStringAsync(content, ContentType.String, false).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetUser - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                var msg = root.GetProperty("msg").GetString();
                if (msg.Contains("不能重复提交") || msg.Contains("匹配不到对应的号源信息"))
                {
                    MainSession.PrintLogEvent.Publish(this, $"预约成功: msg = {msg}");
                    return true;
                }

                var code = root.GetProperty("code").GetInt16();
                if (code != 0)
                {
                    MainSession.PrintLogEvent.Publish(this, $"查苗失败: code={code}");
                    return false;
                }


                MainSession.PrintLogEvent.Publish(this, $"预约成功: msg = {msg}");

                var data = root.GetProperty("data");
                if (data.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"查苗失败: results is empty");
                    return false;
                }


                var bookingResult = root.GetProperty("bookingResult");

                CheckOrder(bookingResult, content.Order);

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
            var bookingId = bookingResult.GetProperty("bookingID").GetString();

            if (!string.IsNullOrEmpty(bookingId))
            {
                //order.BookingID = bookingId;
            }

            MainSession.PrintLogEvent.Publish(this, order.ToLogString());
        }
    }
}
