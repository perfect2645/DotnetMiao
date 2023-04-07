using HttpProcessor.Client;
using HttpProcessor.Container;
using HttpProcessor.Content;
using JkzlSearcher.search;
using JkzlSearcher.session;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;

namespace JkzlSearcher.auth
{
    internal class DoctorAuthController : HttpClientBase
    {
        public DoctorAuthController(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task GetAutholicyAsync(string hospitalId, string deptId, string doctorSn)
        {
            return Task.Factory.StartNew(() =>
            {
                GetAutholicy(hospitalId, deptId, doctorSn);
            });
        }

        private void GetAutholicy(string hospitalId, string deptId, string doctorSn)
        {
            var content = new DoctorAuthContent(hospitalId, deptId, doctorSn);
            content.BuildDefaultHeaders(Client);

            HttpDicResponse response = PostStringAsync(content, ContentType.String).Result;
            var code = response.Body.FirstOrDefault(x => x.Key == Constants.StatusCode).Value?.ToString();
            if (code == null || code != "10000")
            {
                Log($"{Constants.ProjectName}:{response.Body["Message"]}");
                return;
            }

            var result = response.JsonBody.RootElement.GetProperty("Result");
            if (result.ValueKind == JsonValueKind.Null)
            {
                Log($"Null Result");
                return;
            }
            AnalysisResult(result);
        }

        private void AnalysisResult(JsonElement jsonElement)
        {
            var auth = JsonAnalysis.JsonToDic(jsonElement);
            if (!auth.HasItem())
            {
                Log($"Empty auth");
                return;
            }
            MainSession.PrintLogEvent.Publish(this, auth);
        }
    }
}
