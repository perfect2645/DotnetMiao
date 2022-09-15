using Baohe.constants;
using Baohe.session;
using HttpProcessor.Client;
using HttpProcessor.Content;
using HttpProcessor.ExceptionManager;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils.json;

namespace Baohe.search.Liudiao
{
    internal class LiudiaoController : HttpClientBase
    {
        public LiudiaoController(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task LiudiaoAsync()
        {
            return Task.Factory.StartNew(() => Liudiao());
        }

        private void Liudiao()
        {
            var url = "https://fycombat.yihu.com/AHForm/doRegHFSSNGWSYEpidemic";
            var content = new LiudiaoContent(url);
            content.AddHeader("Cookie", BaoheSession.Cookie);
            content.AddHeader("Referer", content.BuildReferer());

            content.BuildDefaultHeaders(Client);

            HttpDicResponse response = PostStringAsync(content, ContentType.String).Result;
            var code = response.Body.FirstOrDefault(x => x.Key == Constant.StatusCode).Value?.ToString();
            if (code == null || code != "10000")
            {
                throw new HttpException($"{Constant.ProjectName}:Liudiao-{url} - {response.Body["Message"]}", "Liudiao");
            }

            var result = response.JsonBody.RootElement.GetProperty("Result");
            if (result.ValueKind == JsonValueKind.Null)
            {
                throw new HttpException($"{Constant.ProjectName}:Liudiao-{url} - Result is empty", "empty result");
            }
            AnalizeResult(result);
        }

        private void AnalizeResult(JsonElement jsonElement)
        {
            var result = JsonAnalysis.JsonToDicList(jsonElement);
            BaoheSession.AddUserSession(Constant.Liudiao, result);

            BaoheSession.PrintLogEvent.Publish(this, result, "Liudiao");
        }
    }
}
