using Sanya.common;
using Sanya.login;
using Sanya.session;
using Utils;

namespace Sanya.search
{
    internal class DepartmentContent : SanyaContent
    {
        private static string baseUrl = "https://public.health.zoenet.cn/medical/universal-interface/getCustomMenuListByZoneCode";

        public DepartmentContent(SanyaLogin user) : base(baseUrl, user)
        {
            BuildContent();
            BuildHeaderSign();
        }

        private void BuildContent()
        {
            var zoneCode = MainSession.PlatformSession.GetString(Constants.ZoneCode);
            var subscribeType = MainSession.PlatformSession.GetString(Constants.SubscribeType);
            AddContent("zoneCode", zoneCode);
            //AddContent("parentCode", "2c90a2f271ba654f0171c015725d171a");
            AddContent("parentCode", "20e248281adb42778a2daaed36af51bd");
        }

        protected override void BuildHeaderSign()
        {
            base.BuildHeaderSign();
        }
    }
}
