using Haikou.common;
using Haikou.login;
using Haikou.session;
using System;
using Utils;

namespace Haikou.search
{
    internal class DateContent : HaikouContent
    {
        private static string baseUrl = "https://wx.hospite.com/hkfy/hpv9/listDate";
        public DateContent(HaikouLogin user) : base(baseUrl, user)
        {
            BuildContent();
        }

        private void BuildContent()
        {
            var appId = MainSession.PlatformSession.GetString(Constants.Appid);
            var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);
            var serviceCode = MainSession.PlatformSession.GetString(Constants.ServiceCode);
            var areaCode = MainSession.PlatformSession.GetString(Constants.HospitalId);

            AddContent("deptCode", deptId);
            AddContent("serviceCode", serviceCode);
            AddContent("areaCode", areaCode);
            AddContent("appid", appId);
            AddContent("version", Constants.Version);
            AddContent("token", User.Token);
        }
    }
}
