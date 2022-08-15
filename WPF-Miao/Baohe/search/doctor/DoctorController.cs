using Baohe.constants;
using Baohe.session;
using Base.viewModel;
using HttpProcessor.Client;
using HttpProcessor.Content;
using HttpProcessor.ExceptionManager;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Utils;
using Utils.json;

namespace Baohe.search.doctor
{
    internal class DoctorController : HttpClientBase
    {
        public DoctorController(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task GetDoctorListAsync(ISessionItem sessionItem)
        {
            return Task.Factory.StartNew(() => GetDoctorList(sessionItem));
        }

        private void GetDoctorList(ISessionItem sessionItem)
        {
            var url = "https://appoint.yihu.com/appoint/do/doctor/getDocListByDeptId";
            var content = new DoctorContent(url);
            content.AddHeader("Cookie", sessionItem.Cookie);
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
            //"[{"hospitalId":"1040231","deptName":"儿童保健科","doctorUid":"710659564","doctorName":"儿保科医生","doctorSex":"3","lczcName":"主治医师","skill":"","photoUri":"","doctorSn":"710869460","doctorService_gh":"2","doctorService_phone":"0","doctorService_text":"0","doctorNumberList":"","numCount":30}]"

            BaoheSession.PlatformSesstion.AddOrUpdate("DoctorList", doctorDept);
        }
    }
}
