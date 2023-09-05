using SixWater.common;
using SixWater.login;
using SixWater.session;
using Utils;

namespace SixWater.search
{
    internal class ScheduleContent : SixWaterContent
    {
        private static string baseUrl = "https://oss.zsqrmyy.com:8443/patient/register/scheduledDoctorInfos?";

        internal string Date { get; private set; }
        internal string ScheduleId { get; private set; }

        public ScheduleContent(SixWaterLogin user, string date) : base(baseUrl, user)
        {
            Date = date;
            BuildUrl();
        }

        private void BuildUrl()
        {
            var doctorId = MainSession.PlatformSession.GetString(Constants.DoctorId);
            var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);
            var orgId = MainSession.PlatformSession.GetString(Constants.HospitalId);
            RequestUrl = $"{baseUrl}doctorId={doctorId}&deptId={deptId}&orgId={orgId}&keyWrod=&scheduleDate={Date}&familyMemberId={User.UserId}";
        }
    }
}
