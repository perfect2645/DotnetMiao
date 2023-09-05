using Dalian.common;
using Dalian.login;
using Dalian.session;
using Utils;

namespace Dalian.search
{
    internal class MiaoContent : DalianContent
    {
        private static string baseUrl = "https://hlwyy.dlfeyljt.com/patient/v1/appoint/regPoints";
        private static string path = "/appoint/regPoints";

        public string Date { get; set; }

        public MiaoContent(DalianLogin user, string date) : base(baseUrl, path, user)
        {
            Date = date;
            BuildRequestData();
            BuildHeader();
        }

        protected override void BuildRequestData()
        {
            var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);
            var levelId = MainSession.PlatformSession.GetString(Constants.RegLevelId);

            Parameters.AddOrUpdate("deptId", deptId);
            Parameters.AddOrUpdate("targetId", deptId);
            Parameters.AddOrUpdate("targetType", "0");
            Parameters.AddOrUpdate("startDate", Date);
            Parameters.AddOrUpdate("endDate", Date);
            Parameters.AddOrUpdate("pageNo", "1");
            Parameters.AddOrUpdate("pageSize", "999");
            Parameters.AddOrUpdate("regLevelId", levelId);
          
            base.BuildRequestData();
        }
    }
}
