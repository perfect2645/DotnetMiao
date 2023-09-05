using HttpProcessor.Content;
using Yongding.login;

namespace Yongding.common
{
    internal class YongdingContent : HttpStringContent
    {
        public YongdingLogin User { get; private set; }

        public YongdingContent(string baseUrl, YongdingLogin user) : base(baseUrl)
        {
            User = user;

            ContentType = "application/x-www-form-urlencoded";
            BuildHeader();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "yiliao2.lefeiniu.com:8081");
            AddHeader("Connection", "keep-alive");
            AddHeader("Origin", "http://yiliao2.lefeiniu.com:8081");
            AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36 NetType/WIFI MicroMessenger/7.0.20.1781(0x6700143B) WindowsWechat(0x63030073)");
            AddHeader("Accept", "*/*");
            AddHeader("Cookie", User.Cookie);
            AddHeader("Referer", "http://yiliao2.lefeiniu.com:8081/");
            AddHeader("Accept-Encoding", "gzip, deflate");
            AddHeader("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
        }
    }
}
