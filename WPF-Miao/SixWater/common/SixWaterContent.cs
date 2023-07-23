using HttpProcessor.Content;
using SixWater.login;

namespace SixWater.common
{
    internal class SixWaterContent : HttpStringContent
    {
        public SixWaterLogin User { get; private set; }

        public SixWaterContent(string baseUrl, SixWaterLogin user) : base(baseUrl)
        {
            ContentType = "application/json";
            User = user;
            BuildHeader();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "oss.zsqrmyy.com:8443");
            AddHeader("Connection", "keep-alive");
            AddHeader("Authorization", User.Authorization);
            AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.143 Safari/537.36 MicroMessenger/7.0.9.501 NetType/WIFI MiniProgramEnv/Windows WindowsWechat");
            AddHeader("X-Request-Uuid", User.UUid);
            AddHeader("Referer", "https://servicewechat.com/wxf07924550288e202/18/page-frame.html");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
        }
    }
}
