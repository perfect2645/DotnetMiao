using Sxjk.common;
using Sxjk.login;

namespace Sxjk.search
{
    internal class UserContent : SxjkContent
    {
        private static string baseUrl = "https://zsyy.wzlwpz.nbnfsoft.com:7073/v2/microapi/patient/list";
        public UserContent(SxjkLogin user) : base(baseUrl, user)
        {
            BuildContent();
        }

        private void BuildContent()
        {

        }
    }
}
