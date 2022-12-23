using HttpProcessor.Content;
using Jingyang.session;
using System;

namespace Jingyang.common
{
    internal class JingyangContent : HttpStringContent
    {
        public JingyangContent(string url) : base(url)
        {
            ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            BuildHeader();
        }

        protected virtual void BuildHeader()
        {
            AddHeader("Host", "cnfw.mailiku.com");
            AddHeader("Connection", "keep-alive");
            AddHeader("Accept", "*/*");
            AddHeader("Origin", "http://cnfw.mailiku.com");
            AddHeader("X-Requested-With", "XMLHttpRequest");
            AddHeader("User-Agent", "Mozilla/5.0 AppleWebKit/605.1.15 Chrome/81.0.4044.138 Safari/537.36");
            AddHeader("Cookie", MainSession.Auth);
            //Cookie ？ noneed
            AddHeader("Referer", "http://www.jxy-tech.com/booking/companies/514966/?v=1");
            AddHeader("Accept-Encoding", "gzip, deflate");
            AddHeader("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
        }
    }
}
