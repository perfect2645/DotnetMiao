using gaoxin.common;
using gaoxin.session;
using HttpProcessor.Client;
using System;
using System.Net.Http;
using System.Text.Json;
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
            ProcessAction = new Action<GaoxinContent>(Yuyue);
            IntervalOnTime = new IntervalOnTime("yuyue", 300);
        }

        public void StartInterval(YuyueContent content)
        {
            IntervalOnTime.StartIntervalOntime(async () => await ProcessAsync(content));
        }

        private void Yuyue(GaoxinContent content)
        {
            if (IsSuccess)
            {
                IntervalOnTime?.StopInterval();
            }
            var order = (content as YuyueContent)?.Order;
            if (order != null)
            {
                var orderMsg = order.ToLogString();
                MainSession.PrintLogEvent.Publish(this, orderMsg);
            }

            HttpDicResponse response = PostStringDecodeAsync(content, Decode).Result;
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

            IsSuccess = true;
            CheckYuyueResult(resultValue);
        }

        private void CheckYuyueResult(JsonElement resultValue)
        {
            IsSuccess = true;
        }
    }
}
