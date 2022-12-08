using HttpProcessor.Client;
using HttpProcessor.Content;
using JkzlSearcher.session;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;

namespace JkzlSearcher.search
{
    internal class HospitalController : HttpClientBase
    {
        public HospitalController(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task GetHospitalByIdAsync(string hospitalId)
        {
            return Task.Factory.StartNew(() =>
            {
                GetHospitalById(hospitalId);
            });
        }

        private void GetHospitalById(string hospitalId)
        {
            var content = new HospitalContent(hospitalId);
            Log($"start HospitalById, hospitalId = {hospitalId}");
            HttpDicResponse response = PostStringAsync(content, ContentType.String).Result;
            var code = response.Body.FirstOrDefault(x => x.Key == Constants.StatusCode).Value?.ToString();
            if (code == null || code != "10000")
            {
                Log($"{Constants.ProjectName}:{response.Body["Message"]}, hospitalId = {hospitalId}");
                return;
            }

            var result = response.JsonBody.RootElement;
            if (result.ValueKind == JsonValueKind.Null)
            {
                Log($"Null Result, hospitalId = {hospitalId}");
                return;
            }
            AnalysisResult(result, hospitalId);
        }

        private void AnalysisResult(JsonElement jsonElement, string hospitalId)
        {
            var hospital = JsonAnalysis.JsonToDic(jsonElement);
            if (!hospital.HasItem())
            {
                Log($"Empty Result, hospitalId = {hospitalId}");
                return;
            }
            MainSession.PrintLogEvent.Publish(this, hospital);
        }
    }
}
