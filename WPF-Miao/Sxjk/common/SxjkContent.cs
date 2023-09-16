using Sxjk.login;
using HttpProcessor.Content;
using System.Collections.Generic;
using Utils;
using System.Text;
using System;
using System.Reflection.Metadata;
using Sxjk.session;
using Utils.datetime;

namespace Sxjk.common
{
    internal class SxjkContent : HttpStringContent
    {
        public SxjkLogin User { get; private set; }
        protected string Parameters { get; private set; }
        protected string ParameterEncode { get; private set; }
        protected string SignMD5 { get; private set; }
        protected string BaseUrl { get; }

        public Dictionary<string, string> UrlDic { get; private set; }

        public SxjkContent(string baseUrl, SxjkLogin user) : base(baseUrl)
        {
            ContentType = "application/json;charset=UTF-8";
            BaseUrl = baseUrl;
            User = user;
            UrlDic = new Dictionary<string, string>();
            BuildHeader();
        }

        protected virtual void BuildUrl()
        {
            UrlDic.AddOrUpdate("version_name", User.VersionName);
            UrlDic.AddOrUpdate("os", "web");

            BuildParameters();
            BuildSign();

            var timestamp = DateTimeUtil.GetTimeStamp();

            var urlEncodeParameters = UnicodeConverter.EncodeOriginal(ParameterEncode, true);
            RequestUrl = $"{BaseUrl}parameters={urlEncodeParameters}&sign={SignMD5}&timestamp={timestamp}";

            MainSession.PrintLogEvent.Publish(this, $"Build Url:{RequestUrl}");
        }

        protected void BuildParameters()
        {
            var sb = new StringBuilder();
            foreach (var item in UrlDic)
            {
                sb.Append($"{item.Key}={item.Value}&");
            }
            Parameters = sb.ToString().TrimEnd('&');
            ParameterEncode = Encrypt.EncryptAes(Parameters);
        }

        protected void BuildSign()
        {
            var md5Source = $"{ParameterEncode}{Constants.Md5Key}";
            SignMD5 = Encryptor.GetMD5_32(md5Source);
        }


        protected virtual void BuildHeader()
        {
            AddHeader("Host", "ymjz.sxcdc.cn");
            AddHeader("Connection", "keep-alive");
            AddHeader("Accept", "application/json, text/plain, */*");
            AddHeader("User-Agent", "Mozilla/5.0 (iPhone; CPU iPhone OS 15_2_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Mobile/15E148 MicroMessenger/8.0.29(0x18001d38) NetType/WIFI Language/zh_CN");
            AddHeader("Cookie", User.Cookie);
            AddHeader("Referer", "https://ymjz.sxcdc.cn/SXJKWX/mine/adultList");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
            AddHeader("Accept-Language", "zh-CN,zh-Hans;q=0.9");
        }
    }
}
