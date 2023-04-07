using Shaoguan.common;
using Shaoguan.login;
using Utils;

namespace Shaoguan.appointment
{
    internal class YuyueContent : MainContent
    {
        private static string url = "http://qjfy-adm-prod.hosp-proxy.cksoft.tech/api/register/submit?signature=";
        public Order Order { get; private set; }
        public YuyueContent(Order order, ShaoguanLogin user) : base(url, user)
        {
            Order = order;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("deptId", Order.Deptid);
            AddContent("deptName", Order.DeptName);
            AddContent("doctorId", Order.DoctorId);
            AddContent("doctorName", Order.DoctorName);
            AddContent("regDate", Order.RegDate);
            AddContent("timeID", Order.TimeID);
            AddContent("startTime", Order.StartTime);
            AddContent("endTime", Order.EndTime);
            AddContent("payment_type", "WX_GZH");
            AddContent("patient_id", Order.PatientId);
            AddContent("nowTime", string.Empty);
            AddContent("timestamp", Order.TimeStamp);
            AddContent("machine_code", "WeChat-SA");
            AddContent("token", Order.Token);
        }
    }
}
