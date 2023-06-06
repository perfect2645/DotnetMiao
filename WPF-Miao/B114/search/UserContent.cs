using B114.common;
using B114.login;

namespace B114.search
{
    internal class UserContent : B114Content
    {
        private static string baseUrl = "https://dm.cdpc.org.cn/dmxcx/out/province/person/list";
        public UserContent(B114Login user) : base(baseUrl, user)
        {
            BuildContent();
        }

        private void BuildContent()
        {

        }
    }
}
