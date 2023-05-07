using HttpProcessor.Content;
using HosTwo.login;
using HosTwo.session;
using Utils;

namespace HosTwo.common
{
    internal class HosTwoContent : HttpStringContent
    {
        public HosTwoLogin User { get; private set; }

        public HosTwoContent(string baseUrl, HosTwoLogin user) : base(baseUrl)
        {
            User = user;

            ContentType = "application/x-www-form-urlencoded";
            BuildHeader();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "fwcs.linkingcloud.cn");
            AddHeader("Connection", "keep-alive");
            AddHeader("Origin", "https://fwcs.linkingcloud.cn");
            AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36 NetType/WIFI MicroMessenger/7.0.20.1781(0x6700143B) WindowsWechat(0x63030073)");
            AddHeader("Accept", "*/*");
            AddHeader("Sec-Fetch-Site", "same-origin");
            AddHeader("Sec-Fetch-Mode", "cors");
            AddHeader("Sec-Fetch-Dest", "empty");
            AddHeader("Referer", "https://fwcs.linkingcloud.cn/App/yuyue/index.html");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
            AddHeader("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");

            AddHeader("Cookie", User.Cookie);
        }
    }
}
