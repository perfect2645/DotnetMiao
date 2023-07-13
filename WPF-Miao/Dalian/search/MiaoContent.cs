using Dalian.common;
using Dalian.login;
using Dalian.session;
using Utils;

namespace Dalian.search
{
    internal class MiaoContent : DalianContent
    {
        private static string baseUrl = "https://www.114yygh.com/mobile/product/detail?_time=";
        private static string path = "/myself/queryPatients";

        public string Date { get; set; }

        public MiaoContent(DalianLogin user, string date) : base(baseUrl, path, user)
        {
            Date = date;
            BuildHeader();
            BuildContent();
        }

        protected override void BuildHeader()
        {
            base.BuildHeader();

            
        }

        private void BuildContent()
        {
            var hosId = MainSession.PlatformSession.GetString(Constants.HospitalId);
            var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);

            AddContent("hosCode", hosId);
            AddContent("secondDeptCode", deptId);
            AddContent("target", Date);
        }
    }
}
