using HttpProcessor.Content;
using HosFour.login;
using HosFour.session;
using Utils;

namespace HosFour.common
{
    internal class HosFourContent : HttpStringContent
    {
        public HosFourLogin User { get; private set; }

        public HosFourContent(string baseUrl, HosFourLogin user) : base(baseUrl)
        {
            var prifix = MainSession.PlatformSession.GetString(Constants.HospitalPrefix);
            RequestUrl = $"https://{prifix}.{baseUrl}";
            User = user;
            BuildHeader();
        }

        private void BuildHeader()
        {
            var prifix = MainSession.PlatformSession.GetString(Constants.HospitalPrefix);
            AddHeader("Host", $"{prifix}.ldrmyy120.com");
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
