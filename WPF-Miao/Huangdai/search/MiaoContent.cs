using Huangdai.common;
using Huangdai.login;
using Huangdai.session;
using Utils;


namespace Huangdai.search
{
    internal class MiaoContent : HuangdaiContent
    {
        private static string url = "https://hscx.whcdc.org/vaccineServer/RegApiManage/queryNumberSource";
        public string Date { get; set; }
        public MiaoContent(HuangdaiLogin user, string date) : base(url, user)
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
