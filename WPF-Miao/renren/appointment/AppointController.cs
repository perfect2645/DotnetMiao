using HttpProcessor.Client;
using HttpProcessor.Content;
using HttpProcessor.ExceptionManager;
using renren.session;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Utils;
using Utils.json;
using Utils.stringBuilder;

namespace renren.appointment
{
    internal class AppointController : HttpClientBase
    {
        public bool IsSuccess { get; private set; }
        public AppointController(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task AppointAsync(AppointContent content)
        {
            await Task.Factory.StartNew(() => Appoint(content));
        }

        public void Appoint(AppointContent content)
        {
            try
            {
                content.BuildDefaultHeaders(Client);
                content.Order.Count++;
                Log(content.Order.ToLogString());
                HttpDicResponse response = PostStringAsync(content, ContentType.Json).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"Appoint response is null");
                    return;
                }

                var result = response.JsonBody.RootElement;
                var schedule = AnalysisResult(result, content.Order);
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"预约异常{ex.Message}");
            }
        }
        private bool AnalysisResult(JsonElement jsonElement, Order order)
        {
            var dicResult = JsonAnalysis.JsonToDic(jsonElement);
            var code = dicResult["code"].ToInt();
            var message = dicResult["message"].NotNullString();
            if (code != 200)
            {
                MainSession.PrintLogEvent.Publish(this, $"预约异常{message}");
                return false;
            }
            order.IntervalOnTime.StopInterval();
            MainSession.PrintLogEvent.Publish(this, $"result:预约申请提交成功");
            MainSession.PrintLogEvent.Publish(this, $"{order.ToLogString()}");
            return true;
        }
    }
}
