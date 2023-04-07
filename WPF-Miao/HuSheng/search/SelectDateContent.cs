using HttpProcessor.Content;
using HuSheng.session;

namespace HuSheng.search
{
    internal class SelectDateContent : HttpStringContent
    {
        public string AppDate { get; }
        public SelectDateContent(string url, string date) : base(url)
        {
            AppDate = date;
            ContentType = "application/x-www-form-urlencoded";
            BuildHeader();
            BuildContent();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "hoosn.cn");
            AddHeader("Connection", "keep-alive");
            AddHeader("Cache-Control", "max-age=0");
            AddHeader("Upgrade-Insecure-Requests", "1");
            AddHeader("Origin", "http://hoosn.cn");
            AddHeader("User-Agent", "Mozilla/5.0 AppleWebKit/605.1.15 Chrome/81.0.4044.138 Safari/537.36");
            AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            AddHeader("Cookie", MainSession.Cookie);
            AddHeader("Referer", "http://hoosn.cn/yy/toSelectDate?subId=8D4AAA5FA2C04B8E971C89FCA2A4D4F4&openid=oSfkt5jTELgDNfJnxR_HjyF5Ardo");
            AddHeader("Accept-Encoding", "gzip, deflate");
            AddHeader("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
        }

        private void BuildContent()
        {
            AddContent("selectAgainID", string.Empty);
            AddContent("appDate", AppDate);
        }
    }
}
