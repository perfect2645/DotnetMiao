using Jiangbei.common;
using Jiangbei.login;

namespace Jiangbei.search
{
    internal class UserContent : JiangbeiContent
    {
        private static string baseUrl = "https://zsyy.wzlwpz.nbnfsoft.com:7073/v2/microapi/patient/list";
        public UserContent(JiangbeiLogin user) : base(baseUrl, user)
        {
            BuildContent();
        }

        private void BuildContent()
        {

        }
    }
}
