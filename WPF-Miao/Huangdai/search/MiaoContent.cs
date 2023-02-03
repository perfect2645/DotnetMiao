using Jikong.common;
using Jikong.login;
using Jikong.session;
using Utils;


namespace Jikong.search
{
    internal class MiaoContent : JikongContent
    {
        private static string url = "https://hscx.whcdc.org/vaccineServer/RegApiManage/queryNumberSource";
        public string Date { get; set; }
        public MiaoContent(JikongLogin user, string date) : base(url, user)
        {
            Date = date;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent(MainSession.PlatformSession, Constants.DocId);
            AddContent("regDate", Date);
            AddContent(MainSession.PlatformSession, Constants.DeptId);
        }
    }
}
