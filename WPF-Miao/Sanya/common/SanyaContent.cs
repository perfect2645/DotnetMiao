using Sanya.login;
using HttpProcessor.Content;
using System;
using Sanya.session;
using Utils;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Sanya.common
{
    internal class SanyaContent : HttpStringContent
    {
        public SanyaLogin User { get; private set; }

        public Dictionary<string, object>  HeaderSignDic { get; private set; }

        public SanyaContent(string baseUrl, SanyaLogin user) : base(baseUrl)
        {
            User = user;
            HeaderSignDic = new Dictionary<string, object>();
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
            HeaderSignDic = Content.ToDictionary(k => k.Key, v => v.Value);

            var appCode = MainSession.PlatformSession.GetString(Constants.AppCode);
            HeaderSignDic.AddOrUpdate(Constants.ZoeUuid, User.ZoeUuid);
            HeaderSignDic.AddOrUpdate(Constants.AppCode, appCode);
            HeaderSignDic.AddOrUpdate(Constants.Token, User.Token);
            HeaderSignDic.AddOrUpdate(Constants.TokenId, User.Token);

            HeaderSignDic = HeaderSignDic.OrderBy(x => x.Key).ToDictionary(k => k.Key, v => v.Value);


            var headerSb = new StringBuilder();
            foreach (var header in HeaderSignDic)
            {
                headerSb.Append(header.Key).Append("=").Append(header.Value);
            }

            var headerSignStr = headerSb.ToString();

            var headerSignMD5 = Encryptor.GetMD5_32(headerSignStr).ToUpper();

            AddHeader("headersign", headerSignMD5);
        }
    }
}
