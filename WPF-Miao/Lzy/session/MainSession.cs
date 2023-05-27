using Base.session;
using Base.viewmodel.status;
using Lzy.appointment;
using System.Collections.Generic;
using Lzy.login;
using Base.model;

namespace Lzy.session
{
    public class MainSession : MainSessionBase, ISessionContainer
    {
        public static string Cookie { get; set; }
        public static Dictionary<string, object> PlatformSession { get; private set; }
        internal static List<LzyLogin> Users { get; set; }
        public static ReSessionEvent ReSessionEvent { get; }
        public static OrderEvent OrderEvent { get; }
        public static AppointSession AppointSession { get; private set; }
        internal static Dictionary<string, List<Order>> Orders { get; set; }
        public static List<DspVal> DateList { get; set; }
        public static List<DspVal> TimeList { get; set; }

        static MainSession()
        {
            PlatformSession = new Dictionary<string, object>();
            ReSessionEvent = new ReSessionEvent();
            OrderEvent = new OrderEvent();
            Users = new List<LzyLogin>();
            Orders = new Dictionary<string, List<Order>>();
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
