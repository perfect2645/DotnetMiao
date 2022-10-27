using renren.common;
using renren.session;
using System;

namespace renren.search.miao
{
    internal class ScheduleContent : RenrenBaseContent
    {
        public ScheduleContent(string url) : base(url)
        {
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent(MainSession.PlatformSesstion, Constants.UserHospitalId);
            AddContent(MainSession.PlatformSesstion, Constants.ServiceId);
            AddContent(MainSession.PlatformSesstion, Constants.ScheduleFrom);
            AddContent(MainSession.PlatformSesstion, Constants.ScheduleTo);
            AddContent("bookingType", "0");
            AddContent("bookingToken", MainSession.PlatformSesstion[Constants.PublicKey]);
            AddContent(MainSession.PlatformSesstion, Constants.OpenId);
        }
    }
}
