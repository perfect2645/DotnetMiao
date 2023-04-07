using Jikong.common;
using Jikong.login;
using Utils;

namespace Jikong.appointment
{
    internal class YuyueContent : JikongContent
    {
        private static string url = "https://hscx.whcdc.org/vaccineServer/RegApiManage/lockNumber";
        public Order Order { get; private set; }
        public YuyueContent(Order order, JikongLogin user) : base(url, user)
        {
            Order = order;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("itemName", Order.ItemName);
            AddContent("itemCode", Order.ItemCode);
            AddContent("minuteHourRegTotal", Order.MinuteHourRegTotal);
            AddContent("visitDate", Order.VisitDate);
            AddContent("visitTime", Order.VisitTime);
            AddContent("amOrPm", Order.AmOrPm);
            AddContent("patientId", Order.UserId);
            AddContent("scheduleCode", Order.ScheduleCode);
            AddContent("scheduleInfoCode", Order.ScheduleInfoCode);
            AddContent("checkType", Order.CheckType);
            AddContent("checkCode", Order.CheckCode);
            AddContent("type", Order.Type);
        }
    }
}
