using renren.common;
using renren.session;

namespace renren.search.patient
{
    internal class PatientDetailContent : RenrenBaseContent
    {
        public PatientDetailContent(string url) : base(url)
        {
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent(Constants.PatientId, MainSession.UserSession[Constants.PatientId]);
            AddContent(MainSession.PlatformSession, Constants.UserHospitalId);
            AddContent(MainSession.PlatformSession, Constants.HospitalId);
        }
    }
}
