using HttpProcessor.Client;
using HttpProcessor.Container;
using HttpProcessor.Content;
using Longchi.session;
using System;
using System.Net.Http;
using Utils.stringBuilder;

namespace Longchi.appointment
{
    public class YuyueController : HttpClientBase
    {
        public bool IsSuccess { get; set; }
        private bool IsHeaderBuilt { get; set; }
        internal YuyueContent Content { get; set; }

        private readonly TokenController TokenController;

        public string Key { get; set; }

        public YuyueController(HttpClient httpClient) : base(httpClient)
        {
            TokenController = HttpServiceController.GetService<TokenController>();
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
                HttpDicResponse response = PostStringAsync(content, ContentType.String).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"Appoint failed - {response?.Message},请检查参数");
                    return false;
                }

                var root = response.JsonBody.RootElement;
                var code = root.GetProperty("code").NotNullString();
                var msg = root.GetProperty("msg").NotNullString();
                if (code != "1")
                {
                    MainSession.PrintLogEvent.Publish(this, $"预约失败:{msg}");
                    return false;
                }
                IsSuccess = true;
                MainSession.PrintLogEvent.Publish(this, $"预约结果:{msg}");
                MainSession.PrintLogEvent.Publish(null, $"{content.Order.ToLogString()}");

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
