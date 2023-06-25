using Puzhou.login;
using HttpProcessor.Content;

namespace Puzhou.common
{
    internal class PuzhouContent : HttpStringContent
    {
        public GzjkLogin User { get; private set; }

        public PuzhouContent(string baseUrl, GzjkLogin user) : base(baseUrl)
        {
            User = user;

            BuildHeader();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "zsyy.wzlwpz.nbnfsoft.com:7073");
            AddHeader("Connection", "keep-alive");
            AddHeader("Accept", "application/json, text/plain, */*");
            AddHeader("Authorization", User.Authorization);
            AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36 NetType/WIFI MicroMessenger/7.0.20.1781(0x6700143B) WindowsWechat(0x63030073)");
            AddHeader("Cookie", $"token=%22{User.Authorization}%22");
            AddHeader("Sec-Fetch-Site", "same-origin");
            AddHeader("Sec-Fetch-Mode", "cors");
            AddHeader("Sec-Fetch-Dest", "empty");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
            AddHeader("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
        }
    }
}
