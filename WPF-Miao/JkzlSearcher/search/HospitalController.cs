using Base.viewmodel.status;
using HttpProcessor.Client;
using HttpProcessor.Content;
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
    internal class HospitalController : HttpClientBase
    {
        public HospitalController(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<Dictionary<string, object>> GetHospitalByIdAsync(string hospitalId)
        {
            return await Task.Factory.StartNew(() =>
            {
                return GetHospitalById(hospitalId);
            });
        }

        private Dictionary<string, object> GetHospitalById(string hospitalId)
        {
            var content = new HospitalContent(hospitalId);
            Log($"start HospitalById, hospitalId = {hospitalId}");
            HttpDicResponse response = PostStringAsync(content, ContentType.String).Result;
            var code = response.Body.FirstOrDefault(x => x.Key == Constants.StatusCode).Value?.ToString();
            if (code == null || code != "10000")
            {
                var msg = response.Body.GetString("Message");
                Log($"{Constants.ProjectName}:{msg}, hospitalId = {hospitalId}");
                if (!string.IsNullOrEmpty(msg) && msg.Contains("频繁"))
                {
                    MainSession.CurrentHospitalId -= 1;
                    MainSession.SetStatus(MiaoProgress.Searchend);
                }
                return null;
            }

            var result = response.JsonBody.RootElement;
            if (result.ValueKind == JsonValueKind.Null)
            {
                Log($"Null Result, hospitalId = {hospitalId}");
                return null;
            }
            return AnalysisResult(result, hospitalId);
        }

        private Dictionary<string, object> AnalysisResult(JsonElement jsonElement, string hospitalId)
        {
            var hospital = JsonAnalysis.JsonToDic(jsonElement);
            if (!hospital.HasItem())
            {
                Log($"Empty Result, hospitalId = {hospitalId}");
                return null;
            }
            MainSession.PrintLogEvent.Publish(this, hospital);

            return hospital;
        }
    }
}
