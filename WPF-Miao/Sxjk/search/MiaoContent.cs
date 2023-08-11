using Sxjk.common;
using Sxjk.login;
using Sxjk.session;
using Utils;

namespace Sxjk.search
{
    internal class MiaoContent : SxjkContent
    {
        private static string baseUrl = "https://ymjz.sxcdc.cn/jmbshanxi/Encryption/Adult/GetAdultStationDayListNew?";

        public MiaoContent(SxjkLogin user, string date) : base(baseUrl, user)
        {
            BuildUrl();
            AddReferer();
        }

        protected override void BuildUrl()
        {
            var cityCode = MainSession.PlatformSession.GetString(Constants.CityCode);
            var stationCode = MainSession.PlatformSession.GetString(Constants.StationCode);

            UrlDic.AddOrUpdate("bact_id", User.BactId);
            UrlDic.AddOrUpdate("child_code", User.UserId);
            UrlDic.AddOrUpdate("priceId", User.PriceId);
            UrlDic.AddOrUpdate("station_code", stationCode);
            UrlDic.AddOrUpdate("user_name", User.LoginUserName);
            UrlDic.AddOrUpdate("city_code", cityCode);
            base.BuildUrl();
        }

        private void AddReferer()
        {
            AddHeader("Referer", "https://ymjz.sxcdc.cn/SXJKWX/selectDate");
        }
    }
}
