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

namespace Baohe.search.doctor
{
    internal class DoctorController : HttpClientBase
    {
        public DoctorController(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task GetDoctorListAsync()
        {
            return Task.Factory.StartNew(() => GetDoctorList());
        }

        private void GetDoctorList()
        {
            var url = "https://appoint.yihu.com/appoint/do/doctor/getDocListByDeptId";
            var content = new DoctorContent(url);
            content.AddHeader("Cookie", BaoheSession.Cookie);
            content.AddHeader("Referer", content.BuildReferer());

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
            AnalizeResult(result);
        }

        private void AnalizeResult(JsonElement jsonElement)
        {
            var doctorDept = JsonAnalysis.JsonToDicList(jsonElement);

            BaoheSession.AddMiaoSession(Constant.DoctorList, doctorDept);

            BaoheSession.PrintLogEvent.Publish(this, doctorDept, "DoctorList");
        }
    }
}
