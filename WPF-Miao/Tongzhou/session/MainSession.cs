using Base.viewmodel.status;
using Tongzhou.appointment;
using System.Collections.Generic;
using Tongzhou.login;
using Base.session;

namespace Tongzhou.session
{
    public class MainSession : MainSessionBase, ISessionContainer
    {
        public static string Cookie { get; set; }
        public static Dictionary<string, object> PlatformSession { get; private set; }
        internal static List<TongzhouLogin> Users { get; set; }
        public static ReSessionEvent ReSessionEvent { get; }
        public static OrderEvent OrderEvent { get; }
        public static AppointSession AppointSession { get; private set; }
        internal static Dictionary<string, List<Order>> Orders { get; set; }

        static MainSession()
        {
            PlatformSession = new Dictionary<string, object>();
            ReSessionEvent = new ReSessionEvent();
            OrderEvent = new OrderEvent();
            Users = new List<TongzhouLogin>();
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


        #region Timestamp

        private static int _localTimeOffset = 0;
        public static int LocalTimeOffset
        {
            get
            {
                return ++_localTimeOffset;
            }
        }

        #endregion Timestamp
    }
}
