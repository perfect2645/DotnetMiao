using Baohe.constants;
using Baohe.session;
using Base.viewModel;
using HttpProcessor.Client;
using HttpProcessor.Content;
using HttpProcessor.ExceptionManager;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;
using Utils.number;

namespace Baohe.search.numbers
{
    internal class AppointNumbersController : HttpClientBase
    {
        public AppointNumbersController(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task<bool> GetNumbersAsync(bool isPrintLog = false)
        {
            return Task.Factory.StartNew(() => GetNumbers(isPrintLog));
        }

        private bool GetNumbers(bool isPrintLog = false)
        {
            var url = "https://appoint.yihu.com/appoint/do/registerInfo/getNumbers";

            var arrangeWaterList = SessionBuilder.GetAvailableArrangeWater();

            var index = 0;
            if (arrangeWaterList.Count > 0)
            {
                index = NumberUtil.IntRandom(0, arrangeWaterList.Count - 1);
            }
            var arrangeWater = arrangeWaterList[index]!;

            var content = new AppointNumbersContent(url, arrangeWater);
            content.AddHeader("Cookie", MainSession.Cookie);
            content.AddHeader("Referer", content.BuildReferer());

            content.BuildDefaultHeaders(Client);

            HttpDicResponse response = PostStringAsync(content, ContentType.String).Result;
            var code = response.Body.FirstOrDefault(x => x.Key == Constant.StatusCode).Value?.ToString();
            if (code == null || code != "10000")
            {
                throw new HttpException($"{Constant.ProjectName}:GetNumbers-{url} - {response.Body["Message"]}", Constant.GetNumbers);
            }

            var result = response.JsonBody.RootElement.GetProperty("Result");
            if (result.ValueKind == JsonValueKind.Null)
            {
                throw new HttpException($"{Constant.ProjectName}:GetNumbers-{url} - Result is empty", "empty result");
            }

            var numbers = AnalysisResult(result, arrangeWater);

            if (isPrintLog)
            {
                MainSession.PrintLogEvent.Publish(this, numbers, "Numbers");
            }

            return true;
        }

        private List<Dictionary<string, object>> AnalysisResult(JsonElement jsonElement, Dictionary<string, object> arrangeWater)
        {
            var numbers = JsonAnalysis.JsonToDicList(jsonElement);
            foreach (var number in numbers)
            {
                number.AddOrUpdate("arrangeWater", arrangeWater);
            }

            numbers = numbers.DisorderItems();
            MainSession.AddMiaoSession(Constant.Numbers, numbers);

            return numbers;
        }
    }
}
