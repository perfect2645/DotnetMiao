using Sanya.common;
using Sanya.login;

namespace Sanya.search
{
    internal class UserContent : SanyaContent
    {
        private static string baseUrl = "https://zsyy.wzlwpz.nbnfsoft.com:7073/v2/microapi/patient/list";
        public UserContent(SanyaLogin user) : base(baseUrl, user)
        {
            BuildContent();
        }

        private void BuildContent()
        {

        }
    }
}
