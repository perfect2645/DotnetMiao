using Huangdai.common;
using Huangdai.login;
using Utils;

namespace Huangdai.appointment
{
    internal class YuyueContent : HuangdaiContent
    {
        private static string url = "https://hscx.whcdc.org/vaccineServer/RegApiManage/lockNumber";
        public Order Order { get; private set; }
        public YuyueContent(Order order, HuangdaiLogin user) : base(url, user)
        {
            Order = order;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("itemName", Order.ItemName);
            AddContent("visitDate", Order.VisitDate);
            AddContent("visitTime", Order.VisitTime);
            AddContent("amOrPm", Order.AmOrPm);
            AddContent("patientId", Order.UserId);
            AddContent("scheduleCode", Order.ScheduleCode);
            AddContent("scheduleInfoCode", Order.ScheduleInfoCode);
            AddContent("type", Order.Type);
        }
    }
}
