using HttpProcessor.Content;
using Jikong.login;
using Jikong.session;

namespace Jikong.common
{
    internal class JikongContent : HttpStringContent
    {
        public JikongLogin User { get; private set; }

        public JikongContent(string url, JikongLogin user) : base(url)
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
            AddHeader("User-Agent", "Mozilla/5.0 AppleWebKit/605.1.15 Chrome/81.0.4044.138 Safari/537.36");
            AddHeader("Referer", "https://servicewechat.com/wxe78593328cbc7561/2/page-frame.html");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
        }
    }
}
