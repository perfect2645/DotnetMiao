using HttpProcessor.Content;
using jinyinhu.session;
using Utils;

namespace jinyinhu.common
{
    internal class JinyinhuContent : HttpStringContent
    {
        public JinyinhuContent(string url) : base(url)
        {
            BuildHeader();
        }

        protected virtual void BuildHeader()
        {
            AddHeader("Host", "101.34.141.250:9653");
            AddHeader("Connection", "keep-alive");
            AddHeader("Origin", "http://yy.test.shinegosoft.com.cn");
            AddHeader("Authori-zation", MainSession.PlatformSession.GetString("Authori-zation"));
            AddHeader("User-Agent", "Mozilla/5.0 AppleWebKit/605.1.15 Chrome/81.0.4044.138 Safari/537.36");
            AddHeader("Accept", "*/*");

            var userId = MainSession.PlatformSession.GetString(Constants.UserId);
            var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);
            AddHeader("Referer", $"http://yy.test.shinegosoft.com.cn/mobile/pages/business/business?id={userId}&type={deptId}");
            AddHeader("Accept-Encoding", "gzip, deflate");
            AddHeader("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
        }
    }
}
