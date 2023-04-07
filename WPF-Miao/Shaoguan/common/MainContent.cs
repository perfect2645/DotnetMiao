using HttpProcessor.Content;
using Huangshi.Encrypt;
using Shaoguan.login;
using Shaoguan.session;
using System;
using Utils.datetime;

namespace Shaoguan.common
{
    internal class MainContent : HttpStringContent
    {
        public ShaoguanLogin User { get; private set; }

        public MainContent(string baseUrl, ShaoguanLogin user) : base(baseUrl)
        {
            ContentType = "text/plain";
            User = user;
            BuildUrl(baseUrl);
            BuildHeader();
        }

        private void BuildUrl(string baseUrl)
        {
            var timestamp = DateTimeUtil.GetTimeStampLong();
            var sign = new Signature(Constants.MachineCode, timestamp, User.Token);

            var signJson = sign.SignJson;
            var signature = JsReader.GetEncodeString(signJson);

            RequestUrl = $"{baseUrl}{signature}";
        }

        private void BuildHeader()
        {
            AddHeader("Host", "qjfy-adm-prod.hosp-proxy.cksoft.tech");
            AddHeader("Origin", "http://qjfy-h5.hosp-proxy.cksoft.tech");
            AddHeader("Accept-Encoding", "gzip, deflate");
            AddHeader("Connection", "keep-alive");
            AddHeader("Accept", "application/json, text/plain, */*");
            AddHeader("User-Agent", "Mozilla/5.0 (iPhone; CPU iPhone OS 15_2_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Mobile/15E148 MicroMessenger/8.0.29(0x18001d38) NetType/4G Language/zh_CN");
            AddHeader("Referer", $"http://qjfy-h5.hosp-proxy.cksoft.tech/");
            AddHeader("Accept-Language", "zh-CN,zh-Hans;q=0.9");
        }
    }
}
