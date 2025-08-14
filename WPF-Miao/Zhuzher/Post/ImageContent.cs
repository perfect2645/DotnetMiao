using HttpProcessor.Content;
using System;

namespace Zhuzher.Post
{
    internal class ImageContent : HttpStringContent
    {
        public ImageContent(string url) : base(url)
        {
            BuildHearders();
        }

        private void BuildHearders()
        {
            AddHeader("Host", "vanke-app.oss-cn-shenzhen.aliyuncs.com");
            AddHeader("Sec-Fetch-Site", "cross-site");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
            AddHeader("Connection", "keep-alive");
            AddHeader("Sec-Fetch-Mode", "no-cors");
            AddHeader("Accept", "image/webp,image/avif,image/jxl,image/heic,image/heic-sequence,video/*;q=0.8,image/png,image/svg+xml,image/*;q=0.8,*/*;q=0.5");
            AddHeader("User-Agent", "Mozilla/5.0 (iPhone; CPU iPhone OS 18_0_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Mobile/15E148 xxxx2024111302 vanke_app_version/5.6.80 X_API_VERSION/20241113 vanke_app/zhuzher vanke_jsbridge_version/5.6.80");
            AddHeader("Referer", "https://enterprise.4009515151.com/");
            AddHeader("Sec-Fetch-Dest", "image");
            AddHeader("Accept-Language", "zh-CN,zh-Hans;q=0.9");

        }
    }
}
