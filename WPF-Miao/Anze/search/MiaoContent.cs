using Anze.common;
using Anze.login;
using Anze.session;
using Utils;

namespace Anze.search
{
    internal class MiaoContent : AnzeContent
    {
        private static string baseUrl = "https://ljzyyapi.yuanbaodaojia.com/v1/vaccine_day_list";

        public MiaoContent(AnzeLogin user) : base(baseUrl, user)
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
