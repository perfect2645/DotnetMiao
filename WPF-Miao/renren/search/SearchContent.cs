using HttpProcessor.Content;
using renren.session;
using Utils.stringBuilder;

namespace renren.search
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
            AddHeader("Host", "www.medic.ren");
            AddHeader("medicToken", MainSession.UserSession.Key);
            AddHeader("Accept", "*/*");
            AddHeader("Authorization", "ddtoken");
            AddHeader("source", "wx");
            AddHeader("Accept-Language", "zh-CN,zh-Hans;q=0.9");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
            AddHeader("Origin", "https://www.medic.ren");
            AddHeader("User-Agent", "Mozilla/5.0 AppleWebKit/605.1.15 Chrome/81.0.4044.138 Safari/537.36");
            AddHeader("Referer", "https://www.medic.ren/app/");
            AddHeader("Connection", "keep-alive");
        }

        private void BuildContent()
        {
            //AddContent("ORG_CODE", "whsjjkfqzyjxmsqwsfwzx");
            //AddContent("yyDate", MainSession.MiaoSession["Date"]);
        }
    }
}
