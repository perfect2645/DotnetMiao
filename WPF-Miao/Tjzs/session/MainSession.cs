using Base.session;
using Base.viewmodel.status;
using Tjzs.appointment;
using Tjzs.login;
using System.Collections.Generic;

namespace Tjzs.session
{
    public class MainSession : MainSessionBase, ISessionContainer
    {
        public static string Cookie { get; set; }
        public static Dictionary<string, object> PlatformSession { get; private set; }
        internal static List<TjzsLogin> Users { get; set; }
        public static OrderEvent OrderEvent { get; }
        public static AppointSession AppointSession { get; private set; }
        internal static Dictionary<string, Order> Orders { get; set; }

        static MainSession()
        {
            PlatformSession = new Dictionary<string, object>();
            OrderEvent = new OrderEvent();
            Users = new List<TjzsLogin>();
            Orders = new Dictionary<string, Order>();
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
