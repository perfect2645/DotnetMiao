using HttpProcessor.Content;
using Lzy.login;
using Lzy.session;
using Utils;
using System;

namespace Lzy.common
{
    internal class LzyContent : HttpStringContent
    {
        public LzyLogin User { get; private set; }

        public LzyContent(string baseUrl, LzyLogin user) : base(baseUrl)
        {
            User = user;

            ContentType = "application/x-www-form-urlencoded";
            BuildHeader();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "wechat.yunhebj.com");
            AddHeader("Connection", "keep-alive");
            AddHeader("Cache-Control", "max-age=0");
            AddHeader("Upgrade-Insecure-Requests", "1");
            AddHeader("Origin", "http://wechat.yunhebj.com");
            AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36 NetType/WIFI MicroMessenger/7.0.20.1781(0x6700143B) WindowsWechat(0x63030073)");
            AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
            AddHeader("Cookie", User.Cookie);

            var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);
            AddHeader("Referer", $"http://wechat.yunhebj.com/app/index.php?i=30&c=entry&do=course&m=lzl_course&id={deptId}&date=2023-06-03&week=6");
            AddHeader("Accept-Encoding", "gzip, deflate");
            AddHeader("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
        }
    }
}
