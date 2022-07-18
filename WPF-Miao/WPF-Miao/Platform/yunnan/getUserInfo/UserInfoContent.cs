using HttpProcessor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Miao.Platform.yunnan.viewModel;

namespace WPF_Miao.Platform.yunnan.getUserInfo
{
    public class UserInfoContent : HttpClientContentBase
    {
        public UserInfoContent()
        {
            BuildDefaultHeaders();
        }

        #region Headers

        private void BuildDefaultHeaders()
        {
            HttpRequestMessage.Headers.Add("Host", "weixin.ngarihealth.com");
            HttpRequestMessage.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            HttpRequestMessage.Headers.Add("Sec-Fetch-Site", "same-origin");
            HttpRequestMessage.Headers.Add("Sec-Fetch-Mode", "cors");
            HttpRequestMessage.Headers.Add("Sec-Fetch-Dest", "empty");
            HttpRequestMessage.Headers.Add("User-Agent", @"Mozilla/5.0 (iPhone; CPU iPhone OS 15_2_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Mobile/15E148 Language/zh_CN");
            HttpRequestMessage.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            HttpRequestMessage.Headers.Add("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
            HttpRequestMessage.Headers.Add("Accept", "*/*");
        }

        #endregion Headers

        #region Request

        private void BuildSessionHeaders(ISessionItem sessionItem)
        {
            HttpRequestMessage.Headers.Add("Cookie", sessionItem.Cookie);
            HttpRequestMessage.Headers.Add("Referer", sessionItem.Referer);
        }

        #endregion Request
    }
}
