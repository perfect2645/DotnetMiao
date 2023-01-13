using HttpProcessor.Content;
using Tianhe.login;
using Tianhe.session;

namespace Tianhe.common
{
    internal class TianheContent : HttpStringContent
    {
        public TianheLogin User { get; private set; }

        public TianheContent(string url, TianheLogin user) : base(url)
        {
            User = user;
            BuildHeader();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "ldsq.ldrmyy120.com");
            AddHeader("Connection", "keep-alive");
            AddHeader("Authorization", User.Authorization);
            AddHeader("Cookie", User.Cookie);
            AddHeader("Accept-Encoding", "gzip, deflate, br");
            AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.143 Safari/537.36 MicroMessenger/7.0.9.501 NetType/WIFI MiniProgramEnv/Windows WindowsWechat");
            AddHeader("Referer", "https://servicewechat.com/wxe78593328cbc7561/2/page-frame.html");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
        }
    }
}
