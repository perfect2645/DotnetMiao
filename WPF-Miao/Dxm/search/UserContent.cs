using Dxm.common;
using Dxm.login;

namespace Dxm.search
{
    internal class UserContent : DxmContent
    {
        private static string baseUrl = "https://dm.cdpc.org.cn/dmxcx/person/list";
        public UserContent(DxmLogin user) : base(baseUrl, user)
        {
            BuildContent();
        }

        private void BuildContent()
        {

        }
    }
}
