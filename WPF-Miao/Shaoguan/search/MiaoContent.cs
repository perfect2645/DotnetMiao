using Shaoguan.common;
using Shaoguan.login;
using Shaoguan.session;
using Utils;


namespace Shaoguan.search
{
    internal class MiaoContent : MainContent
    {
        private static string url = "https://hscx.whcdc.org/vaccineServer/RegApiManage/queryNumberSource";
        public string Date { get; set; }
        public MiaoContent(ShaoguanLogin user, string date) : base(url, user)
        {
            Date = date;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent(MainSession.PlatformSession, Constants.DoctorId);
            AddContent("regDate", Date);
            AddContent(MainSession.PlatformSession, Constants.DeptId);
        }
    }
}
