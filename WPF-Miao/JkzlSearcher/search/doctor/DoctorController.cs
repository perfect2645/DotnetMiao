using Base.viewmodel.status;
using HttpProcessor.Client;
using HttpProcessor.Content;
using JkzlSearcher.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;

namespace JkzlSearcher.search
{
    internal class DoctorController : HttpClientBase
    {
        public DoctorController(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<List<Dictionary<string, object>>> GetDoctorListAsync(string hosId, string deptId)
        {
            return await Task.Factory.StartNew(() =>
            {
                return GetDoctorList(hosId, deptId);
            });
        }

        private List<Dictionary<string, object>> GetDoctorList(string hosId, string deptId)
        {
            var content = new DoctorContent(deptId, hosId);
            content.BuildDefaultHeaders(Client);

            HttpDicResponse response = PostStringAsync(content, ContentType.String).Result;
            var code = response.Body?.FirstOrDefault(x => x.Key == Constants.StatusCode).Value?.ToString();
            if (code == null || code != "10000")
            {
                var msg = response.Body.GetString("Message");
                Log($"{Constants.ProjectName}:{msg}, hospitalId = {hosId} , departmentId = {deptId}");
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
                Log($"Null Result, hospitalId = {hosId} , departmentId = {deptId}");
                return null;
            }
            return AnalysisResult(result, hosId, deptId);
        }

        private List<Dictionary<string, object>> AnalysisResult(JsonElement jsonElement, string hosId, string deptId)
        {
            var hospital = JsonAnalysis.JsonToDicList(jsonElement);
            if (!hospital.HasItem())
            {
                Log($"Empty Result, hospitalId = {hosId} , departmentId = {deptId}");
                return null;
            }
            MainSession.PrintLogEvent.Publish(this, hospital);

            return hospital;
        }
    }
}
