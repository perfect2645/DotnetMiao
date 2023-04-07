using gaoxin.common;
using gaoxin.session;
using HttpProcessor.Client;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils.stringBuilder;
using Utils.timerUtil;

namespace gaoxin.appointment
{
    internal class YuyueController : GaoxinControllerBase
    {
        public IntervalOnTime IntervalOnTime { get; set; }
        public bool IsSuccess { get; set; }
        public YuyueController(HttpClient httpClient) : base(httpClient)
        {
            ProcessAction = new Action<GaoxinContent>(YuyueAsync);
            IntervalOnTime = new IntervalOnTime("yuyue", 300);
        }

        public void StartInterval(YuyueContent content)
        {
            IntervalOnTime.StartIntervalOntime(() => ProcessAsync(content));
        }

        public async void YuyueAsync(GaoxinContent content)
        {
            if (IsSuccess)
            {
                IntervalOnTime?.StopInterval();
            }
            var order = (content as YuyueContent)?.Order;
            if (order != null)
            {
                var orderMsg = order.ToLogString();
                Log(orderMsg);
            }

            HttpDicResponse response = await PostStringDecodeAsync(content, Decode);
            var resultValue = CheckGetResultValue(response);
            if (resultValue.ValueKind == JsonValueKind.Null)
            {
                return;
            }

            if (resultValue.ValueKind == JsonValueKind.Undefined)
            {
                return;
            }

            if (resultValue.ValueKind == JsonValueKind.String)
            {
                var result = resultValue.NotNullString();
                var message = $"{result} - User: {order.UserName}";
                MainSession.PrintLogEvent.Publish(this, message);
                return;
            }

            CheckYuyueResult(resultValue, order);
        }

        private void CheckYuyueResult(JsonElement resultValue, Order order)
        {
            MainSession.PrintLogEvent.Publish(this, "预约完成，请查看预约结果");
            MainSession.PrintLogEvent.Publish(this, $"{order.ToLogString()}");
            IsSuccess = true;
        }
    }
}
