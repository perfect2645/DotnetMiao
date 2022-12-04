using JkzlSearcher.common;
using JkzlSearcher.session;
using System.Net.Http;
using System.Text;

namespace JkzlSearcher.search.user
{
    internal class MemberListContent : JkzlContent
    {
        public MemberListContent(string url) : base(url)
        {
            ContentType = "application/x-www-form-urlencoded";
            BuildContent();
        }

        public override void BuildDefaultHeaders(HttpClient httpClient)
        {
            base.BuildDefaultHeaders(httpClient);
        }

        public void BuildContent()
        {
            AddContent(MainSession.PlatformSession, Constants.AccountSn);
            AddContent("hosKey", string.Empty);
        }

        public string BuildReferer()
        {
            var platformType = MainSession.PlatformSession[Constants.PlatformType];
            var hospitalId = MainSession.PlatformSession[Constants.HospitalId];

            var refererTemplate = $"https://appoint.yihu.com/appoint/doctor/ghDoctorList.html?platformType={platformType}&hospitalId={hospitalId}&exConsult=&consultHosId={hospitalId}";

            return refererTemplate;
        }
    }
}
