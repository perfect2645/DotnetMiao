using Dalian.common;
using Dalian.login;


namespace Dalian.login
{
    internal class TokenContent : DalianContent
    {
        private static string baseUrl = "https://www.114yygh.com/mobile/token/accredit?_time=";
        private static string path = "/myself/queryPatients";

        public TokenContent(DalianLogin user) : base(baseUrl, path, user)
        {
            BuildContent();
        }

        private void BuildContent()
        {

        }
    }
}
