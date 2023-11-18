using HttpProcessor.Content;
using Rika.login;
using Rika.session;
using System;
using System.Security.Policy;
using System.Text;
using Utils;
using Utils.datetime;
using Utils.stringBuilder;

namespace Rika.common
{
    internal class RikaContent : HttpStringContent
    {
        public RikaLogin User { get; private set; }
        public string BaseUrl { get; set; }

        public RikaContent(string baseUrl, RikaLogin user) : base(baseUrl)
        {
            User = user;
            BaseUrl = baseUrl;
            BuildHeader();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "pyx.ygnetservice.cn");
            AddHeader("Connection", "keep-alive");
            AddHeader("Accept", "*/*");
            AddHeader("User-Agent", "User-Agent: Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36 NetType/WIFI MicroMessenger/7.0.20.1781(0x6700143B) WindowsWechat(0x63030073)");
            AddHeader("X-Requested-With", "XMLHttpRequest");
            AddHeader("Cookie", User.Cookie);
            AddHeader("Referer", "http://pyx.ygnetservice.cn/Wxgzh_Hospital//GzhHospitalYwService_JYFW/FUN_JYFW_YYGH_YS.action?openid=o0u9u6pNQfh7XfKLS9M4zHA6iwlE&clientID=A0000005&ksID=015&ksMC=%E8%AE%A1%E5%88%92%E5%85%8D%E7%96%AB%E7%A7%91%E9%97%A8%E8%AF%8A");
            AddHeader("Accept-Encoding", "gzip, deflate");
            AddHeader("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
        }
    }
}
