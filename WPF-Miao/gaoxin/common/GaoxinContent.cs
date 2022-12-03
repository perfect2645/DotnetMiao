using HttpProcessor.Content;
using gaoxin.session;
using Utils;

namespace gaoxin.common
{
    internal class GaoxinContent : HttpStringContent
    {
        public GaoxinContent(string url) : base(url)
        {
            BuildHeader();
        }

        protected virtual void BuildHeader()
        {
            AddHeader("Host", "wx1936.cnhis.cc");
            AddHeader("Connection", "keep-alive");
            AddHeader("Accept", "application/json, text/plain, */*");
            AddHeader("openId", "");
            AddHeader("userType", "TX");
            AddHeader("User-Agent", "Mozilla/5.0 AppleWebKit/605.1.15 Chrome/81.0.4044.138 Safari/537.36");
            AddHeader("token", MainSession.PlatformSession.GetString("token"));
            AddHeader("Cookie", MainSession.PlatformSession.GetString("Cookie"));
            AddHeader("Referer", "http://wx1936.cnhis.cc/wxcommon/web/");
            AddHeader("Accept-Encoding", "gzip, deflate");
            AddHeader("Accept-Language", "zh-CN,zh-Hans;q=0.9");
        }
    }
}
