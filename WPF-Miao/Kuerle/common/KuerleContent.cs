using HttpProcessor.Content;
using Kuerle.login;
using Kuerle.session;
using System;
using Utils;

namespace Kuerle.common
{
    internal class KuerleContent : HttpStringContent
    {
        public KuerleLogin User { get; private set; }

        public KuerleContent(string baseUrl, KuerleLogin user) : base(baseUrl)
        {
            User = user;
            BuildHeader();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "yuyue.azjkzx.cn");
            AddHeader("Connection", "keep-alive");
            AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.143 Safari/537.36 MicroMessenger/7.0.9.501 NetType/WIFI MiniProgramEnv/Windows WindowsWechat");
            AddHeader("Referer", "https://servicewechat.com/wx9462592068e4073e/5/page-frame.html");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
        }
    }
}
