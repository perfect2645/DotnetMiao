using Huangshi.common;
using Huangshi.login;
using Utils;

namespace Huangshi.appointment
{
    internal class YuyueContent : HuangshiContent
    {
        private static string url = "https://hscx.whcdc.org/vaccineServer/RegApiManage/lockNumber";
        public Order Order { get; private set; }
        public YuyueContent(Order order, HuangshiLogin user) : base(url, user)
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
