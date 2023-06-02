using Dayim.login;
using HttpProcessor.Content;

namespace Dayim.common
{
    internal class DayimContent : HttpStringContent
    {
        public DayimLogin User { get; private set; }

        public DayimContent(string baseUrl, DayimLogin user) : base(baseUrl)
        {
            User = user;

            BuildHeader();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "dm.cdpc.org.cn");
            AddHeader("Connection", "keep-alive");
            AddHeader("dmxcxToken", User.DmxcxToken);
            AddHeader("User-Agent", "Mozilla/5.0 (iPhone; CPU iPhone OS 16_3_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Mobile/15E148 MicroMessenger/8.0.37(0x1800252c) NetType/4G Language/zh_CN");
            AddHeader("Referer", "https://servicewechat.com/wx51500ac89206cb24/4/page-frame.html");
            AddHeader("Accept-Encoding", "gzip,compress,br,deflate");
        }
    }
}
