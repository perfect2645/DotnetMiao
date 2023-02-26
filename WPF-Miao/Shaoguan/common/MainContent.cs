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
            AddHeader("Host", "hscx.whcdc.org");
            AddHeader("Connection", "keep-alive");
            AddHeader("Origin", "https://hscx.whcdc.org");
            AddHeader("User-Agent", "Mozilla/5.0 AppleWebKit/605.1.15 Chrome/81.0.4044.138 Safari/537.36");
            AddHeader("Accept", "application/json");

            AddHeader("Sec-Fetch-Site", "same-origin");
            AddHeader("Sec-Fetch-Mode", "cors");
            AddHeader("Sec-Fetch-Dest", "empty");
            AddHeader("Referer", $"https://hscx.whcdc.org/vaccine-h5/?code={User.Code}&state=");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
            AddHeader("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
        }
    }
}
