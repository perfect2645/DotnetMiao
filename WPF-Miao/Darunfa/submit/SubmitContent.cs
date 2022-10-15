using Darunfa.session;
using HttpProcessor.Content;

namespace Darunfa.submit
{
    internal class SubmitContent : HttpStringContent
    {
        public SubmitContent(string url) : base(url)
        {
            ContentType = "application/x-www-form-urlencoded";
            BuildHeader();
            BuildContent();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "yx.feiniu.com");
            AddHeader("Connection", "keep-alive");
            AddHeader("User-Agent", "Mozilla/5.0 AppleWebKit/605.1.15 Chrome/81.0.4044.138 Safari/537.36");
            AddHeader("Referer", "https://servicewechat.com/wx08cc6bd15fabfa53/84/page-frame.html");
            AddHeader("Accept-Encoding", "gzip,compress,br,deflate");
        }

        private void BuildContent()
        {
            AddContent("apiVersion", "t141");
            AddContent("appVersion", "1.5.1");
            AddContent("areaCode", "CS000016");
            AddContent("channel", "online");
            AddContent("clientid", "a7ea53059fc868e2e3e2dd7c04027035");
        }
    }
}
