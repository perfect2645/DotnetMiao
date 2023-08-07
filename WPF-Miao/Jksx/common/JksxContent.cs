using HttpProcessor.Content;
using Jksx.login;
using Jksx.session;
using System;
using Utils;

namespace Jksx.common
{
    internal class JksxContent : HttpStringContent
    {
        public JksxLogin User { get; private set; }

        public JksxContent(string baseUrl, JksxLogin user) : base(baseUrl)
        {
            User = user;

            BuildHeader();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "uhapi.sxyygh.com");
            AddHeader("Connection", "keep-alive");
            AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.143 Safari/537.36 MicroMessenger/7.0.9.501 NetType/WIFI MiniProgramEnv/Windows WindowsWechat");
            AddHeader("channel", "UH_RDSP_HOSPXCX");
            AddHeader("Referer", "https://servicewechat.com/wx2184acfbec0b5f08/290/page-frame.html");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
        }
    }
}
