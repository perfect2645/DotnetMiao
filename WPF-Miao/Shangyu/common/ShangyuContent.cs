using HttpProcessor.Content;
using Shangyu.login;
using Shangyu.session;
using Utils;
using System;

namespace Shangyu.common
{
    internal class ShangyuContent : HttpStringContent
    {
        public ShangyuLogin User { get; private set; }
        protected string BaseUrl { get; private set; }

        public ShangyuContent(string baseUrl, ShangyuLogin user) : base(baseUrl)
        {
            var hospitalCode = MainSession.PlatformSession.GetString(Constants.HospitalId);
            BaseUrl = $"{baseUrl}{hospitalCode}";
            User = user;

            BuildHeader();
        }

        private void BuildHeader()
        {
            var hosId = MainSession.PlatformSession.GetString(Constants.HospitalId);

            AddHeader("Host", "app.gocent.com.cn");
            AddHeader("Connection", "keep-alive");
            AddHeader("Access-Control-Allow-Origin", "*");
            AddHeader("Accept", "application/json, text/plain, */*");
            AddHeader("Origin", "http://app.gocent.com.cn");
            AddHeader("Authorization", User.Authorization);
            AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36 NetType/WIFI MicroMessenger/7.0.20.1781(0x6700143B) WindowsWechat(0x63030073)");
            AddHeader("Sec-Fetch-Site", "cross-site");
            AddHeader("Sec-Fetch-Mode", "cors");
            AddHeader("Sec-Fetch-Dest", "empty");
            AddHeader("Referer", "http://app.gocent.com.cn/unite/index.aspx");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
            AddHeader("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
        }
    }
}
