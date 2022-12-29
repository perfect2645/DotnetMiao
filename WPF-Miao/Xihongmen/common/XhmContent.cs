using HttpProcessor.Content;
using Xihongmen.session;

namespace Xihongmen.common
{
    internal class XhmContent : HttpStringContent
    {
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
            AddHeader("User-Agent", "Mozilla/5.0 AppleWebKit/605.1.15 Chrome/81.0.4044.138 Safari/537.36");
            AddHeader("Accept", "*/*");
            AddHeader("Sec-Fetch-Site", "same-origin");
            AddHeader("Sec-Fetch-Mode", "cors");
            AddHeader("Sec-Fetch-Dest", "empty");
            AddHeader("Referer", "https://yiyuan.dabannet.cn/h5/");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
            AddHeader("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
        }
    }
}
