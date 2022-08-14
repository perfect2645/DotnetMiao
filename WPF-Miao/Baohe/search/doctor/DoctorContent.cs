using Baohe.baseClasses;
using Baohe.constants;
using Baohe.session;
using HttpProcessor.Content;
using System.Net.Http;
using System.Text;

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
            AddContent("arrangeId", "160023903");
            AddContent("hospitalId", "1040231");
            AddContent("appliedDepartment", "");
            AddContent("channelId", "9000370");
            AddContent("ClinicCard", "");
        }

        public string BuildReferer()
        {
            var platformType = BaoheSession.PlatformSesstion[Constant.PlatformType];
            var hospitalId = BaoheSession.PlatformSesstion[Constant.HospitalId];
            var time = BaoheSession.PlatformSesstion[Constant.SessionTime];

            var refererTemplate = $"https://appoint.yihu.com/appoint/hospital/ghDeptList.html?platformType={platformType}&hospitalId={hospitalId}&time={time}";

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
