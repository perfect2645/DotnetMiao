using HttpProcessor.Content;
using Longchi.session;
using System;

namespace Longchi.common
{
    internal class LongchiContent : HttpStringContent
    {

        public string Cookie { get; private set; }
        public LongchiContent(string url, string cookie) : base(url)
        {
            ContentType = "application/x-www-form-urlencoded";
            Cookie = cookie;
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
            AddHeader("Cookie", Cookie);
            AddHeader("Referer", "http://cnfw.mailiku.com/index/index/show/id/8.html");
            AddHeader("Accept-Encoding", "gzip, deflate");
            AddHeader("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
        }
    }
}
