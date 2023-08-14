using Sxjk.common;
using Sxjk.session;
using System;
using Utils;
using Utils.datetime;
using Utils.stringBuilder;

namespace Sxjk.login
{
    internal class LoginContent : SxjkContent
    {
        private static string baseUrl = "https://ymjz.sxcdc.cn/jmbweb/Encryption/wx/toHtml5Servlet?";
        public LoginContent(SxjkLogin user) : base(baseUrl, user)
        {

            ConvertLoginData();
            //BuildUrl();
            //AddReferer();
            //BuildContent();
        }

        protected override void BuildUrl()
        {



            var cityCode = MainSession.PlatformSession.GetString(Constants.CityCode);

            UrlDic.AddOrUpdate("code", User.Code);
            UrlDic.AddOrUpdate("appid", User.Appid);
            UrlDic.AddOrUpdate("user_name", User.LoginUserName);
            UrlDic.AddOrUpdate("city_code", cityCode);
            UrlDic.AddOrUpdate("version_name", User.VersionName);
            UrlDic.AddOrUpdate("os", "web");
            UrlDic.AddOrUpdate("userId", "null");
            UrlDic.AddOrUpdate("timeStamp", "null");

            BuildParameters();
            BuildSign();

            var urlEncodeParameters = UnicodeConverter.EncodeOriginal(ParameterEncode, true);
            RequestUrl = $"{BaseUrl}parameters={urlEncodeParameters}&sign={SignMD5}&timestamp=null";
        }

        private void ConvertLoginData()
        {
            //code=001KK1100R0OsQ1QPK100qZwwB0KK11i&appid=wx83daa62ee4b38e4b&user_name=other#388418&city_code=140800000000&version_name=6.6.0&os=web&userId=null&timeStamp=null
            try
            {
                var loginDataUrlDecode = UnicodeConverter.Decode(User.LoginData);
                var loginDataDecode = Encrypt.DecryptAes(loginDataUrlDecode);
                var loginDataDic = UrlConverter.QueryToDic(loginDataDecode);
                var code = loginDataDic.GetString("code");
                var appid = loginDataDic.GetString("appid");
                var loginUserName = loginDataDic.GetString("user_name");

                User.Code = code;
                User.Appid = appid;
                User.LoginUserName = loginUserName;
                User.VersionName = loginDataDic.GetString("version_name");
            }
            catch(Exception ex)
            {
                Logging.GLog.Logger.Error(ex);
                MainSession.PrintLogEvent.Publish(User, $"ConvertLoginData failed.{ex.Message}");
            }
        }

        private void AddReferer()
        {
            AddHeader("Referer", "https://ymjz.sxcdc.cn/SXJKWX/mine/adultList");
        }

        private void BuildContent()
        {

        }
    }
}
