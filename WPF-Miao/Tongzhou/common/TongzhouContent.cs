using HttpProcessor.Content;
using System.Collections.Generic;
using Tongzhou.login;
using Tongzhou.session;
using Utils;

namespace Tongzhou.common
{
    internal class TongzhouContent : HttpStringContent
    {
        public TongzhouLogin User { get; private set; }

        public TongzhouContent(TongzhouLogin user) : base(null)
        {
            User = user;

            RequestUrl = $"https://weixin.ngarihealth.com/weixin/wx/mp/{user.WxId}/gateway";
            ContentType = "application/json";
            BuildHeader();
        }

        public TongzhouContent(string baseUrl, TongzhouLogin user) : base(baseUrl)
        {
            User = user;

            ContentType = "application/json";
            BuildHeader();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "weixin.ngarihealth.com");
            AddHeader("Connection", "keep-alive");
            AddHeader("Origin", "https://weixin.ngarihealth.com");
            AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36 NetType/WIFI MicroMessenger/7.0.20.1781(0x6700143B) WindowsWechat(0x63030073)");
            AddHeader("Accept", "*/*");
            AddHeader("Sec-Fetch-Site", "same-origin");
            AddHeader("Sec-Fetch-Mode", "cors");
            AddHeader("Sec-Fetch-Dest", "empty");
            AddHeader("Cookie", User.Cookie);
            AddHeader("Referer", User.Referer);
            AddHeader("Accept-Encoding", "gzip, deflate, br");
            AddHeader("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
        }
    }
}
