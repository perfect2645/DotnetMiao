using HttpProcessor.Client;
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
                if (code != "2")
                {
                    MainSession.PrintLogEvent.Publish(this, $"预约失败:{msg}");
                    return false;
                }

                if (msg != "正在预约，请稍后...")
                {
                    MainSession.PrintLogEvent.Publish(this, $"预约失败:{msg}");
                    return false;
                }

                var return_id = root.GetProperty("return_id").NotNullString();
                if (string.IsNullOrEmpty(return_id))
                {
                    MainSession.PrintLogEvent.Publish(this, $"预约失败:{msg}");
                    return false;
                }
                content.Order.ReturnId = return_id;

                VerifyYuyue(content.Order);
                MainSession.PrintLogEvent.Publish(this, $"预约结果:{msg}");
                MainSession.PrintLogEvent.Publish(null, $"{content.Order.ToLogString()}");

                return false;
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"预约异常{ex.Message}");
                return false;
            }
        }

        private bool VerifyYuyue(Order order)
        {
            var verifyController = MainSession.VerifyYuyueSession.GetController($"{order.UserName}|{order.Date}");
            var isValid = verifyController.VerifyYuyueAsync(order);
            return isValid;
        }
    }
}
