

using HttpProcessor.Content;
using Zhuzher.search;

namespace Zhuzher.Common
{
    internal class OnewoContent : HttpStringContent
    {
        public UserProject User { get; set; }

        public OnewoContent(string url, UserProject user) : base(url)
        {
            User = user;
            BuildHeader();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "z.onewo.com");
            AddHeader("Authorization", User.Authorization);
            AddHeader("Sec-Fetch-Site", "cross-site");
            AddHeader("Accept-Language", "zh-CN,zh-Hans;q=0.9");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
            AddHeader("Sec-Fetch-Mode", "cors");
            AddHeader("Accept", "*/*");
            AddHeader("Origin", "https://enterprise.4009515151.com");
            AddHeader("User-Agent", "Mozilla/5.0 (iPhone; CPU iPhone OS 17_0_3 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Mobile/15E148 xxxx2023101701 vanke_app_version/5.5.40 X_API_VERSION/20231017 vanke_app/zhuzher vanke_jsbridge_version/5.5.40");
            AddHeader("Referer", "https://enterprise.4009515151.com/");
            AddHeader("Connection", "keep-alive");
            AddHeader("Sec-Fetch-Dest", "empty");
        }
    }
}