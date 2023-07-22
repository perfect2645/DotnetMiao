using SixWater.common;
using SixWater.login;
using SixWater.session;
using System;
using Utils;

namespace SixWater.search
{
    internal class MiaoContent : SixWaterContent
    {
        private static string baseUrl = "https://oss.zsqrmyy.com:8443/patient/register/doctorScheduledRecords?doctorId=&deptId=781&doctorScheduleId=50628&orgId=1478314809638342658&dateDivision=&scheduleDate=2023-07-22";

        public MiaoContent(SixWaterLogin user, string date, string scheduleId) : base(baseUrl, user)
        {
            BuildUrl();
        }

        private void BuildUrl()
        {
            var doctorId = MainSession.PlatformSession.GetString(Constants.DoctorId);
        }
    }
}
