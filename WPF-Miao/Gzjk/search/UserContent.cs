using Puzhou.common;
using Puzhou.login;

namespace Puzhou.search
{
    internal class UserContent : GzjkContent
    {
        private static string baseUrl = "https://zsyy.wzlwpz.nbnfsoft.com:7073/v2/microapi/patient/list";
        public UserContent(GzjkLogin user) : base(baseUrl, user)
        {
            BuildContent();
        }

        private void BuildContent()
        {

        }
    }
}
