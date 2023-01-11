using HttpProcessor.Content;
using Xihongmen.login;
using Xihongmen.session;

namespace Xihongmen.common
{
    internal class XhmContent : HttpStringContent
    {
        public const string Key = "9b4fdBbFyGmrXQ1BmBQIjLAcmNbMfmQPg%2BIcLHe4G%2BKCdDc";

        public XhmLogin User { get; private set; }

        public XhmContent(string url, XhmLogin user) : base(url)
        {
            User = user;
            ContentType = "application/x-www-form-urlencoded";
            BuildHeader();
        }

        public XhmContent(string url) : base(url)
        {
            ContentType = "application/x-www-form-urlencoded";
            BuildHeader();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "yiyuan.dabannet.cn");
            AddHeader("Connection", "keep-alive");
            AddHeader("Origin", "https://yiyuan.dabannet.cn");

            
            AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36 NetType/WIFI MicroMessenger/7.0.20.1781(0x6700143B) WindowsWechat(0x63030073)");
            AddHeader("Accept", "*/*");
            AddHeader("Sec-Fetch-Site", "same-origin");
            AddHeader("Sec-Fetch-Mode", "cors");
            AddHeader("Sec-Fetch-Dest", "empty");
            AddHeader("Referer", "https://yiyuan.dabannet.cn/h5/");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
            AddHeader("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
            if (!string.IsNullOrEmpty(User?.Cookie))
            {
                AddHeader("Cookie", User?.Cookie);
            }
        }
    }
}
