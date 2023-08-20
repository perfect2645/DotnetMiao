using HttpProcessor.Content;
using Lujiazhen.login;
using Lujiazhen.session;
using System;
using System.Security.Policy;
using Utils;

namespace Lujiazhen.common
{
    internal class LujiazhenContent : HttpStringContent
    {
        public LujiazhenLogin User { get; private set; }
        public string BaseUrl { get; set; }

        public LujiazhenContent(string baseUrl, LujiazhenLogin user) : base(baseUrl)
        {
            User = user;
            BaseUrl = baseUrl;
            ContentType = "application/x-www-form-urlencoded";
            BuildHeader();
        }

        protected void BuildUrl()
        {
            var appPrefix = MainSession.PlatformSession.GetString(Constants.AppPrefix);
            RequestUrl = $"https://{appPrefix}{BaseUrl}";

        }

        private void BuildHeader()
        {
            var appPrefix = MainSession.PlatformSession.GetString(Constants.AppPrefix);
            AddHeader("Host", $"{appPrefix}.yuanbaodaojia.com");
            AddHeader("Connection", "keep-alive");
            AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.143 Safari/537.36 MicroMessenger/7.0.9.501 NetType/WIFI MiniProgramEnv/Windows WindowsWechat");
            AddHeader("Referer", "https://servicewechat.com/wxcbe627ede7df27a5/2/page-frame.html");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
        }
    }
}
