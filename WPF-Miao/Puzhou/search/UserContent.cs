using Puzhou.common;
using Puzhou.login;

namespace Puzhou.search
{
    internal class UserContent : PuzhouContent
    {
        private static string baseUrl = "https://dm.cdpc.org.cn/dmxcx/out/province/person/list";
        public UserContent(PuzhouLogin user) : base(baseUrl, user)
        {
            BuildContent();
        }

        private void BuildContent()
        {

        }
    }
}
