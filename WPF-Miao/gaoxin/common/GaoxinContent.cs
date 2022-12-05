using HttpProcessor.Content;
using gaoxin.session;
using Utils;

namespace gaoxin.common
{
    internal class GaoxinContent : HttpStringContent
    {
        public string Description { get; set; }
        public GaoxinContent(string url, string description) : base(url)
        {
            Description = description;
            BuildHeader();
        }

        protected virtual void BuildHeader()
        {
            AddHeader("Host", "ymglfw.care4u.cn");
            AddHeader("Connection", "keep-alive");
            AddHeader("Accept", "application/json, text/plain, */*");
            AddHeader("openId", "");
            AddHeader("userType", "TX");
            AddHeader("User-Agent", "Mozilla/5.0 AppleWebKit/605.1.15 Chrome/81.0.4044.138 Safari/537.36");
            AddHeader("token", MainSession.Token);
            AddHeader("Referer", "https://servicewechat.com/wx4293c7bbe5d3df87/8/page-frame.html");
            AddHeader("Accept-Encoding", "gzip, deflate");
        }
    }
}
