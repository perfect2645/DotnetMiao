using Guangde.common;
using Guangde.login;
using Guangde.session;
using Utils;

namespace Guangde.search
{
    internal class MiaoContent : GuangdeContent
    {
        private static string baseUrl = "https://ljzyyapi.yuanbaodaojia.com/v1/vaccine_day_list";

        public MiaoContent(GuangdeLogin user) : base(baseUrl, user)
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
