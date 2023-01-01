using HttpProcessor.Client;
using HttpProcessor.Content;
using Longchi.session;
using System;
using System.Net.Http;
using Utils.stringBuilder;

namespace Longchi.appointment
{
    internal class VerifyYuyueController : HttpClientBase
    {
        private bool IsHeaderBuilt { get; set; }
        public VerifyYuyueController(HttpClient httpClient) : base(httpClient)
        {
        }

        public bool VerifyYuyueAsync(Order order)
        {
            MainSession.PrintLogEvent.Publish(null, $"开始验证预约：{order.ReturnId}");
            var content = new VerifyYuyueContent(order);
            return VerifyYuyue(content);
        }

        internal bool VerifyYuyue(VerifyYuyueContent content)
        {
            try
            {
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
                    MainSession.PrintLogEvent.Publish(this, $"验证预约失败:code={code}, msg={msg}");
                    return false;
                }

                var order_id = root.GetProperty("order_id").NotNullString();
                if (string.IsNullOrEmpty(order_id) || "00".Equals(order_id))
                {
                    MainSession.PrintLogEvent.Publish(this, $"验证预约失败:{msg}");
                    return false;
                }
                content.Order.OrderId = order_id;

                MainSession.PrintLogEvent.Publish(this, $"验证预约结果:{msg}");

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
