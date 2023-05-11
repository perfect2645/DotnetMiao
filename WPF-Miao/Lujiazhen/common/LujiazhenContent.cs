using HttpProcessor.Content;
using Lujiazhen.login;
using Lujiazhen.session;
using System;
using Utils;

namespace Lujiazhen.common
{
    internal class LujiazhenContent : HttpStringContent
    {
        public LujiazhenLogin User { get; private set; }

        public LujiazhenContent(string baseUrl, LujiazhenLogin user) : base(baseUrl)
        {
            User = user;

            ContentType = "application/x-www-form-urlencoded";
            BuildHeader();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "ljzyyapi.yuanbaodaojia.com");
            AddHeader("Connection", "keep-alive");
            AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.143 Safari/537.36 MicroMessenger/7.0.9.501 NetType/WIFI MiniProgramEnv/Windows WindowsWechat");
            AddHeader("Referer", "https://servicewechat.com/wxcbe627ede7df27a5/1/page-frame.html");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
        }
    }
}
