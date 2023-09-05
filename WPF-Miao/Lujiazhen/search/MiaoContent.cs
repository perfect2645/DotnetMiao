using Lujiazhen.common;
using Lujiazhen.login;
using Lujiazhen.session;
using Utils;

namespace Lujiazhen.search
{
    internal class MiaoContent : LujiazhenContent
    {
        private static string baseUrl = ".yuanbaodaojia.com/v1/vaccine_day_list";

        public MiaoContent(LujiazhenLogin user) : base(baseUrl, user)
        {
            BuildUrl();
            BuildContent();
        }

        private void BuildContent()
        {
            var hosId = MainSession.PlatformSession.GetString(Constants.HospitalId);
            var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);
            AddContent("vaccineId", deptId);
            AddContent("vaccineAddressId", hosId);
            AddContent("token", User.Token);
        }
    }
}
