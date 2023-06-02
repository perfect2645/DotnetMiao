using Dayim.common;
using Dayim.login;

namespace Dayim.search
{
    internal class UserContent : DayimContent
    {
        private static string baseUrl = "https://dm.cdpc.org.cn/dmxcx/out/province/person/list";
        public UserContent(DayimLogin user) : base(baseUrl, user)
        {
            BuildContent();
        }

        private void BuildContent()
        {

        }
    }
}
