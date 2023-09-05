using Dalian.common;
using Dalian.login;
using Dalian.session;
using System;
using Utils;

namespace Dalian.search
{
    internal class ScheduleContent : DalianContent
    {
        private static string baseUrl = "https://hlwyy.dlfeyljt.com/patient/v1/appoint/getRegDates";
        private static string path = "/appoint/getRegDates";


        public ScheduleContent(DalianLogin user) : base(baseUrl, path, user)
        {
            BuildRequestData();
            BuildHeader();
        }

        protected override void BuildRequestData()
        {
            var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);

            Parameters.AddOrUpdate("deptId", deptId);
            Parameters.AddOrUpdate("targetType", 0);

            var today = DateTime.Today.ToString("yyyyMMdd");
            var endDate = DateTime.Today.AddDays(14).ToString("yyyyMMdd");

            Parameters.AddOrUpdate("startDate", today);
            Parameters.AddOrUpdate("endDate", endDate);
          
            base.BuildRequestData();
        }
    }
}
