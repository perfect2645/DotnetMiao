using HttpProcessor.Content;

namespace Jkchegu.search
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
            AddHeader("Referer", "http://app.whkfqws.com/wx-mobile/Vaccination/vaccination.do?ETID=7bf4400434ea4e80a6dfb331f6f6a077");

            AddHeader("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
            AddHeader("Accept-Encoding", "gzip, deflate");
        }

        private void BuildContent()
        {
            AddContent("ORG_CODE", "whsjjkfqzyjxmsqwsfwzx");
            AddContent("yyDate", "2022-10-07");
        }
    }
}
