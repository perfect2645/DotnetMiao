using Puzhou.common;
using Puzhou.login;

namespace Puzhou.search
{
    internal class UserContent : PuzhouContent
    {
        private static string baseUrl = "https://zsyy.wzlwpz.nbnfsoft.com:7073/v2/microapi/patient/list";
        public UserContent(PuzhouLogin user) : base(baseUrl, user)
        {
            BuildContent();
        }

        private void BuildContent()
        {

        }
    }
}
