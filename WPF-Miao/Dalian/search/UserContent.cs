using Dalian.common;
using Dalian.login;

namespace Dalian.search
{
    internal class UserContent : DalianContent
    {
        private static string baseUrl = "https://www.114yygh.com/mobile/patient/list?_time=";
        public UserContent(DalianLogin user) : base(baseUrl, user)
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
