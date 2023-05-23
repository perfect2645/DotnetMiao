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
            AddHeader("Host", "bzjk.qiyingtian.com");
            AddHeader("Connection", "keep-alive");
            AddHeader("Accept", "*/*");
            AddHeader("Origin", "https://bzjk.qiyingtian.com");
            AddHeader("X-Requested-With", "XMLHttpRequest");
            AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36 NetType/WIFI MicroMessenger/7.0.20.1781(0x6700143B) WindowsWechat(0x63030073)");
            AddHeader("Cookie", User.Cookie);
            AddHeader("Referer", "https://servicewechat.com/wx9462592068e4073e/5/page-frame.html");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
        }
    }
}
