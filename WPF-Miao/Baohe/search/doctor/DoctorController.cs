using Baohe.constants;
using Baohe.session;
using Base.viewModel;
using HttpProcessor.Client;
using HttpProcessor.Content;
using HttpProcessor.ExceptionManager;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

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
                throw new HttpException($"{Constant.ProjectName}:GetDoctorList-{url} - {response.Body["Message"]}", Constant.GetNumbers);
            }

            var result = response.JsonBody.RootElement;
        }
    }
}
