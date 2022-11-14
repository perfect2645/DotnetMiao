using HttpProcessor.Content;
using Jkchegu.session;
using Utils.stringBuilder;

namespace Jkchegu.search
{
    internal class DateCountContent : HttpStringContent
    {
        public string Etid { get; }
        public DateCountContent(string etid, string url) : base(url)
        {
            Etid = etid;
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

            AddHeader("Referer", $"http://app.whkfqws.com/wx-mobile/Vaccination/vaccination.do?ETID={Etid}");

            AddHeader("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
            AddHeader("Accept-Encoding", "gzip, deflate");
        }

        private void BuildContent()
        {
            AddContent("ORG_CODE", "whsjjkfqzyjxmsqwsfwzx");

        }
    }
}
