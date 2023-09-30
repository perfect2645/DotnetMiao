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
            ContentType = "application/x-www-form-urlencoded";
            BuildHeader();
        }

        protected void BuildUrl()
        {
            var appPrefix = MainSession.PlatformSession.GetString(Constants.AppPrefix);
            RequestUrl = $"https://{appPrefix}{BaseUrl}";

        }

        private void BuildHeader()
        {
            var appPrefix = MainSession.PlatformSession.GetString(Constants.AppPrefix);
            AddHeader("Host", $"{appPrefix}.yuanbaodaojia.com");
            AddHeader("Connection", "keep-alive");
            AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.143 Safari/537.36 MicroMessenger/7.0.9.501 NetType/WIFI MiniProgramEnv/Windows WindowsWechat");
            AddHeader("Referer", "https://servicewechat.com/wxcbe627ede7df27a5/2/page-frame.html");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
        }

        protected virtual void BuildContent()
        {
            BuildSign();
        }

        protected virtual void BuildSign()
        {
            var sb = new StringBuilder();
            foreach(var content in Content)
            {
                sb.Append("&");
                sb.Append(content.Key);
                sb.Append("=");
                sb.Append(content.Value.NotNullString());
            }

            sb.Append("&key=8C81915139AA5CCC160A0AC9168FF2C4");
            var signStr = sb.ToString().TrimStart('&');
            var signEncode = Encryptor.GetMD5_32(signStr);

            Content.AddOrUpdate("tokenSign", signEncode);
        }
    }
}
