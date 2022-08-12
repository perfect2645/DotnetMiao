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
            content.AddHeader("Cookie", sessionItem.Cookie);
            content.AddHeader("Referer", sessionItem.Referer);

            content.BuildDefaultHeaders(Client);

            HttpDicResponse userInfo = PostStringAsync(content, ContentType.String).Result;
            var userid = userInfo.Body.FirstOrDefault(x => x.Key == Constant.AccountSn).Value?.ToString();
            if (userid == null || userid == "0")
            {
                throw new HttpException($"{Constant.ProjectName}:{url} has issue", Constant.AccountSn);
            }
            sessionItem.Key = userid;
            BaoheSession.AddOrUpdate(sessionItem, userInfo.Body);
            sessionItem.PrintLogEvent.Publish(this, userInfo.Body);
        }
    }
}
