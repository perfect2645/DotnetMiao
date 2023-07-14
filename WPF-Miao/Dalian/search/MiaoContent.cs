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
            var appId = MainSession.PlatformSession.GetString(Constants.AppId);
            var hisDeptId = MainSession.PlatformSession.GetString(Constants.HisDeptId);
            var hosId = MainSession.PlatformSession.GetString(Constants.HospitalId);

            Parameters.AddOrUpdate("hisDeptId", hisDeptId);
            Parameters.AddOrUpdate("scheduleDate", Date);
            Parameters.AddOrUpdate("targetType", 0);
            Parameters.AddOrUpdate("patientId", string.Empty);
            Parameters.AddOrUpdate("pageSize", 9999);
            Parameters.AddOrUpdate("pageNo", 1);
            //Parameters.AddOrUpdate("appId", appId);
            //Parameters.AddOrUpdate("openId", User.OpenId);
            //Parameters.AddOrUpdate("hospId", hosId);
            //Parameters.AddOrUpdate("hospitalId", hosId);
          
            base.BuildRequestData();
        }
    }
}
