

using HttpProcessor.Content;

namespace Zhuzher.Common
{
    internal class ZhuzherContent : HttpStringContent
    {
        public ZhuzherContent(string url) : base(url)
        {
            BuildHeader();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "chaos.4009515151.com");
            AddHeader("X-channel", "zhuzher");
            AddHeader("Zhuzher-Project-Code", "zh-CN,zh-Hans;q=0.9");
            AddHeader("Accept", "*/*");
            AddHeader("Zhuzher-City-Code", "21020012");
            AddHeader("X-Version", "5.3.92");
            AddHeader("Accept-Language", "zh-Hans-CN;q=1.0, ja-JP;q=0.9, en-CN;q=0.8");
            AddHeader("X-API-Version", "20221027");
            AddHeader("X-Platform", "iOS");
            AddHeader("Connection", "keep-alive");
            AddHeader("Zhuzher-Project-Role", "0");
        }
    }
}
