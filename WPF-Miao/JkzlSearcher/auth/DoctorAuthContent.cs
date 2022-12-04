using JkzlSearcher.common;
using JkzlSearcher.session;
using Utils.datetime;

namespace JkzlSearcher.auth
{
    internal class DoctorAuthContent : JkzlContent
    {
        private const string Url = "https://appoint.yihu.com/appoint/do/dept/getHosDeptList";
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
            AddContent(MainSession.PlatformSession, Constants.MemberSn);
            AddContent("cardType", "1");
            AddContent("cardNo", "orderId%3A0");
            AddContent("birthday", "1000031");
        }
    }
}
