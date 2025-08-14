using Rika.common;
using Rika.login;
using Rika.session;
using Utils;
using Utils.datetime;

namespace Rika.search
{
    internal class MiaoContent : RikaContent
    {
        private static string baseUrl = ".yuanbaodaojia.com/v1/vaccine_day_list";

        public MiaoContent(RikaLogin user) : base(baseUrl, user)
        {
            BuildContent();
        }

        private void BuildContent()
        {
            var hosId = MainSession.PlatformSession.GetString(Constants.HospitalId);
            var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);

            var timestamp = DateTimeUtil.GetTimeStamp().Substring(0, 10);
            AddContent("timestamp", timestamp);
            AddContent("token", User.Cookie);
            AddContent("vaccineAddressId", hosId);
            AddContent("vaccineId", deptId);
        }
    }
}
