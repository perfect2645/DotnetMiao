using Sanya.common;
using Sanya.login;

namespace Sanya.search
{
    internal class UserContent : SanyaContent
    {
        private static string baseUrl = "https://public.health.zoenet.cn/medical/medical-card-manage/getUserInfoByUserIdOrToken";
        public UserContent(SanyaLogin user) : base(baseUrl, user)
        {
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("encryptFlag", 1);
        }
    }
}
