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
            AddContent("from", GetSundayOfWeek());
            AddContent("to", GetSaturdayOfWeek());
            AddContent("bookingType", "0");
            AddContent("bookingToken", MainSession.UserSession[Constants.PublicKey]);
            AddContent(MainSession.PlatformSesstion, Constants.OpenId);
        }

        private string GetSundayOfWeek()
        {
            var dayOfWeek = (int)DateTime.Today.DayOfWeek;
            var sunday = DateTime.Today.AddDays(-dayOfWeek);
            return sunday.ToString("yyyy-MM-dd");
        }

        private string GetSaturdayOfWeek()
        {
            var dayOfWeek = (int)DateTime.Today.DayOfWeek;
            var sunday = DateTime.Today.AddDays(-dayOfWeek + 6);
            return sunday.ToString("yyyy-MM-dd");
        }
    }
}
