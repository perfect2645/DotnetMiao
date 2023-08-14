using Sxjk.common;
using Sxjk.login;
using Sxjk.session;
using System;
using Utils;
using Utils.datetime;

namespace Sxjk.tools
{
    internal class BindUserContent : SxjkContent
    {
        private static string baseUrl = "https://ymjz.sxcdc.cn/jmbweb/Encryption/Adult/BindAdultChild?";
        public BindUserContent(SxjkLogin user) : base(baseUrl, user)
        {
            BuildUrl();
            AddReferer();
        }

        protected override void BuildUrl()
        {
            var cityCode = MainSession.PlatformSession.GetString(Constants.CityCode);

            UrlDic.AddOrUpdate("user_name", User.LoginUserName);
            UrlDic.AddOrUpdate("child_code", User.BirthDay);
            UrlDic.AddOrUpdate("city_code", cityCode);
            UrlDic.AddOrUpdate("version_name", User.VersionName);
            UrlDic.AddOrUpdate("os", "web");
            UrlDic.AddOrUpdate("userId", "null");
            UrlDic.AddOrUpdate("timeStamp", "null");

            BuildParameters();
            BuildSign();

            var timestamp = DateTimeUtil.GetTimeStamp();

            var urlEncodeParameters = UnicodeConverter.EncodeOriginal(ParameterEncode, true);
            RequestUrl = $"{BaseUrl}parameters={urlEncodeParameters}&sign={SignMD5}&timestamp={timestamp}";
        }

        private void AddReferer()
        {
            AddHeader("Referer", "https://ymjz.sxcdc.cn/SXJKWX/main.html?version=2.0.0");
        }
    }
}
