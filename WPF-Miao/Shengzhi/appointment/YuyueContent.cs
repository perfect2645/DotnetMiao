using Shengzhi.common;
using Shengzhi.login;
using Utils;

namespace Shengzhi.appointment
{
    internal class YuyueContent : ShengzhiContent
    {
        private static string url = "https://ldsq.ldrmyy120.com/rest/v1/api/examine/vaccine_created/";
        public Order Order { get; private set; }
        public YuyueContent(Order order, ShengzhiLogin user) : base(url, user)
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
