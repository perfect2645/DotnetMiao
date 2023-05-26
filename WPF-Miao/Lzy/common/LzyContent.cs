using HttpProcessor.Content;
using Lzy.login;
using Lzy.session;
using Utils;
using System;

namespace Lzy.common
{
    internal class LzyContent : HttpStringContent
    {
        public LzyLogin User { get; private set; }

        public LzyContent(string baseUrl, LzyLogin user) : base(baseUrl)
        {
            User = user;

            ContentType = "application/x-www-form-urlencoded";
            BuildHeader();
        }

        private void BuildHeader()
        {
            var hosId = MainSession.PlatformSession.GetString(Constants.HospitalId);

            AddHeader("Host", "mix.med.gzhc365.com");
            AddHeader("Connection", "keep-alive");
            AddHeader("Hc-Proj-Info", "project/his-wxapp;type/miniapp;ch/wechat;ver/mix;");
            AddHeader("Hc-Src-Hisid", hosId);
            AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.143 Safari/537.36 MicroMessenger/7.0.9.501 NetType/WIFI MiniProgramEnv/Windows WindowsWechat");
            AddHeader("channel", "wx_xcx");
            AddHeader("client", "patient");
            AddHeader("device-id", string.Empty);
            AddHeader("his-id", hosId);
            AddHeader("request-id", Guid.NewGuid().ToString());
            AddHeader("uid", User.Uid);
            AddHeader("uuid", Guid.NewGuid().ToString());
            AddHeader("Referer", "http://servicewechat.com/wxe7621e2cd9dc36c0/145/page-frame.html");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
        }
    }
}
