

using HttpProcessor.Content;
using Zhuzher.search;

namespace Zhuzher.Common
{
    internal class ZhuzherContent : HttpStringContent
    {
        public UserProject User { get; set; }

        public ZhuzherContent(string url, UserProject user) : base(url)
        {
            User = user;
            BuildHeader();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "chaos.4009515151.com");
            AddHeader("Zhuzher-Project-Code", User.ProjectCode);
            AddHeader("Accept", "application/json");
            AddHeader("Authorization", User.Authorization);
            AddHeader("Sec-Fetch-Site", "same-site");
            AddHeader("Accept-Language", "zh-CN,zh-Hans;q=0.9");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
            AddHeader("Sec-Fetch-Mode", "cors");
            AddHeader("Origin", "https://enterprise.4009515151.com");
            AddHeader("User-Agent", ZhuzherConstants.UserAgent);
            AddHeader("Referer", "https://enterprise.4009515151.com/");
            AddHeader("Connection", "keep-alive");
            AddHeader("Sec-Fetch-Dest", "empty");
        }
    }
}
