using Dalian.common;
using Dalian.login;


namespace Dalian.login
{
    internal class TokenContent : DalianContent
    {
        private static string baseUrl = "https://www.114yygh.com/mobile/token/accredit?_time=";
        public TokenContent(DalianLogin user) : base(baseUrl, user)
        {
            BuildContent();
        }

        private void BuildContent()
        {

        }
    }
}
