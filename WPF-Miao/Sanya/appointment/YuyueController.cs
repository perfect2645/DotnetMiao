using Sanya.session;
using HttpProcessor.Client;
using HttpProcessor.Content;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Windows.Interop;

namespace Sanya.appointment
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

                var code = root.GetProperty("code").GetInt32();
                if (code != 0)
                {
                    var message = root.GetProperty("message").GetString();
                    MainSession.PrintLogEvent.Publish(this, $"预约失败: success={code}, msg = {message}");
                    return false;
                }

                var success = root.GetProperty("success").GetBoolean();
                if (!success)
                {
                    var message = root.GetProperty("message").GetString();
                    MainSession.PrintLogEvent.Publish(this, $"预约失败: success={success}, msg = {message}");
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

        private bool CheckOrder(JsonElement data, Order order)
        {
            var dataDouble = data.GetDouble();

            if (dataDouble > 0)
            {
                return true;
            }

            order.ResultMsg = dataDouble.ToString();
            MainSession.PrintLogEvent.Publish(this, order.ToLogString());

            return true;
        }
    }
}
