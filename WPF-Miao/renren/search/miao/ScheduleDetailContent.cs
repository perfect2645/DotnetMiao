using renren.common;
using renren.session;
using System;

namespace renren.search.miao
{
    internal class ScheduleDetailContent : RenrenBaseContent
    {
        public Schedule Schedule { get; }

        public ScheduleDetailContent(string url, Schedule schedule) : base(url)
        {
            Schedule = schedule;
            BuildContent();
        }

        private void BuildContent()
        {
            AddContent("date", Schedule.Date);
            AddContent(MainSession.PlatformSession, Constants.ServiceId);
            AddContent("dayText", BuildDayText());
            AddContent("half", Schedule.Half);
            AddContent("period", 2);
            AddContent("doctorId", MainSession.HospitalSession[Constants.TeamId]);
            AddContent(MainSession.PlatformSession, Constants.UserHospitalId);
            AddContent("bookingType", "0");
            AddContent("bookingToken", MainSession.PlatformSession[Constants.PublicKey]);
            AddContent(MainSession.PlatformSession, Constants.OpenId);
        }

        private string BuildDayText()
        {
            var dateDay = Convert.ToDateTime(Schedule.Date).ToString("yyyy年MM月dd日");
            string halfStr = "上午";
            switch(Schedule.Half)
            {
                case "morning":
                    halfStr = "上午"; break;
                case "afternoon":
                    halfStr = "下午"; break;
                case "night":
                    halfStr = "晚上"; break;
                    default:break;
            }
            return $"{dateDay} {halfStr}";
        }
    }
}
