using Jksx.common;
using Jksx.login;
using Jksx.session;
using Utils;

namespace Jksx.appointment
{
    internal class YuyueContent : JksxContent
    {
        private static string baseUrl = "ldrmyy120.com/rest/v1/api/examine/vaccine_created/";
        public Order Order { get; private set; }
        public YuyueContent(Order order, JksxLogin user) : base(baseUrl, user)
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
