using Base.session;
using Base.viewmodel.status;
using Xihongmen.appointment;
using System.Collections.Generic;

namespace Xihongmen.session
{
    public class MainSession : MainSessionBase, ISessionContainer
    {
        public static string Phone { get; set; }
        public static Dictionary<string, object> PlatformSession { get; private set; }

        public static UserSession UserSession { get; private set; }

        public static ReSessionEvent ReSessionEvent { get; }
        public static ScheduleEvent ScheduleEvent { get; }

        static MainSession()
        {
            PlatformSession = new Dictionary<string, object>();
            UserSession = new UserSession();
            ReSessionEvent = new ReSessionEvent();
            ScheduleEvent = new ScheduleEvent();
        }

        #region MiaoStatus

        public static void SetStatus(MiaoProgress miaoProgress)
        {
            MiaoStatus.MiaoProgress = miaoProgress;
        }

        public static MiaoProgress GetStatus()
        {
            return MiaoStatus.MiaoProgress;
        }

        public static void SetStatus(MiaoProgress miaoProgress, object data)
        {
            MiaoStatus.OnStatusUpdate(miaoProgress, data);
        }

        #endregion MiaoStatus
    }
}
