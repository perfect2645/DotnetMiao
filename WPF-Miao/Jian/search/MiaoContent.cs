using Jian.appointment;
using Jian.common;
using Jian.login;
using Jian.session;
using Utils;

namespace Jian.search
{
    internal class MiaoContent : JianContent
    {
        private static string baseUrl = "https://mix.med.gzhc365.com/api/register/schedulelist?_route=";

        public Order ScheduleOrder { get; set; }

        public MiaoContent(JianLogin user, Order scheduleOrder) : base(baseUrl, user)
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

        }
    }
}
