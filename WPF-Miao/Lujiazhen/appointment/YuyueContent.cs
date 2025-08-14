using Lujiazhen.common;
using Lujiazhen.login;
using Lujiazhen.session;
using System;
using Utils;
using Utils.datetime;

namespace Lujiazhen.appointment
{
    internal class YuyueContent : LujiazhenContent
    {
        private static string url = ".yuanbaodaojia.com/v1/booking_vaccine_new";
        public Order Order { get; private set; }
        public YuyueContent(Order order) : base(url, order.User)
        {
            Order = order;

            if (string.IsNullOrEmpty(User.Sign))
            {
                BaseUrl = $".yuanbaodaojia.com/v1/booking_vaccine";
            }
            BuildUrl();
            BuildContent();
        }

        protected override void BuildContent()
        {
            AddContent("familyId", Order.FamilyId);
            if (!string.IsNullOrEmpty(Order.User.Sign))
            {
                AddContent("sign", Order.User.Sign);
            }
            var timestamp = DateTimeUtil.GetTimeStamp().Substring(0, 10);
            Content.AddOrUpdate("timestamp", timestamp);
            AddContent("token", Order.User.Token);
            AddContent("vaccineDayId", Order.VaccineDayId);
            AddContent("vaccineDayNumId", Order.VaccineDayNumId);
            AddContent("vaccineId", Order.VaccineId);

            base.BuildContent();
        }
    }
}
