using HttpProcessor.Client;
using HttpProcessor.Content;
using Yongding.session;
using System;
using System.Net.Http;
using System.Text.Json;
using Utils;
using Utils.json;
using Utils.stringBuilder;
using Yongding.search;
using HttpProcessor.Container;

namespace Yongding.appointment
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
            MainSession.PrintLogEvent.Publish(null, $"GetTime");
            var timeController = HttpServiceController.GetService<MiaoController>();
            var isTimeGet = timeController.SearchMiao(order);
            if (!isTimeGet)
            {
                return false;
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

                var code = root.GetProperty("code").GetInt32();
                var message = root.GetProperty("message").GetString();
                if (code != 200)
                {
                    MainSession.PrintLogEvent.Publish(this, $"预约失败: code={code}, msg={message}");
                    return false;
                }

                var data = root.GetProperty("data");
                if (data.ValueKind == JsonValueKind.Null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"预约失败: results is empty");
                    return false;
                }
                MainSession.PrintLogEvent.Publish(this, $"msg={message}");
                return SaveOrderResult(data, content.Order);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"预约异常{ex.Message}");
                return false;
            }
        }

        private bool SaveOrderResult(JsonElement data, Order order)
        {
            try
            {
                var orderData = JsonAnalysis.JsonToDic(data);

                MainSession.PrintLogEvent.Publish(this, orderData);

                if (!string.IsNullOrEmpty(orderData.GetString("id")))
                {
                    order.OrderId = orderData.GetString("id");
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
