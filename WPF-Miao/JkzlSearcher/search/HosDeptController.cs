using HttpProcessor.Client;
using HttpProcessor.Container;
using HttpProcessor.Content;
using HttpProcessor.ExceptionManager;
using JkzlSearcher.session;
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

        public Task GetHosDeptAsync(string hospitalId)
        {
            return Task.Factory.StartNew(() => 
            {
                HosDeptContent.SetHospitalId(hospitalId);
                GetHosDept(hospitalId);
            });
        }

        private void GetHosDept(string hospitalId)
        {
            
            Log($"start GetHosDept, hospitalId = {hospitalId}");
            HttpDicResponse response = PostStringAsync(HosDeptContent, ContentType.String).Result;
            var code = response.Body.FirstOrDefault(x => x.Key == Constants.StatusCode).Value?.ToString();
            if (code == null || code != "10000")
            {
                Log($"{Constants.ProjectName}:{response.Body["Message"]}, hospitalId = {hospitalId}");
                return;
            }

            var result = response.JsonBody.RootElement.GetProperty("Result");
            if (result.ValueKind == JsonValueKind.Null)
            {
                Log($"Null Result, hospitalId = {hospitalId}");
                return;
            }
            AnalysisResult(result, hospitalId);
        }

        private void AnalysisResult(JsonElement jsonElement, string hospitalId)
        {
            var doctorDept = JsonAnalysis.JsonToDicList(jsonElement);
            if (!doctorDept.HasItem())
            {
                Log($"Empty Result, hospitalId = {hospitalId}");
                return;
            }
            MainSession.PrintLogEvent.Publish(this, doctorDept);

            var hospitalController = HttpServiceController.GetService<HospitalController>();
            hospitalController.GetHospitalByIdAsync(hospitalId);
        }
    }
}
