using B114.common;
using B114.login;


namespace B114.login
{
    internal class TokenContent : B114Content
    {
        private static string baseUrl = "https://www.114yygh.com/mobile/token/accredit?_time=";
        public TokenContent(B114Login user) : base(baseUrl, user)
        {
            BuildContent();
        }

        private void BuildContent()
        {

        }
    }
}
