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
            content.AddHeader("Referer", "https://appoint.yihu.com/appoint/register/registerOrder.html?platformType=9000370&hospitalId=1040231&deptId=7175975&doctorSn=710869460&arrangeId=160023903&utm_source=0.0.h.1026.bus010.0&channelId=9000370&sceneId=&isGuahao=&fmId=hfssqngzwsy&retid=8f1adb4a37e143a885d53db93f803eeb&canreg=1");

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
