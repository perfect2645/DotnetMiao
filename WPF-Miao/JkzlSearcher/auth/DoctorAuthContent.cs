using JkzlSearcher.common;
using JkzlSearcher.session;
using Utils;

namespace JkzlSearcher.auth
{
    internal class DoctorAuthContent : JkzlContent
    {
        private const string Url = "https://appoint.yihu.com/appoint/do/registerAuth/queryAuthOnoff_v2";

        private readonly string HospitalId;
        private readonly string DeptId;
        private readonly string DoctorSn;

        public DoctorAuthContent(string hospitalId, string deptId, string doctorSn) : base(Url)
        {
            HospitalId = hospitalId;
            DeptId = deptId;
            DoctorSn = doctorSn;
            ContentType = "application/x-www-form-urlencoded";
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent(Constants.HospitalId, HospitalId);
            AddContent(Constants.DeptId, DeptId);
            AddContent(Constants.DoctorSn, DoctorSn);

            var defaultMember = SessionBuilder.GetDefaultMember();
            AddContent(Constants.MemberSn, defaultMember.GetString("Membersn"));
            AddContent("cardType", "1");
            AddContent("cardNo", MainSession.PlatformSession.GetString("cardNo"));
            AddContent("birthday", defaultMember.GetString("Birthday"));
        }
    }
}
