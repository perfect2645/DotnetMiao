using Sxjk.login;
using HttpProcessor.Content;

namespace Sxjk.common
{
    internal class SxjkContent : HttpStringContent
    {
        public SxjkLogin User { get; private set; }

        public SxjkContent(string baseUrl, SxjkLogin user) : base(baseUrl)
        {
            User = user;

            BuildHeader();
        }

        protected virtual void BuildHeader()
        {
            AddHeader("Host", "ymjz.sxcdc.cn");
            AddHeader("Connection", "keep-alive");
            AddHeader("Accept", "application/json, text/plain, */*");
            AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36 NetType/WIFI MicroMessenger/7.0.20.1781(0x6700143B) WindowsWechat(0x63030073)");
            AddHeader("Cookie", User.Cookie);
            AddHeader("Sec-Fetch-Site", "same-origin");
            AddHeader("Sec-Fetch-Mode", "cors");
            AddHeader("Sec-Fetch-Dest", "empty");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
            AddHeader("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
        }
    }
}
