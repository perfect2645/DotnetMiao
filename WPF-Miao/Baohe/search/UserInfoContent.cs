using HttpProcessor.Content;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Utils;

namespace Baohe.search
{
    internal class UserInfoContent : HttpStringContent
    {
        public UserInfoContent(string url) : base(url)
        {
        }

        public override void BuildDefaultHeaders(HttpClient httpClient)
        {
            AddHeader("Host", "appoint.yihu.com");
            AddHeader("Accept", "application/json, text/javascript, */*; q=0.01");
            AddHeader("Origin", "https://appoint.yihu.com");
            AddHeader("X-Requested-With", "XMLHttpRequest");
            AddHeader("User-Agent", @"Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36");

            AddHeader("Sec-Fetch-Site", "same-origin");
            AddHeader("Sec-Fetch-Mode", "cors");
            AddHeader("Sec-Fetch-Dest", "empty");
            AddHeader("Referer", "https://appoint.yihu.com/appoint/hospital/ghDeptList.html?platformType=9000393&hospitalId=1039346&time=1659972666");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
            AddHeader("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");

            base.BuildDefaultHeaders(httpClient);
        }
    }
}
