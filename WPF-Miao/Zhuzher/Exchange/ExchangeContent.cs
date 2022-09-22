using HttpProcessor.Content;
using Utils.datetime;

namespace Zhuzher.Exchange
{
    internal class ExchangeContent : HttpStringContent
    {
        public ExchangeContent(string url) : base(url)
        {
            BuildHeader();
            BuildContent();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "chaos.4009515151.com");
            AddHeader("Origin", "https://enterprise.4009515151.com");
            AddHeader("Accept-Language", "zh-CN,zh-Hans;q=0.9");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
            AddHeader("Connection", "keep-alive");
            AddHeader("Accept", "application/json");
            AddHeader("User-Agent", "Mozilla/5.0 AppleWebKit/605.1.15 vanke_app_version/5.3.92 X_API_VERSION/20220902 vanke_app/zhuzher vanke_jsbridge_version/5.3.92");
            AddHeader("Referer", "https://enterprise.4009515151.com/");

        }

        private void BuildContent()
        {
            AddContent("activityId", 290);
        }
    }
}
