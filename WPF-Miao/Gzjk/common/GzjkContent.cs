using Gzjk.login;
using HttpProcessor.Content;
using System;
using Utils;
using Utils.datetime;

namespace Gzjk.common
{
    internal class GzjkContent : HttpStringContent
    {
        public GzjkLogin User { get; private set; }

        public GzjkContent(string baseUrl, GzjkLogin user) : base(baseUrl)
        {
            User = user;

            BuildHeader();
        }

        private void BuildHeader()
        {
            AddHeader("Host", "api.cn2030.com");
            AddHeader("Connection", "keep-alive");
            AddHeader("Accept", "application/json, text/plain, */*");
            AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.143 Safari/537.36 MicroMessenger/7.0.9.501 NetType/WIFI MiniProgramEnv/Windows WindowsWechat");
            AddHeader("cookie", User.Cookie);
            AddHeader("zftsl", BuildZftsl());
            AddHeader("Referer", "https://servicewechat.com/wx9c8036c9d91af8b6/11/page-frame.html");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
        }

        public string BuildZftsl()
        {
            var dateParam = DateTimeUtil.GetTimeStamp().ToDouble() / 10000;
            var dateParamFloor = Math.Floor(dateParam);
            var md5Param = $"zfsw_{dateParamFloor}";
            var zfswMd5 = Encryptor.GetMD5_32(md5Param);
            return zfswMd5;
        }
    }
}
