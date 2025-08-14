

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
            AddHeader("Accept", "application/json, application/json");
            AddHeader("Origin", "https://enterprise.4009515151.com");
            AddHeader("User-Agent", ZhuzherConstants.UserAgent);
            AddHeader("Referer", "https://enterprise.4009515151.com/");
            AddHeader("Connection", "keep-alive");
            AddHeader("Sec-Fetch-Dest", "empty");
        }

        public void AddDeviceId()
        {
            AddHeader("X-Device-ID", User.DeviceId);
        }
    }
}