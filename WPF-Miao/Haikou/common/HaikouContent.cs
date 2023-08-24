using HttpProcessor.Content;
using Haikou.login;
using Haikou.session;
using System;
using System.Security.Policy;
using Utils;

namespace Haikou.common
{
    internal class HaikouContent : HttpStringContent
    {
        public HaikouLogin User { get; private set; }
        public string BaseUrl { get; set; }

        public HaikouContent(string baseUrl, HaikouLogin user) : base(baseUrl)
        {
            User = user;
            BuildHeader();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "wx.hospite.com");
            AddHeader("Connection", "keep-alive");
            AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.143 Safari/537.36 MicroMessenger/7.0.9.501 NetType/WIFI MiniProgramEnv/Windows WindowsWechat");
            AddHeader("token", User.Token);
            AddHeader("Referer", "https://servicewechat.com/wx6583f3ef203bd236/65/page-frame.html");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
        }
    }
}
