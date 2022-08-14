using Baohe.constants;
using Baohe.session;
using Base.viewModel;
using HttpProcessor.Client;
using HttpProcessor.Content;
using HttpProcessor.ExceptionManager;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Baohe.search
{
    internal class AppointNumbersController : HttpClientBase
    {
        public AppointNumbersController(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task GetNumbersAsync(ISessionItem sessionItem)
        {
            return Task.Factory.StartNew(() => GetNumbers(sessionItem));
        }

        private void GetNumbers(ISessionItem sessionItem)
        {
            var url = "https://appoint.yihu.com/appoint/do/registerInfo/getNumbers";
            var content = new AppointNumbersContent(url);
            content.AddHeader("Cookie", content.BuildCookie());
            content.AddHeader("Referer", content.BuildReferer());

            content.BuildDefaultHeaders(Client);

            HttpDicResponse appointNumbers = PostStringAsync(content, ContentType.String).Result;
            var code = appointNumbers.Body.FirstOrDefault(x => x.Key == Constant.StatusCode).Value?.ToString();
            if (code == null || code != "10000")
            {
                throw new HttpException($"{Constant.ProjectName}:GetNumbers-{url} - {appointNumbers.Body["Message"]}", Constant.GetNumbers);
            }
            BaoheSession.AddOrUpdate(sessionItem, appointNumbers.Body);
            sessionItem.PrintLogEvent.Publish(this, appointNumbers.Body);
        }
    }
}
