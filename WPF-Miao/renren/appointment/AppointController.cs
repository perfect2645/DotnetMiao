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

        internal int Yuyue(List<Order> orderList)
        {
            if (IsSuccess)
            {
                MainSession.PrintLogEvent.Publish(this, $"预约结束，退出循环");
                return 1;
            }
            foreach (var order in orderList)
            {
                if (IsSuccess)
                {
                    order.IntervalOnTime.StopInterval();
                    MainSession.PrintLogEvent.Publish(this, $"预约结束，退出循环");
                    return 1;
                }
                MainSession.PrintLogEvent.Publish(this, order.ToLogString());
                var content = new AppointContent(order);

                Thread.Sleep(1000);

                Task.Factory.StartNew(() => AppointAsync(content));
                order.IntervalOnTime.StartIntervalOntime(() =>
                {
                    Task.Factory.StartNew(() => AppointAsync(content));
                });
            }

            return 0;
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

                MainSession.PrintLogEvent.Publish(this, $"开始预约：");
                MainSession.PrintLogEvent.Publish(this, $"{content.Order.ToLogString()}");
                HttpDicResponse response = PostStringAsync(content, ContentType.Json).Result;
                //if (response == null)
                //{
                //    MainSession.PrintLogEvent.Publish(this, $"Appoint response is null");
                //}

                //var result = response.JsonBody.RootElement;
                //var schedule = AnalysisResult(result, content.Order);
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
            return true;
        }
    }
}
