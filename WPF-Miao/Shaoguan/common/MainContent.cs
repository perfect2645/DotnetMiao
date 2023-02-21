using HttpProcessor.Content;
using Shaoguan.login;
using Shaoguan.session;

namespace Shaoguan.common
{
    internal class MainContent : HttpStringContent
    {
        public ShaoguanLogin User { get; private set; }

        public MainContent(string url, ShaoguanLogin user) : base(url)
        {
            User = user;
            BuildHeader();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "hscx.whcdc.org");
            AddHeader("Connection", "keep-alive");
            AddHeader("Origin", "https://hscx.whcdc.org");
            AddHeader("User-Agent", "Mozilla/5.0 AppleWebKit/605.1.15 Chrome/81.0.4044.138 Safari/537.36");
            AddHeader("Accept", "application/json");
            if (!string.IsNullOrEmpty(User.OpenId))
            {
                AddHeader("openId", User.OpenId);
            }
            AddHeader("token", string.Empty);
            AddHeader("sign", User.Sign);
            AddHeader("idCard", User.IdCardEncode);
            AddHeader("Sec-Fetch-Site", "same-origin");
            AddHeader("Sec-Fetch-Mode", "cors");
            AddHeader("Sec-Fetch-Dest", "empty");
            AddHeader("Referer", $"https://hscx.whcdc.org/vaccine-h5/?code={User.Code}&state=");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
            AddHeader("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
        }
    }
}
