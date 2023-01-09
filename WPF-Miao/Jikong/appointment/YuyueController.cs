using HttpProcessor.Client;
using HttpProcessor.Content;
using Jikong.session;
using System;
using System.Net.Http;
using System.Threading;
using Utils.stringBuilder;
using System.Text.Json;

namespace Jikong.appointment
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
                    MainSession.PrintLogEvent.Publish(this, $"Appoint failed - {response?.Message},请检查参数");
                    return false;
                }

                var root = response.JsonBody.RootElement;
                JsonElement error;
                var isError = root.TryGetProperty("error", out error);
                if (isError)
                {
                    var errMsg = error.NotNullString();
                    MainSession.PrintLogEvent.Publish(this, $"预约失败:error {errMsg}");
                    if (errMsg.Contains("总预约次数已满"))
                    {
                        return true;
                    }
                    return false;
                }
                var msg = root.GetProperty("message").NotNullString();
                if (msg != "预约成功")
                {
                    MainSession.PrintLogEvent.Publish(this, $"预约失败:message: {msg}");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"预约异常{ex.Message}");
                return false;
            }
        }
    }
}
