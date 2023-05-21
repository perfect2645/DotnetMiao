using Base.viewmodel.status;
using HttpProcessor.Client;
using HttpProcessor.Content;
using suiyang.session;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Utils.stringBuilder;
using Utils.timerUtil;

namespace suiyang.appointment
{
    public class YuyueController : HttpClientBase
    {
        internal YuyueContent Content { get; set; }
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
                HttpDicResponse response = PostStringAsync(content, ContentType.Json).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"Appoint failed - {response?.Message},请检查参数");
                    return false;
                }

                var success = response.JsonBody.RootElement.GetProperty("success").NotNullString().ToBool();
                var message = response.JsonBody.RootElement.GetProperty("message").NotNullString();
                if (!success)
                {
                    MainSession.PrintLogEvent.Publish(this, $"预约失败:{message}");
                    return false;
                }
                MainSession.PrintLogEvent.Publish(this, $"预约结果:{message}");
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
