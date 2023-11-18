using Rika.common;
using Rika.login;
using Rika.session;
using System;
using Utils;
using Utils.datetime;

namespace Rika.appointment
{
    internal class YuyueContent : RikaContent
    {
        private static string url = ".yuanbaodaojia.com/v1/booking_vaccine_new";
        public Order Order { get; private set; }
        public YuyueContent(Order order) : base(url, order.User)
        {
            Order = order;
        }

        private void BuildContent()
        {
            AddContent("familyId", Order.FamilyId);
            var timestamp = DateTimeUtil.GetTimeStamp().Substring(0, 10);
            Content.AddOrUpdate("timestamp", timestamp);
            AddContent("token", Order.User.Cookie);
            AddContent("vaccineDayId", Order.VaccineDayId);
            AddContent("vaccineDayNumId", Order.VaccineDayNumId);
            AddContent("vaccineId", Order.VaccineId);
        }
    }
}
