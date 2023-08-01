using Sanya.common;
using Sanya.login;
using Utils;

namespace Sanya.search
{
    internal class UserContent : SanyaContent
    {
        private static string baseUrl = "https://public.health.zoenet.cn/medical/medical-card-manage/getUserInfoByUserIdOrToken";
        public UserContent(SanyaLogin user) : base(baseUrl, user)
        {
            BuildContent();
            BuildHeaderSign();
        }

        private void BuildContent()
        {
            AddContent("encryptFlag", 1);
        }

        protected override void BuildHeaderSign()
        {
            HeaderSignDic.AddOrUpdate("encryptFlag", 1);
            base.BuildHeaderSign();
        }
    }
}
