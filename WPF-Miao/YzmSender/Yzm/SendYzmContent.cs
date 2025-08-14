using HttpProcessor.Content;
using Utils;
using Utils.number;
using YzmSender.session;

namespace YzmSender.search
{
    internal class SendYzmContent : HttpStringContent
    {

        public SendYzmContent() : base(Constants.BaseUrl)
        {
            BuildUrl();
            BuildHeaders();
        }

        protected void BuildUrl()
        {
            var baseUrl = MainSession.PlatformSession.GetString(Constants.BaseUrl);
            var randomPhone = NumberUtil.GetRandomPhone();
            var url = $"{baseUrl}phone={randomPhone}";
            RequestUrl = url ;
        }

        private void BuildHeaders()
        {
            AddHeader("Host", "dxi.glzhealth.com");
            AddHeader("Connection", "keep-alive");
            AddHeader("Accept", "application/json, text/plain, */*");
            AddHeader("xweb_xhr", "1");
            AddHeader("source", "10002");
            AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/107.0.0.0 Safari/537.36 MicroMessenger/7.0.20.1781(0x6700143B) NetType/WIFI MiniProgramEnv/Windows WindowsWechat/WMPF WindowsWechat(0x6309080f)XWEB/8461");
            AddHeader("Sec-Fetch-Site", "cross-site");
            AddHeader("Sec-Fetch-Mode", "cors");
            AddHeader("Sec-Fetch-Dest", "empty");
            AddHeader("Referer", "https://servicewechat.com/wxfb0f7df5d9bffade/143/page-frame.html");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
            AddHeader("Accept-Language", "zh-CN,zh;q=0.9");
        }

    }
}
