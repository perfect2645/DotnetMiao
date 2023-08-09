using Sxjk.common;
using Sxjk.login;
using Sxjk.session;
using System;
using Utils;

namespace Sxjk.search
{
    internal class UserContent : SxjkContent
    {
        private static string baseUrl = "https://ymjz.sxcdc.cn/jmbshanxi/Encryption/Adult/GetAdultList?";
        public UserContent(SxjkLogin user) : base(baseUrl, user)
        {
            BuildUrl();
            AddReferer();
            BuildContent();
        }

        protected override void BuildUrl()
        {
            var cityCode = MainSession.PlatformSession.GetString(Constants.CityCode);

            UrlDic.AddOrUpdate("bact_id", string.Empty);
            UrlDic.AddOrUpdate("user_name", User.LoginUserName);
            UrlDic.AddOrUpdate("type", "0");
            UrlDic.AddOrUpdate("city_code", cityCode);
            base.BuildUrl();
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
