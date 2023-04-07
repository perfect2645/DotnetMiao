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
            AddContent(MainSession.PlatformSession, Constants.UserHospitalId);
            AddContent(MainSession.PlatformSession, Constants.ServiceId);
            AddContent(MainSession.PlatformSession, Constants.ScheduleFrom);
            AddContent(MainSession.PlatformSession, Constants.ScheduleTo);
            AddContent("bookingType", "0");
            AddContent("bookingToken", MainSession.PlatformSession[Constants.PublicKey]);
            AddContent(MainSession.PlatformSession, Constants.OpenId);
        }
    }
}
