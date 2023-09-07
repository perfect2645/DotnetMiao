using Jingjiang.common;
using Jingjiang.login;
using Jingjiang.session;
using Utils;
using Utils.datetime;

namespace Jingjiang.search
{
    internal class MiaoContent : JingjiangContent
    {
        private static string baseUrl = ".yuanbaodaojia.com/v1/vaccine_day_list";

        public MiaoContent(JingjiangLogin user) : base(baseUrl, user)
        {
            BuildUrl();
            BuildContent();
        }

        protected override void BuildContent()
        {
            // timestamp=1693933038&token=d0e6689e39905500bd972bf9282bfdd2&vaccineAddressId=1&vaccineId=6&key=8C81915139AA5CCC160A0AC9168FF2C4
            var hosId = MainSession.PlatformSession.GetString(Constants.HospitalId);
            var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);

            var timestamp = DateTimeUtil.GetTimeStamp().Substring(0, 10);
            AddContent("timestamp", timestamp);
            AddContent("token", User.Token);
            AddContent("vaccineAddressId", hosId);
            AddContent("vaccineId", deptId);

            base.BuildContent();
        }
    }
}
