using Baohe.constants;
using Baohe.session;
using Base.viewModel;
using HttpProcessor.Client;
using HttpProcessor.Content;
using HttpProcessor.ExceptionManager;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;

namespace Baohe.search.numbers
{
    internal class AppointNumbersController : HttpClientBase
    {
        public AppointNumbersController(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task GetNumbersAsync(ISessionItem sessionItem)
        {
            return Task.Factory.StartNew(() => GetNumbers(sessionItem));
        }

        private void GetNumbers(ISessionItem sessionItem)
        {
            var url = "https://appoint.yihu.com/appoint/do/registerInfo/getNumbers";
            var content = new AppointNumbersContent(url, sessionItem);
            content.AddHeader("Cookie", content.BuildCookie());
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
            AnalizeResult(result, sessionItem);
        }

        private void AnalizeResult(JsonElement jsonElement, ISessionItem sessionItem)
        {
            var numbers = JsonAnalysis.JsonToDicList(jsonElement);
            BaoheSession.PlatformSesstion.AddOrUpdate(Constant.Numbers, numbers);

            sessionItem.PrintLogEvent.Publish(this, numbers, "Numbers");
        }
    }
}
