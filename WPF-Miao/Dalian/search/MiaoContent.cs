using Dalian.common;
using Dalian.login;
using Dalian.session;
using Utils;

namespace Dalian.search
{
    internal class MiaoContent : DalianContent
    {
        private static string baseUrl = "https://www.114yygh.com/mobile/product/detail?_time=";

        public string Date { get; set; }

        public MiaoContent(DalianLogin user, string date) : base(baseUrl, user)
        {
            Date = date;
            BuildHeader();
            BuildContent();
        }

        protected override void BuildHeader()
        {
            base.BuildHeader();

            var hosCode = MainSession.PlatformSession.GetString(Constants.HospitalId);
            var firstDept = MainSession.PlatformSession.GetString(Constants.FirstDeptCode);
            var secondDept = MainSession.PlatformSession.GetString(Constants.DeptId);
            AddHeader("Referer", $"https://www.114yygh.com/wechat/hospital/source/date?hosCode={hosCode}&firstDeptCode={firstDept}&secondDeptCode={secondDept}&code={secondDept}");
        }

        private void BuildContent()
        {
            var hosId = MainSession.PlatformSession.GetString(Constants.HospitalId);
            var firstDeptId = MainSession.PlatformSession.GetString(Constants.FirstDeptCode);
            var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);

            AddContent("hosCode", hosId);
            AddContent("firstDeptCode", firstDeptId);
            AddContent("secondDeptCode", deptId);
            AddContent("target", Date);
        }
    }
}
