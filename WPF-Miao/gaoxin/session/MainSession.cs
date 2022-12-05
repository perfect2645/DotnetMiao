using Base.session;
using Base.viewmodel.status;
using gaoxin.appointment;
using System.Collections.Generic;

namespace gaoxin.session
{
    public class MainSession : MainSessionBase, ISessionContainer
    {
        public static int Interval { get; set; }
        public static string Cookie { get; set; }
        public static string Token { get; set; }
        public static string OrderToken { get; set; }
        public static string Code { get; set; }
        public static string DisparkId { get; set; }
        public static Dictionary<string, object> PlatformSession { get; private set; }

        public static UserSession UserSession { get; private set; }
        internal static AppointSession AppointSession { get; private set; }
        public static ReSessionEvent ReSessionEvent { get; }
        public static OrderEvent OrderEvent { get; }

        static MainSession()
        {
            PlatformSession = new Dictionary<string, object>();
            UserSession = new UserSession();
            ReSessionEvent = new ReSessionEvent();
            OrderEvent = new OrderEvent();
        }

        public static void InitSession()
        {
            AppointSession = new AppointSession();
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
