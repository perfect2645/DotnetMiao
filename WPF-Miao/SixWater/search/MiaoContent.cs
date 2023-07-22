using SixWater.common;
using SixWater.login;
using SixWater.session;
using System;
using Utils;

namespace SixWater.search
{
    internal class MiaoContent : SixWaterContent
    {
        private static string baseUrl = "https://oss.zsqrmyy.com:8443/patient/register/doctorScheduledRecords?";

        internal string Date { get; private set; }
        internal string ScheduleId { get; private set; }

        public MiaoContent(SixWaterLogin user, string date, string scheduleId) : base(baseUrl, user)
        {
            Date = date;
            ScheduleId = scheduleId;
            BuildUrl();
        }

        private void BuildUrl()
        {
            var doctorId = MainSession.PlatformSession.GetString(Constants.DoctorId);
            var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);
            var orgId = MainSession.PlatformSession.GetString(Constants.HospitalId);
            RequestUrl = $"{baseUrl}doctorId={doctorId}&deptId={deptId}&doctorScheduleId={ScheduleId}&orgId={orgId}&dateDivision=&scheduleDate={Date}";
        }
    }
}
