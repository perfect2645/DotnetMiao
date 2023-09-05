using HttpProcessor.Client;
using HttpProcessor.Content;
using Kuerle.session;
using System;
using System.Net.Http;
using System.Text.Json;
using Utils;
using Utils.json;
using Utils.stringBuilder;

namespace Kuerle.appointment
{
    public class YuyueController : HttpClientBase
    {
        public bool IsSuccess { get; set; }
        private bool IsHeaderBuilt { get; set; }
        public YuyueController(HttpClient httpClient) : base(httpClient)
        {
            IsSuccess = false;
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
                HttpDicResponse response = PostStringAsync(content, ContentType.Json, false).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"Yuyue - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("ErrCode").GetInt32();
                if (code == 0)
                {
                    MainSession.PrintLogEvent.Publish(this, $"预约成功: code={code}, user={content.User.UserName}");
                    return true;
                }
                var msg = root.GetProperty("Message").GetString();
                MainSession.PrintLogEvent.Publish(this, $"预约结果: code={code}, msg={msg}");
                if (code == 106 && msg.Contains("您已预约"))
                {
                    MainSession.PrintLogEvent.Publish(this, $"预约成功: code={code}, msg={msg}");
                    content.Order.ResultMsg = msg;
                    return true;
                }
                MainSession.PrintLogEvent.Publish(this, $"预约结果: code={code}, msg={msg}");
                if (code == 106 && msg.Contains("现已无余量"))
                {
                    MainSession.PrintLogEvent.Publish(this, $"预约失败: code={code}, msg={msg}");
                    content.Order.ResultMsg = msg;
                    return true;
                }
                if (code != 0)
                {
                    MainSession.PrintLogEvent.Publish(this, $"预约失败: code={code}, msg={msg}");
                    content.Order.ResultMsg = msg;
                    return false;
                }

                return false;
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"预约异常{ex.Message}");
                return false;
            }
        }
    }
}
