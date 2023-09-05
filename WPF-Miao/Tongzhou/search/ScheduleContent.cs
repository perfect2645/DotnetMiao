using Tongzhou.common;
using Tongzhou.login;
using Tongzhou.session;
using Utils;

namespace Tongzhou.search
{
    internal class ScheduleContent : TongzhouPostContent
    {
        public ScheduleContent(TongzhouLogin user) : base("queryDoctorSchedulingByDateV2", "appoint.scheduleService", user)
        {
            ContentPrefix = "[";
            ContentSuffix = "]";
            BuildContent();
            BuildSignHeaders();
        }

        private void BuildContent()
        {
            var organId = MainSession.PlatformSession.GetString(Constants.HospitalId).ToInt();
            var doctorId = MainSession.PlatformSession.GetString(Constants.DoctorId).ToInt();

            AddContent("organId", organId);
            AddContent("depart", null);
            AddContent("appointDepartCodes", new string[]{ });
            AddContent("sourceLevel", null);
            AddContent("hisSourceLevelText", null);
            AddContent("busType", null);
            AddContent("sourceType", 1);
            AddContent("doctorId", doctorId);
            AddContent("date", null);
            AddContent("scheduleDesc", null);
        }
    }
}
