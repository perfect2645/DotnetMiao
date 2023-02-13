using Tianhe.common;
using Tianhe.login;
using Tianhe.session;
using Utils;

namespace Tianhe.appointment
{
    internal class YuyueContent : TianheContent
    {
        private static string baseUrl = "ldrmyy120.com/rest/v1/api/examine/vaccine_created/";
        public Order Order { get; private set; }
        public YuyueContent(Order order, TianheLogin user) : base(baseUrl, user)
        {
            Order = order;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("duty_time_id", Order.DutyTimeId);
            AddContent("vaccine_id", Order.VaccineId);
            AddContent("hospital", Order.HosipitalId);
            AddContent("inoculate_times", Order.InoculateTimes);
            AddContent("patient", Order.UserId);
            AddContent("see_date", Order.SeeDate);
            AddContent("address", Order.Address);
        }
    }
}
