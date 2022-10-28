using HttpProcessor.Content;
using renren.session;

namespace renren.common
{
    internal class RenrenBaseContent : HttpStringContent
    {
        public RenrenBaseContent(string url) : base(url)
        {
            BuildHeader();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "www.medic.ren");
            AddHeader(MainSession.PlatformSession, Constants.MedicToken);
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
    }
}
