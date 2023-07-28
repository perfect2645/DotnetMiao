using Sanya.login;
using HttpProcessor.Content;
using System;
using Sanya.session;
using Utils;

namespace Sanya.common
{
    internal class SanyaContent : HttpStringContent
    {
        public SanyaLogin User { get; private set; }

        public SanyaContent(string baseUrl, SanyaLogin user) : base(baseUrl)
        {
            User = user;

            BuildHeader();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "public.health.zoenet.cn");
            AddHeader("Connection", "keep-alive");

            var uuid = Guid.NewGuid().ToString().Replace("-", string.Empty);

            AddHeader("noncePf", uuid);
            AddHeader("level", string.Empty);
            AddHeader("Origin", "https://public.health.zoenet.cn");
            AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36 NetType/WIFI MicroMessenger/7.0.20.1781(0x6700143B) WindowsWechat(0x63030073)");
            AddHeader("node", "open");
            AddHeader("Accept", "*/*");
            AddHeader("Cookie", User.Cookie);
            AddHeader("Sec-Fetch-Site", "same-origin");
            AddHeader("Sec-Fetch-Mode", "cors");
            AddHeader("Sec-Fetch-Dest", "empty");

            var zoneCode = MainSession.PlatformSession.GetString(Constants.ZoneCode);
            var zoneName = MainSession.PlatformSession.GetString(Constants.ZoneName);

            AddHeader("Referer", $"https://public.health.zoenet.cn/medical/appoint/select-hospital?entryName=appoint&zoneCode={zoneCode}&zoneName={zoneName}&platformSingleHospital=1&title=6aKE57qm5oyC5Y+3");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
            AddHeader("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
        }

        protected virtual void BuildHeaderSign()
        {
            AddHeader("headersign", "");
        }
    }
}
