using JkzlSearcher.common;
using JkzlSearcher.session;
using Utils;
using Utils.datetime;

namespace JkzlSearcher.auth
{
    internal class DoctorAuthContent : JkzlContent
    {
        private const string Url = "https://appoint.yihu.com/appoint/do/registerAuth/queryAuthOnoff_v2";
        public DoctorAuthContent() : base(Url)
        {
            ContentType = "application/x-www-form-urlencoded";
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent(MainSession.PlatformSession, Constants.HospitalId);
            AddContent(MainSession.PlatformSession, Constants.DeptId);
            AddContent(MainSession.PlatformSession, Constants.DoctorSn);

            var defaultMember = SessionBuilder.GetDefaultMember();
            AddContent(Constants.MemberSn, defaultMember.GetString("Membersn"));
            AddContent("cardType", "1");
            AddContent("cardNo", MainSession.PlatformSession.GetString("cardNo"));
            AddContent("birthday", defaultMember.GetString("Birthday"));
        }
    }
}
