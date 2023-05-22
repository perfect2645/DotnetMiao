using Anze.common;
using Anze.login;

namespace Anze.appointment
{
    internal class YuyueContent : AnzeContent
    {
        private static string url = "https://ljzyyapi.yuanbaodaojia.com/v1/booking_vaccine";
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
        }
    }
}
