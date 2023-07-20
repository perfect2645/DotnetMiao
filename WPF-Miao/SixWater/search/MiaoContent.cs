using SixWater.common;
using SixWater.login;
using SixWater.session;
using Utils;

namespace SixWater.search
{
    internal class MiaoContent : SixWaterContent
    {
        private static string baseUrl = "https://ljzyyapi.yuanbaodaojia.com/v1/vaccine_day_list";

        public MiaoContent(SixWaterLogin user) : base(baseUrl, user)
        {
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
