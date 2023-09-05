using Jkgx.common;
using Jkgx.login;

namespace Jkgx.search
{
    internal class UserContent : JkgxContent
    {
        private static string baseUrl = "https://dm.cdpc.org.cn/dmxcx/out/province/person/list";
        public UserContent(JkgxLogin user) : base(baseUrl, user)
        {
            BuildContent();
        }

        private void BuildContent()
        {

        }
    }
}
