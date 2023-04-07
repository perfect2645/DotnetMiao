using HttpProcessor.Client;
using HttpProcessor.Content;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.datetime;
using Utils.json;
using Utils.stringBuilder;
using Xihongmen.session;
namespace Xihongmen.search
{
    internal class TimeController : HttpClientBase
    {
        private TimeContent content;

        public TimeController(HttpClient httpClient) : base(httpClient)
        {
            BuildContent();
        }

        public void GetTimeAsync()
        {
            Task.Factory.StartNew(() => GetTime());
        }

        internal void BuildContent()
        {
            content = new TimeContent();
            BuildClientHeaders(content);
        }

        public void GetTime()
        {
            try
            {
                var response = PostStringAsync(content, ContentType.String, false).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"GetTime失败 - {response?.Message},请检查参数");
                }
                var code = response.JsonBody.RootElement.GetProperty("code").NotNullString();
                var msg = response.JsonBody.RootElement.GetProperty("msg").NotNullString();
                MainSession.PrintLogEvent.Publish(this, $"GetTime - {msg}");
                if ("200".Equals(code) && "OK".Equals(msg))
                {
                    var data = response.JsonBody.RootElement.GetProperty("data");
                    SaveTime(data);
                }
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"GetTime异常{ex.Message}");
            }
        }

        private void SaveTime(JsonElement data)
        {
            var times = JsonAnalysis.JsonToDicList(data);
            if (!times.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, $"GetTime信息失败");
                return;
            }

            var timeList = times.Select(t => t["time"]).ToList();
            //var testT = DateTimeUtil.GetTimeStamp(DateTime.Today.AddDays(4).AddHours(8));

            MainSession.PlatformSession.AddOrUpdate("timeList", timeList);
        }
    }
}
