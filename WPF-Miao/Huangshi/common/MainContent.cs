using HttpProcessor.Content;
using Huangshi.login;
using Huangshi.session;

namespace Huangshi.common
{
    internal class MainContent : HttpStringContent
    {
        public HuangshiLogin User { get; private set; }

        public MainContent(string url, HuangshiLogin user) : base(url)
        {
            ContentType = "application/x-www-form-urlencoded";
            User = user;
            BuildHeader();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "gzh.51kys.cn");
            AddHeader("Connection", "keep-alive");
            AddHeader("Accept", "application/json, text/javascript, */*; q=0.01");
            AddHeader("Origin", "http://gzh.51kys.cn");
            AddHeader("X-Requested-With", "XMLHttpRequest");
            AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36 NetType/WIFI MicroMessenger/7.0.20.1781(0x6700143B) WindowsWechat(0x63030073)");
            AddHeader("Cookie", User.Cookie);
            AddHeader("Referer", $"http://gzh.51kys.cn/hssfybjyjkglzx_web/Login/IndexOne");
            AddHeader("Accept-Encoding", "gzip, deflate");
            AddHeader("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
        }
    }
}
