using Huangshi.common;
using Huangshi.login;
using Huangshi.session;
using Utils;


namespace Huangshi.search
{
    internal class MiaoContent : MainContent
    {
        private static string url = "https://hscx.whcdc.org/vaccineServer/RegApiManage/queryNumberSource";
        public string Date { get; set; }
        public MiaoContent(HuangshiLogin user, string date) : base(url, user)
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
