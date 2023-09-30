using HttpProcessor.Client;
using HttpProcessor.Content;
using Huaxi.session;
using System;
using System.Net.Http;
using System.Text.Json;
using Utils;
using Utils.json;
using Utils.stringBuilder;

namespace Huaxi.appointment
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
                HttpDicResponse response = PostStringAsync(content, ContentType.String, false).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"Yuyue - {response?.Message},请检查参数");
                    return false;
                }
                var root = response.JsonBody.RootElement;

                var code = root.GetProperty("errorCode").GetString();
                var msg = root.GetProperty("msg").GetString();
                if (code != "0000")
                {
                    MainSession.PrintLogEvent.Publish(this, $"预约失败: code={code}, msg={msg}");
                    return false;
                }

                var data = root.GetProperty("data");
                if (data.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"预约失败: results is empty");
                    return false;
                }
                MainSession.PrintLogEvent.Publish(this, $"msg={msg}");
                return true;
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"预约异常{ex.Message}");
                return false;
            }
        }

        private bool SaveOrderResult(JsonElement data)
        {
            try
            {
                var orderData = JsonAnalysis.JsonToDic(data);

                MainSession.PrintLogEvent.Publish(this, orderData);

                if (!string.IsNullOrEmpty(orderData.GetString("tranNo")))
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"解析预约结果异常{ex.Message}");
                return false;
            }
        }
    }
}
