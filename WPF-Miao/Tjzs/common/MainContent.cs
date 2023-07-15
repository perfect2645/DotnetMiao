using HttpProcessor.Content;

namespace Tjzs.common
{
    internal class TjzsContent : HttpStringContent
    {
        public TjzsContent(string url) : base(url)
        {
            ContentType = "application/json";
            BuildHeader();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "health-cn.xyz:9033");
            AddHeader("Origin", "https://health-cn.xyz");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
            AddHeader("Connection", "keep-alive");
            AddHeader("Accept", "application/json, text/plain, */*");
            AddHeader("User-Agent", "Mozilla/5.0 (iPhone; CPU iPhone OS 15_2_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Mobile/15E148 MicroMessenger/8.0.29(0x18001d38) NetType/4G Language/zh_CN");
            AddHeader("Referer", $"https://health-cn.xyz/");
            AddHeader("Accept-Language", "zh-CN,zh-Hans;q=0.9");
        }
    }
}
