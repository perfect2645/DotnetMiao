using B114.common;
using B114.login;

namespace B114.search
{
    internal class UserContent : B114Content
    {
        private static string baseUrl = "https://www.114yygh.com/mobile/patient/list?_time=";
        public UserContent(B114Login user) : base(baseUrl, user)
        {
            BuildHeader();
            BuildContent();
        }

        private void BuildContent()
        {

        }

        protected override void BuildHeader()
        {
            base.BuildHeader();
            AddHeader("Referer", "https://www.114yygh.com/wechat/personal/patient");
        }
    }
}
