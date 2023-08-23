using Haikou.common;
using Haikou.login;
using Haikou.session;
using Utils;

namespace Haikou.search
{
    internal class MiaoContent : HaikouContent
    {
        private static string baseUrl = "https://wx.hospite.com/hkfy/schedule/listSchedule";

        internal string Date { get; set; }

        public MiaoContent(HaikouLogin user, string date) : base(baseUrl, user)
        {
            Date = date;
            BuildContent();
        }

        private void BuildContent()
        {
            var appId = MainSession.PlatformSession.GetString(Constants.Appid);
            var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);
            var serviceCode = MainSession.PlatformSession.GetString(Constants.ServiceCode);
            var areaCode = MainSession.PlatformSession.GetString(Constants.HospitalId);

            AddContent("areaCode", areaCode);
            AddContent("deptCode", deptId);
            AddContent("date", Date);
            AddContent("serviceCode", serviceCode);
            AddContent("patientId", User.UserId);
            AddContent("appid", appId);
            AddContent("version", Constants.Version);
            AddContent("token", User.Token);
        }
    }
}
