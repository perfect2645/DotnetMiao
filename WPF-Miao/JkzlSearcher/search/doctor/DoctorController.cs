using HttpProcessor.Client;
using JkzlSearcher.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JkzlSearcher.search
{
    internal class DoctorController : HttpClientBase
    {
        public DoctorController(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task GetDoctorListAsync(string hosId, string deptId)
        {
            return Task.Factory.StartNew(() => GetDoctorList(hosId, deptId));
        }

        private void GetDoctorList(string hosId, string deptId)
        {
            var content = new DoctorContent(deptId, hosId);
            content.BuildDefaultHeaders(Client);

            HttpDicResponse response = PostStringAsync(content, ContentType.String).Result;
            var code = response.Body.FirstOrDefault(x => x.Key == Constant.StatusCode).Value?.ToString();
            if (code == null || code != "10000")
            {
                throw new HttpException($"{Constant.ProjectName}:GetDoctorList-{url} - {response.Body["Message"]}", "bad response");
            }

            var result = response.JsonBody.RootElement.GetProperty("Result");
            if (result.ValueKind == JsonValueKind.Null)
            {
                throw new HttpException($"{Constant.ProjectName}:GetDoctorList-{url} - Result is empty", "empty result");
            }
            AnalysisResult(result);
        }

        private void AnalysisResult(JsonElement jsonElement)
        {
            var doctorDept = JsonAnalysis.JsonToDicList(jsonElement);

            BaoheSession.AddMiaoSession(Constant.DoctorList, doctorDept);

            BaoheSession.PrintLogEvent.Publish(this, doctorDept, "DoctorList");
        }
    }
}
