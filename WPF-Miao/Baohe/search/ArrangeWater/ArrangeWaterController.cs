using Baohe.constants;
using Baohe.session;
using Base.viewModel;
using HttpProcessor.Client;
using HttpProcessor.ExceptionManager;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Baohe.search.ArrangeWater
{
    internal class ArrangeWaterController : HttpClientBase
    {
        public ArrangeWaterController(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task GetArrangeWaterAsync(ISessionItem sessionItem)
        {
            return Task.Factory.StartNew(() => GetArrangeWater(sessionItem));
        }

        private void GetArrangeWater(ISessionItem sessionItem)
        {
            var url = "https://appoint.yihu.com/appoint/do/doctorArrange/getArrangeWater";
            var content = new ArrangeWaterContent(url);
            content.AddHeader("Cookie", content.BuildCookie());
            content.AddHeader("Referer", content.BuildReferer());

            content.BuildDefaultHeaders(Client);

            HttpDicResponse response = PostStringAsync(content).Result;
            var code = response.Body.FirstOrDefault(x => x.Key == Constant.StatusCode).Value?.ToString();
            if (code == null || code != "10000")
            {
                throw new HttpException($"{Constant.ProjectName}:GetArrangeWater-{url} - {response.Body["Message"]}", Constant.GetNumbers);
            }
        }
    }
}
