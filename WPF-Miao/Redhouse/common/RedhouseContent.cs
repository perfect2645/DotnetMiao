using HttpProcessor.Content;
using Redhouse.session;

namespace Redhouse.common
{
    internal class RedhouseContent : HttpStringContent
    {
        public RedhouseContent(string url) : base(url)
        {
            ContentType = "application/x-www-form-urlencoded";
            BuildHeader();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "www.Redhouse.com");
            AddHeader("Connection", "keep-alive");
            AddHeader("Accept", "application/json, text/javascript, */*");
            AddHeader("Origin", "http://www.Redhouse.com");
            AddHeader("X-Requested-With", "XMLHttpRequest");
            AddHeader("User-Agent", "Mozilla/5.0 AppleWebKit/605.1.15 Chrome/81.0.4044.138 Safari/537.36");
            AddHeader("Cookie", MainSession.Cookie);
            AddHeader("Referer", "http://www.Redhouse.com/home/");
            AddHeader("Accept-Encoding", "gzip, deflate");
            AddHeader("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
        }
    }
}
