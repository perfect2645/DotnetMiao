using Base.session;
using Base.viewmodel.status;
using Redhouse.appointment;
using System.Collections.Generic;

namespace Redhouse.session
{
    internal class MainSession : MainSessionBase, ISessionContainer
    {
        public static string Authorization { get; set; }
        public static string Cookie { get; set; }
        public static Dictionary<string, object> PlatformSession { get; private set; }

        public static AppointEvent AppointEvent { get; }

        static MainSession()
        {
            PlatformSession = new Dictionary<string, object>();
            AppointEvent = new AppointEvent();
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
