using Base.session;
using Base.viewmodel.status;
using Xihongmen.appointment;
using System.Collections.Generic;
using Xihongmen.login;

namespace Xihongmen.session
{
    public class MainSession : MainSessionBase, ISessionContainer
    {
        public static Dictionary<string, object> PlatformSession { get; private set; }
        public static ReSessionEvent ReSessionEvent { get; }
        public static ScheduleEvent ScheduleEvent { get; }
        internal static AppointSession AppointSession { get; private set; }
        internal static MiaoSession MiaoSession { get; private set; }
        internal static List<XhmLogin> Users { get; set; }

        static MainSession()
        {
            PlatformSession = new Dictionary<string, object>();
            ReSessionEvent = new ReSessionEvent();
            ScheduleEvent = new ScheduleEvent();
            Users = new List<XhmLogin>();
        }

        public static void InitSession()
        {
            AppointSession = new AppointSession();
            MiaoSession = new MiaoSession();
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
