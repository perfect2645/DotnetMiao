using HttpProcessor.Client;
using HttpProcessor.Container;
using HttpProcessor.Content;
using HttpProcessor.ExceptionManager;
using JkzlSearcher.session;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;

namespace JkzlSearcher.search
{
    internal class HosDeptController : HttpClientBase
    {
        public HosDeptContent HosDeptContent { get; set; }
        public HosDeptController(HttpClient httpClient) : base(httpClient)
        {
            HosDeptContent = new HosDeptContent();
            HosDeptContent.BuildDefaultHeaders(Client);
        }

        public async Task<List<Dictionary<string, object>>> GetHosDeptAsync(string hospitalId)
        {
            return await Task.Factory.StartNew(() => 
            {
                HosDeptContent.SetHospitalId(hospitalId);
                return GetHosDept(hospitalId);
            });
        }

        private List<Dictionary<string, object>> GetHosDept(string hospitalId)
        {
            Log($"start GetHosDept, hospitalId = {hospitalId}");
            var depts = new List<Dictionary<string, object>>();

            HttpDicResponse response = PostStringAsync(HosDeptContent, ContentType.String).Result;
            var code = response.Body.FirstOrDefault(x => x.Key == Constants.StatusCode).Value?.ToString();
            if (code == null || code != "10000")
            {
                Log($"{Constants.ProjectName}:{response.Body["Message"]}, hospitalId = {hospitalId}");
                return depts;
            }

            var result = response.JsonBody.RootElement.GetProperty("Result");
            if (result.ValueKind == JsonValueKind.Null)
            {
                Log($"Null Result, hospitalId = {hospitalId}");
                return depts;
            }
            return AnalysisResult(result, hospitalId);
        }

        private List<Dictionary<string, object>> AnalysisResult(JsonElement jsonElement, string hospitalId)
        {
            var depts = new List<Dictionary<string, object>>();
            depts = JsonAnalysis.JsonToDicList(jsonElement);
            if (!depts.HasItem())
            {
                return depts;
            }
            MainSession.PrintLogEvent.Publish(this, depts);

            return depts;
        }
    }
}
