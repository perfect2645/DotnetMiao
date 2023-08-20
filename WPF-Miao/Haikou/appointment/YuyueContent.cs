using Haikou.common;
using Haikou.login;
using Haikou.session;
using System;
using Utils;

namespace Haikou.appointment
{
    internal class YuyueContent : HaikouContent
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

        private void BuildContent()
        {
            AddContent("vaccineDayId", Order.VaccineDayId);
            AddContent("vaccineDayNumId", Order.VaccineDayNumId);
            AddContent("vaccineId", Order.VaccineId);
            AddContent("familyId", Order.FamilyId);
            AddContent("token", Order.User.Token);

            if (!string.IsNullOrEmpty(Order.User.Sign))
            {
                AddContent("sign", Order.User.Sign);
            }
        }
    }
}
