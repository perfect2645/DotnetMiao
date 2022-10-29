using HttpProcessor.Content;
using hys020.session;

namespace hys020.common
{
    internal class HysBaseContent : HttpStringContent
    {
        public HysBaseContent(string url) : base(url)
        {
            ContentType = "application/x-www-form-urlencoded";
            BuildHeader();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "www.hys020.com");
            AddHeader("Connection", "keep-alive");
            AddHeader("Accept", "application/json, text/javascript, */*");
            AddHeader("Origin", "http://www.hys020.com");
            AddHeader("X-Requested-With", "XMLHttpRequest");
            AddHeader("User-Agent", "Mozilla/5.0 AppleWebKit/605.1.15 Chrome/81.0.4044.138 Safari/537.36");
            AddHeader("Cookie", MainSession.Cookie);
            AddHeader("Referer", "http://www.hys020.com/home/");
            AddHeader("Accept-Encoding", "gzip, deflate");
            AddHeader("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
        }
    }
}
