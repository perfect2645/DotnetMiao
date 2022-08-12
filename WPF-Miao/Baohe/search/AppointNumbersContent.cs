using HttpProcessor.Content;
using System;
using System.Net.Http;

namespace Baohe.search
{
    internal class AppointNumbersContent : HttpStringContent
    {
        public AppointNumbersContent(string url) : base(url)
        {
            BuildContent();
        }

        public override void BuildDefaultHeaders(HttpClient httpClient)
        {
            AddHeader("Host", "appoint.yihu.com");
            AddHeader("Accept", "application/json, text/javascript, */*; q=0.01");
            AddHeader("Origin", "https://appoint.yihu.com");
            AddHeader("X-Requested-With", "XMLHttpRequest");
            AddHeader("User-Agent", @"Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36");

            AddHeader("Sec-Fetch-Site", "same-origin");
            AddHeader("Sec-Fetch-Mode", "cors");
            AddHeader("Sec-Fetch-Dest", "empty");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
            AddHeader("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");

            base.BuildDefaultHeaders(httpClient);
        }

        private void BuildContent()
        {
            AddContent("arrangeId", "160023903");
            AddContent("hospitalId", "1040231");
            AddContent("appliedDepartment", "");
            AddContent("channelId", "9000370");
            AddContent("ClinicCard", "");
        }
    }
}
