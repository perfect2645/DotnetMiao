using renren.common;
using renren.session;

namespace renren.search.patient
{
    internal class PatientContent : RenrenBaseContent
    {
        public PatientContent(string url) : base(url)
        {
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("originUserHospitalId", MainSession.PlatformSession[Constants.UserHospitalId]);
            AddContent("isTest", false);
            AddContent("hasPatients", true);
            AddContent(MainSession.PlatformSession, Constants.AppId);
            AddContent(Constants.Mobile, MainSession.UserSession[Constants.Mobile]);
            AddContent("hasRedpack", false);
            AddContent(MainSession.PlatformSession, Constants.UserHospitalId);
            AddContent(Constants.PublicKey, MainSession.UserSession[Constants.PublicKey]);
            AddContent("hasTemporaryPatient", false);
            AddContent(Constants.UserId, MainSession.UserSession[Constants.UserId]);
            AddContent("presigned", true);
            AddContent("hasNewBpData", false);
            AddContent(MainSession.PlatformSession, Constants.OpenId);
            AddContent(MainSession.PlatformSession, Constants.HospitalId);
            AddContent("image", string.Empty);
            AddContent("nickname", string.Empty);
            AddContent("sex", "女");
            AddContent("tmCount", 0);
        }
    }
}
