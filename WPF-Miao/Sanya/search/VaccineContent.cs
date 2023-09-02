using Sanya.common;
using Sanya.login;
using Sanya.session;
using Utils;

namespace Sanya.search
{
    internal class VaccineContent : SanyaContent
    {
        private static string baseUrl = "https://public.health.zoenet.cn/business-service/mask-appointment/queryOne";

        public VaccineContent(SanyaLogin user) : base(baseUrl, user)
        {
            BuildContent();
            BuildHeaderSign();
        }

        private void BuildContent()
        {
            var orgCode = MainSession.PlatformSession.GetString(Constants.OrgCode);
            var subscribeType = MainSession.PlatformSession.GetString(Constants.SubscribeType);
            AddContent("orgCode", orgCode);
            //AddContent("parentCode", "2c90a2f271ba654f0171c015725d171a");
            AddContent("subscribeType", subscribeType);
        }

        protected override void BuildHeaderSign()
        {
            base.BuildHeaderSign();
        }
    }
}
