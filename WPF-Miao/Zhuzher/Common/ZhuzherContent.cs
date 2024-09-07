

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
            AddHeader("User-Agent", "Mozilla/5.0 (iPhone; CPU iPhone OS 17_6_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Mobile/15E148 xxxx2024090202 vanke_app_version/5.6.57 X_API_VERSION/20240902 vanke_app/zhuzher vanke_jsbridge_version/5.6.57");
            AddHeader("Referer", "https://enterprise.4009515151.com/");
            AddHeader("Connection", "keep-alive");
            AddHeader("Sec-Fetch-Dest", "empty");
        }
    }
}
