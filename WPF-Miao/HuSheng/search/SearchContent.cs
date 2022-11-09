using HttpProcessor.Content;
using HuSheng.session;
using Utils.stringBuilder;

namespace HuSheng.search
{
    internal class SearchContent : HttpStringContent
    {
        public SearchContent(string url) : base(url)
        {
            ContentType = "application/x-www-form-urlencoded";
            BuildHeader();
            BuildContent();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "app.whkfqws.com");
            AddHeader("Connection", "keep-alive");
            AddHeader("Accept", "*/*");
            AddHeader("Origin", "http://app.whkfqws.com");
            AddHeader("X-Requested-With", "XMLHttpRequest");
            AddHeader("User-Agent", "Mozilla/5.0 AppleWebKit/605.1.15 Chrome/81.0.4044.138 Safari/537.36");

            AddHeader("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
            AddHeader("Accept-Encoding", "gzip, deflate");
        }

        private void BuildContent()
        {
            AddContent("ORG_CODE", "whsjjkfqzyjxmsqwsfwzx");
        }
    }
}
