using HttpProcessor.Content;
using Tianhe.session;

namespace Tianhe.common
{
    internal class TianheContent : HttpStringContent
    {
        public TianheContent(string url) : base(url)
        {
            ContentType = "application/x-www-form-urlencoded";
            BuildHeader();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "ctmingyi.com:18082");
            AddHeader("Connection", "keep-alive");
            AddHeader("User-Agent", "Mozilla/5.0 AppleWebKit/605.1.15 Chrome/81.0.4044.138 Safari/537.36");
            AddHeader("Referer", "https://servicewechat.com/wx8d28cdcd4357647c/33/page-frame.html");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
        }
    }
}
