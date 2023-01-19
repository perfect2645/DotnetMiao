using HttpProcessor.Content;
using Shengzhi.login;
using Shengzhi.session;

namespace Shengzhi.common
{
    internal class WechatContent : HttpStringContent
    {
        public ShengzhiLogin User { get; private set; }

        public WechatContent(string url, ShengzhiLogin user) : base(url)
        {
            User = user;
            BuildHeader();
        }

        public WechatContent(string url) : base(url)
        {
            BuildHeader();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "app.quyiyuan.com");
            AddHeader("Connection", "keep-alive");
            AddHeader("Accept", "application/json, text/plain, */*");
            AddHeader("Origin", "https://c.quyiyuan.com");
            AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36 NetType/WIFI MicroMessenger/7.0.20.1781(0x6700143B) WindowsWechat(0x63030073)");
            AddHeader("Sec-Fetch-Site", "same-site");
            AddHeader("Sec-Fetch-Mode", "cors");
            AddHeader("Sec-Fetch-Dest", "empty");
            AddHeader("Referer", "same-site");
            AddHeader("Sec-Fetch-Site", "same-site");

            AddHeader("Referer", "https://c.quyiyuan.com/c_quyiyuan_web/system/c_quyiyuan/index.jsp?wx_forward=home&userSource=0&PublicServiceType=020514&skinType=1&hospitalID=3110012&code=091JvTkl2jEiDa4DxXkl2uwwbR2JvTkw&state=3110012");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
            AddHeader("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
        }
    }
}
