using gaoxin.common;
using Utils;

namespace gaoxin.appointment
{
    internal class YuyueContent : GaoxinContent
    {
        private static string url = "http://wx1936.cnhis.cc/wx/user/appointment/reg.htm";
        public Order Order { get; }
        public YuyueContent(Order order) : base(url)
        {
            ContentType = "application/x-www-form-urlencoded";
            Order = order;
            BuildContent();
        }

        protected override void BuildHeader()
        {
            base.BuildHeader();
            AddHeader("showError", "false");
            AddHeader("Origin", "http://wx1936.cnhis.cc");
        }

        private void BuildContent()
        {
            AddContent("regtype", Order.Regtype);
            AddContent("type", "reg");
            AddContent("deptId", Order.DeptId);
            AddContent("deptName", UnicodeConverter.Encode(Order.DeptName, true));
            AddContent("doctorId", Order.DoctorId);
            AddContent("doctorName", UnicodeConverter.Encode(Order.DoctorName, true));
            AddContent("orgName", UnicodeConverter.Encode(Order.OrgName, true));
            AddContent("customerName", UnicodeConverter.Encode(Order.CustomerName, true));
            AddContent("customerId", Order.CustomerId);
            AddContent("appointAmount", Order.AppointAmount);
            AddContent("appointDate", Order.AppointDate);
            AddContent("timeRange", UnicodeConverter.Encode(Order.TimeRange, true));
            AddContent("remark", string.Empty);
            AddContent("newVersion", "true");
            AddContent("insuranceReg", 0);
            AddContent("medicalCardId", string.Empty);
            AddContent("childMedicalCardId", string.Empty);
            AddContent("scheduleId", Order.ScheduleId);
            AddContent("insurancePromotion", "false");
            AddContent("bco01", Order.Bco01);
        }
    }
}
