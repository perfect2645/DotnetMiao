using SixWater.common;
using SixWater.login;

namespace SixWater.appointment
{
    internal class YuyueContent : SixWaterContent
    {
        private static string url = "https://ljzyyapi.yuanbaodaojia.com/v1/booking_vaccine_new";
        public Order Order { get; private set; }
        public YuyueContent(Order order) : base(url, order.User)
        {
            Order = order;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("vaccineDayId", Order.VaccineDayId);
            AddContent("vaccineDayNumId", Order.VaccineDayNumId);
            AddContent("vaccineId", Order.VaccineId);
            AddContent("familyId", Order.FamilyId);
            AddContent("token", Order.User.Token);
            AddContent("sign", Order.User.Sign);
        }
    }
}
