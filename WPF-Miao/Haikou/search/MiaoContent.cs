using Haikou.common;
using Haikou.login;
using Haikou.session;
using Utils;

namespace Haikou.search
{
    internal class MiaoContent : HaikouContent
    {
        private static string baseUrl = ".yuanbaodaojia.com/v1/vaccine_day_list";

        public MiaoContent(HaikouLogin user) : base(baseUrl, user)
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
