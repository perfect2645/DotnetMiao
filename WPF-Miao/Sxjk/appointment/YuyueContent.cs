using Sxjk.common;
using Sxjk.login;
using Sxjk.session;
using System;
using Utils;
using Utils.datetime;

namespace Sxjk.appointment
{
    internal class YuyueContent : SxjkContent
    {
        private static string baseUrl = "https://ymjz.sxcdc.cn/jmbweb/Encryption/Adult/SaveAdultReservationDetails?";
        public Order Order { get; private set; }
        public YuyueContent(Order order, SxjkLogin user) : base(baseUrl, user)
        {
            Order = order;
            BuildUrl();
            AddReferer();
        }

        protected override void BuildUrl()
        {
            var cityCode = MainSession.PlatformSession.GetString(Constants.CityCode);
            var stationCode = MainSession.PlatformSession.GetString(Constants.StationCode);

            UrlDic.AddOrUpdate("child_code", Order.Child_code);
            UrlDic.AddOrUpdate("station_code", Order.Station_code);
            UrlDic.AddOrUpdate("vaccine_code", Order.Vaccine_code);
            UrlDic.AddOrUpdate("reservation_date", Order.Reservation_date);
            UrlDic.AddOrUpdate("reservation_begin_time", Order.Reservation_begin_time);
            UrlDic.AddOrUpdate("reservation_end_time", Order.Reservation_end_time);
            UrlDic.AddOrUpdate("price_id", Order.Price_id);
            UrlDic.AddOrUpdate("address_info", Order.Address_info);
            UrlDic.AddOrUpdate("user_name", Order.LoginUser_name);
            UrlDic.AddOrUpdate("city_code", cityCode);
            base.BuildUrl();
        }

        private void AddReferer()
        {
            var today = DateTimeUtil.GetToday();

            AddHeader("Referer", $"https://ymjz.sxcdc.cn/SXJKWX/comfirmOrder?child_name=%E5%BC%A0%E6%A2%A6%E5%9C%86&station_name=%E9%AB%98%E5%AE%B6%E5%9E%A3%E7%A4%BE%E5%8C%BA%E5%8D%AB%E7%94%9F%E6%9C%8D%E5%8A%A1%E7%AB%99%E6%88%90%E4%BA%BA%E9%97%A8%E8%AF%8A&vaccine_code=B488&work_begin_time=08%3A00&work_end_time=12%3A00&currDate={today}");
        }
    }
}
