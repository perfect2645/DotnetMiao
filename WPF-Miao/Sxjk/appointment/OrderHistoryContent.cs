using Sxjk.common;
using Sxjk.login;
using Sxjk.session;
using System;
using Utils;

namespace Sxjk.appointment
{
    internal class OrderHistoryContent : SxjkContent
    {
        private static string baseUrl = "https://ymjz.sxcdc.cn/jmbweb/Encryption/Adult/GetAdultReservationList?";
        public OrderHistoryContent(SxjkLogin user) : base(baseUrl, user)
        {
            BuildUrl();
            AddReferer();
        }

        protected override void BuildUrl()
        {
            var cityCode = MainSession.PlatformSession.GetString(Constants.CityCode);

            UrlDic.AddOrUpdate("type", "0");
            UrlDic.AddOrUpdate("child_code", User.UserId);
            UrlDic.AddOrUpdate("user_name", User.LoginUserName);
            UrlDic.AddOrUpdate("page_index", "0");
            UrlDic.AddOrUpdate("city_code", cityCode);
            base.BuildUrl();
        }

        private void AddReferer()
        {
            AddHeader("Referer", "https://ymjz.sxcdc.cn/SXJKWX/mine/reserveList");
        }
    }
}
