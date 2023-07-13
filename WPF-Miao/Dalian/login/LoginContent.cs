using Dalian.common;
using Dalian.login;

namespace Dalian.login
{
    internal class LoginContent : DalianContent
    {
        private static string baseUrl = "https://www.114yygh.com/mobile/login?_time=";
        private static string path = "/myself/queryPatients";
        public LoginContent(DalianLogin user) : base(baseUrl, path, user)
        {
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("mobile", "");
        }
    }
}
