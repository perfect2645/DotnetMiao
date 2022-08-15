using Baohe.baseClasses;
using Baohe.constants;
using Baohe.session;
using HttpProcessor.Content;
using System.Net.Http;
using System.Text;
using Utils.datetime;

namespace Baohe.search.doctor
{
    internal class DoctorContent : ContentBase
    {
        public DoctorContent(string url) : base(url)
        {
            ContentType = "application/x-www-form-urlencoded";
            BuildContent();
        }

        public override void BuildDefaultHeaders(HttpClient httpClient)
        {
            base.BuildDefaultHeaders(httpClient);
        }

        private void BuildContent()
        {
            AddContent("t", DateTimeUtil.GetTimeStamp());
            AddContent("hosId", BaoheSession.PlatformSesstion[Constant.HospitalId]);
            AddContent("deptId", BaoheSession.PlatformSesstion[Constant.DeptId]);
            AddContent("pageIndex", "1");
            AddContent("pageSize", "15");
            AddContent("multiHosDeptId", "");
        }

        public string BuildReferer()
        {
            var platformType = BaoheSession.PlatformSesstion[Constant.PlatformType];
            var hospitalId = BaoheSession.PlatformSesstion[Constant.HospitalId];

            var refererTemplate = $"https://appoint.yihu.com/appoint/doctor/ghDoctorList.html?platformType={platformType}&hospitalId={hospitalId}&exConsult=&consultHosId={hospitalId}";

            return refererTemplate;
        }

        public override string BuildCookie()
        {
            var sb = new StringBuilder();
            var cookie = base.BuildCookie();


            cookie = $"{cookie}{sb? .ToString() ?? string.Empty}";
            return cookie;
        }
    }
}
