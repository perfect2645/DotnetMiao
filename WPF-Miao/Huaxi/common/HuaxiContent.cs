using HttpProcessor.Content;
using Huaxi.login;
using Huaxi.session;
using System;
using System.Security.Policy;
using System.Text;
using Utils;
using Utils.datetime;
using Utils.stringBuilder;

namespace Huaxi.common
{
    internal class HuaxiContent : HttpStringContent
    {
        public HuaxiLogin User { get; private set; }
        public string BaseUrl { get; set; }

        public HuaxiContent(string baseUrl, HuaxiLogin user) : base(baseUrl)
        {
            User = user;
            BaseUrl = baseUrl;
            BuildHeader();
        }

        private void BuildHeader()
        {
            AddHeader("Host", $"mcpwxp.motherchildren.com");
            AddHeader("Connection", "keep-alive");
            AddHeader("accessToken", User.AccessToken);
            AddHeader("Accept-Encoding", "gzip,compress,br,deflate");
            AddHeader("User-Agent", "Mozilla/5.0 (iPhone; CPU iPhone OS 15_2_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Mobile/15E148 MicroMessenger/8.0.29(0x18001d38) NetType/4G Language/zh_CN");
            AddHeader("Referer", "https://servicewechat.com/wx38285c6799dac2d1/99/page-frame.html");
        }

        protected virtual void BuildContent()
        {
        }
    }
}
