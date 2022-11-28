using HttpProcessor.Content;
using jieyang.session;

namespace jieyang.common
{
    internal class jieyangBaseContent : HttpStringContent
    {
        public jieyangBaseContent(string url) : base(url)
        {
            BuildHeader();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "wx1936.cnhis.cc");
            AddHeader("Connection", "keep-alive");
            AddHeader("Accept", "application/json, text/plain, */*");
            AddHeader("openId", "");
            AddHeader("userType", "TX");
            AddHeader("User-Agent", "Mozilla/5.0 AppleWebKit/605.1.15 Chrome/81.0.4044.138 Safari/537.36");
            AddHeader("token", "E8F0F87646FAEFE62B54A807B04E8093714741CDBB08E107D7B52BE9B6C5F5DBB81F0534394ECDCD3645ED1111F8DE11");
            AddHeader("Cookie", "SESSION=ZTUxNTdlMDctNjVjZC00ZmI1LWFmMWEtNGU0MmJhYTk3NzYz");
            AddHeader("Referer", "http://wx1936.cnhis.cc/wxcommon/web/");
            AddHeader("Accept-Encoding", "gzip, deflate");
            AddHeader("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
        }
    }
}
