using HttpProcessor.Client;
using HttpProcessor.Content;
using HttpProcessor.ExceptionManager;
using JkzlSearcher.session;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils.json;

namespace JkzlSearcher.search
{
    internal class HosDeptController : HttpClientBase
    {
        public HosDeptController(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task GetHosDeptAsync()
        {
            return Task.Factory.StartNew(() => GetHosDept());
        }

        private void GetHosDept()
        {
            var content = new HosDeptContent();
            content.BuildDefaultHeaders(Client);

            HttpDicResponse response = PostStringAsync(content, ContentType.String).Result;
            var code = response.Body.FirstOrDefault(x => x.Key == Constants.StatusCode).Value?.ToString();
            if (code == null || code != "10000")
            {
                throw new HttpException($"{Constants.ProjectName}:{response.Body["Message"]}", "bad response");
            }

            var result = response.JsonBody.RootElement.GetProperty("Result");
            if (result.ValueKind == JsonValueKind.Null)
            {
                throw new HttpException($"{Constants.ProjectName} - Result is empty", "empty result");
            }
            AnalysisResult(result);
        }

        private void AnalysisResult(JsonElement jsonElement)
        {
            var doctorDept = JsonAnalysis.JsonToDicList(jsonElement);
            MainSession.PrintLogEvent.Publish(this, doctorDept);
        }
    }
}
