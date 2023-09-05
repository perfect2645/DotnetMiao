using B114.common;
using B114.login;

namespace B114.login
{
    internal class LoginContent : B114Content
    {
        private static string baseUrl = "https://www.114yygh.com/mobile/login?_time=";
        public LoginContent(B114Login user) : base(baseUrl, user)
        {
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("mobile", "");
        }
    }
}
