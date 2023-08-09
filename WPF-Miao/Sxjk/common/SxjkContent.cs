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
            ContentType = "application/json";
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
            AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36 NetType/WIFI MicroMessenger/7.0.20.1781(0x6700143B) WindowsWechat(0x63030073)");
            AddHeader("Cookie", User.Cookie);
            AddHeader("Sec-Fetch-Site", "same-origin");
            AddHeader("Sec-Fetch-Mode", "cors");
            AddHeader("Sec-Fetch-Dest", "empty");
            AddHeader("Accept-Encoding", "gzip, deflate, br");
            AddHeader("Accept-Language", "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7");
        }
    }
}
