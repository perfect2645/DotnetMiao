using HttpProcessor.Client;
using HttpProcessor.Content;
using Huangdai.session;
using System;
using System.Net.Http;
using System.Threading;
using Utils.stringBuilder;
using System.Text.Json;
using Utils.json;
using Utils;

namespace Huangdai.appointment
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
                var code = root.GetProperty("code").NotNullString();
                var msg = root.GetProperty("msg").NotNullString();
                if (code != "0" || msg != "操作完成")
                {
                    MainSession.PrintLogEvent.Publish(this, $"预约失败:code={code}, message: {msg}");
                    return false;
                }

                var data = root.GetProperty("data");
                return SaveOrderResult(data);
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
