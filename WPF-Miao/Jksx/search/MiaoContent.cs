using Jksx.common;
using Jksx.login;
using Jksx.session;
using Utils;

namespace Jksx.search
{
    internal class MiaoContent : JksxContent
    {
        private static string baseUrl = "https://ljzyyapi.yuanbaodaojia.com/v1/vaccine_day_list";

        public MiaoContent(JksxLogin user) : base(baseUrl, user)
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
