using gaoxin.common;
using HttpProcessor.Client;
using System;
using System.Net.Http;
using System.Text.Json;
using Utils.timerUtil;

namespace gaoxin.appointment
{
    internal class YuyueController : GaoxinControllerBase
    {
        public IntervalOnTime IntervalOnTime { get; set; }
        internal YuyueContent Content { get; set; }
        public YuyueController(HttpClient httpClient) : base(httpClient)
        {
            ProcessAction = new Action<GaoxinContent>(Yuyue);
        }

        private void Yuyue(GaoxinContent content)
        {
            HttpDicResponse response = PostStringDecodeAsync(content, Decode).Result;
            var resultValue = CheckGetResultValue(response);
            if (resultValue.ValueKind == JsonValueKind.Null)
            {
                return;
            }

            CheckYuyueResult(resultValue);
        }

        private void CheckYuyueResult(JsonElement resultValue)
        {
        }
    }
}
