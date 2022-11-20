using HttpProcessor.Content;
using suiyang.session;
using System;

namespace suiyang.common
{
    internal class SuiyangBaseContent : HttpStringContent
    {
        public SuiyangBaseContent(string url) : base(url)
        {
            ContentType = "application/x-www-form-urlencoded";
            BuildHeader();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "www.jxy-tech.com");
            AddHeader("Connection", "keep-alive");
            AddHeader("User-Agent", "Mozilla/5.0 AppleWebKit/605.1.15 Chrome/81.0.4044.138 Safari/537.36");
            AddHeader("X-Requested-With", "XMLHttpRequest");
            AddHeader("Authorization", MainSession.Auth);
            AddHeader("Accept", "*/*");
            //Cookie ？ noneed
            AddHeader("Referer", "http://www.jxy-tech.com/booking/companies/514966/?v=1");
            AddHeader("Accept-Encoding", "gzip, deflate");
            AddHeader("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
        }
    }
}
