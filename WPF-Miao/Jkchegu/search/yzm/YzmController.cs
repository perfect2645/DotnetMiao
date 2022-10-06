using HttpProcessor.Client;
using Jkchegu.session;
using System.Net.Http;
using System.Threading.Tasks;
using Utils.datetime;

namespace Jkchegu.search.yzm
{
    internal class YzmController : HttpClientBase
    {
        public YzmController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void GetYzmAsync()
        {
            Task.Factory.StartNew(() => GetYzm());
        }

        public void GetYzm()
        {
            var timeStamp = DateTimeUtil.GetTimeStamp();
            var url = $"http://app.whkfqws.com/wx-mobile/Vaccination/code.do?t={timeStamp}";

            var content = new SearchContent(url);
            content.AddHeader("Cookie", JkSession.Cookie);

            content.BuildDefaultHeaders(Client);

            var response = SearchAsync(content.RequestUrl);
            if (response == null)
            {
                JkSession.PrintLogEvent.Publish(this, $"登录过期了");
            }
        }
    }
}
