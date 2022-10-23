using Baohe.constants;
using Baohe.session;
using HttpProcessor.Client;
using HttpProcessor.Content;
using HttpProcessor.ExceptionManager;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.datetime;
using Utils.json;

namespace Baohe.search.ArrangeWater
{
    internal class ArrangeWaterController : HttpClientBase
    {
        public ArrangeWaterController(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task<bool> GetArrangeWaterAsync(bool isPrintLog = false)
        {
            return Task.Factory.StartNew(() => GetArrangeWater(isPrintLog));
        }

        private bool GetArrangeWater(bool isPrintLog = false)
        {
            BaoheSession.PrintLogEvent.Publish(this, $"GetArrangeWater Start");
            var url = "https://appoint.yihu.com/appoint/do/doctorArrange/getArrangeWater";
            var content = new ArrangeWaterContent(url);
            content.AddHeader("Cookie", BaoheSession.Cookie);
            content.AddHeader("Referer", content.BuildReferer());

            content.BuildDefaultHeaders(Client);

            HttpDicResponse response = PostStringAsync(content, ContentType.String).Result;
            BaoheSession.PrintLogEvent.Publish(this, $"GetArrangeWater End");
            var code = response.Body.FirstOrDefault(x => x.Key == Constant.StatusCode).Value?.ToString();
            if (code == null || code != "10000")
            {
                throw new HttpException($"{Constant.ProjectName}:GetArrangeWater-{url} - {response.Body["Message"]}", "Arrange Water");
            }

            var result = response.JsonBody.RootElement.GetProperty("Result");
            if (result.ValueKind == JsonValueKind.Null)
            {
                throw new HttpException($"{Constant.ProjectName}:GetArrangeWater-{url} - Result is empty", "empty result");
            }

            var arrangeWaters = AnalysisResult(result);

            if (isPrintLog)
            {
                BaoheSession.PrintLogEvent.Publish(this, arrangeWaters, "ArrangeWater");
            }

            return true;
        }

        private List<Dictionary<string, object>> AnalysisResult(JsonElement jsonElement)
        {
            var arrangeWater = JsonAnalysis.JsonToDicList(jsonElement);
            var arrangeWaterList = SessionBuilder.GetAvailableArrangeWater(arrangeWater);

            if (!arrangeWaterList.HasItem())
            {
                throw new HttpException($"{Constant.ProjectName}:查苗成功-没有可用苗", "no water");
            }

            BaoheSession.AddMiaoSession(Constant.ArrangeWater, arrangeWaterList);

            return arrangeWaterList;
        }
    }
}
