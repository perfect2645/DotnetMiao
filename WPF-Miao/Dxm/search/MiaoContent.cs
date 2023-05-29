using Dxm.appointment;
using Dxm.common;
using Dxm.login;
using Dxm.session;
using Utils;

namespace Dxm.search
{
    internal class MiaoContent : DxmContent
    {
        private static string baseUrl = "https://mix.med.gzhc365.com/api/register/schedulelist?_route=";

        public Order ScheduleOrder { get; set; }

        public MiaoContent(DxmLogin user, Order scheduleOrder) : base(baseUrl, user)
        {
            ScheduleOrder = scheduleOrder;
            var hosId = MainSession.PlatformSession.GetString(Constants.HospitalId);
            RequestUrl = $"{baseUrl}h{hosId}&";
            BuildContent();
        }

        private void BuildContent()
        {
            var hosId = MainSession.PlatformSession.GetString(Constants.HospitalId);
            var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);
            var doctorId = MainSession.PlatformSession.GetString(Constants.DoctorId);
            var doctorSign = MainSession.PlatformSession.GetString(Constants.DoctorSign);

            AddContent("hisId", hosId);
            AddContent("platformId", hosId);
            AddContent("platformSource", 3);
            AddContent("subSource", 1);
            AddContent("scheduleDate", ScheduleOrder.ScheduleDate);
            AddContent("doctorId", doctorId);
            AddContent("deptId", deptId);
            AddContent("t", ScheduleOrder.SearchMonth);
            AddContent("sign", doctorSign);
            AddContent("login_access_token", User.LoginAccessToken);
        }
    }
}
