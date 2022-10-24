using renren.common;
using renren.session;

namespace renren.search.miao
{
    internal class ScheduleDetailContent : RenrenBaseContent
    {
        public string Date { get; private set; }
        public string Half { get; private set; }
        public string Period { get; private set; }

        public ScheduleDetailContent(string url) : base(url)
        {
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("date", Date);
            AddContent(MainSession.PlatformSesstion, Constants.ServiceId);
            AddContent("dayText", BuildDayText());
            AddContent("half", Half);
            AddContent("period", 2);
            AddContent("doctorId", MainSession.HospitalSession[Constants.TeamId]);
            AddContent(MainSession.PlatformSesstion, Constants.UserHospitalId);
            AddContent("bookingType", "0");
            AddContent("bookingToken", MainSession.UserSession[Constants.PublicKey]);
            AddContent(MainSession.PlatformSesstion, Constants.OpenId);
        }

        private string BuildDayText()
        {
            return "2022年10月13日 下午";
        }
    }
}
