using Sanya.common;
using Sanya.login;
using Sanya.session;
using Utils;

namespace Sanya.search
{
    internal class MiaoContent : SanyaContent
    {
        private static string baseUrl = "https://public.health.zoenet.cn/business-service/mask-appointment/queryTimeDetails";
        public MiaoContent(SanyaLogin user) : base(baseUrl, user)
        {
            BuildContent();
            BuildHeaderSign();
        }

        private void BuildContent()
        {
            var orgCode = MainSession.PlatformSession.GetString(Constants.OrgCode);
            var subscribeType = MainSession.PlatformSession.GetString(Constants.SubscribeType);
            var serviceId = MainSession.PlatformSession.GetString(Constants.DeptId);
            AddContent("orgCode", orgCode);
            AddContent("subscribeType", subscribeType);
            AddContent("serviceId", serviceId);
        }

        protected override void BuildHeaderSign()
        {
            base.BuildHeaderSign();
        }
    }
}
