using Dxm.common;
using Dxm.login;
using Dxm.session;
using Utils;

namespace Dxm.search
{
    internal class ScheduleContent : DxmContent
    {
        private static string baseUrl = "https://mix.med.gzhc365.com/api/register/dateschedulelist?_route=";

        public string Date { get; set; }

        public ScheduleContent(DxmLogin user, string date) : base(baseUrl, user)
        {
            Date= date;
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
            AddContent("doctorId", doctorId);
            AddContent("deptId", deptId);
            AddContent("t", Date);
            AddContent("sign", doctorSign);
            AddContent("login_access_token", User.LoginAccessToken);
        }
    }
}
