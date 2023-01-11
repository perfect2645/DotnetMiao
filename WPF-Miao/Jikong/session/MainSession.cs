using Base.session;
using Base.viewmodel.status;
using Jikong.appointment;
using System.Collections.Generic;
using Jikong.login;
using Base.model;
using System;
using System.Linq;

namespace Jikong.session
{
    public class MainSession : MainSessionBase, ISessionContainer
    {
        public static string Cookie { get; set; }
        public static Dictionary<string, object> PlatformSession { get; private set; }
        internal static List<JikongLogin> Users { get; set; }
        public static ReSessionEvent ReSessionEvent { get; }
        public static OrderEvent OrderEvent { get; }
        public static AppointSession AppointSession { get; private set; }
        internal static MiaoSession MiaoSession { get; private set; }
        
        internal static Dictionary<string, List<Order>> Orders { get; set; }

        internal static List<string> DateList 
        {
            get { return GetDateList(); }
        }

        static MainSession()
        {
            PlatformSession = new Dictionary<string, object>();
            ReSessionEvent = new ReSessionEvent();
            OrderEvent = new OrderEvent();
            Users = new List<JikongLogin>();
            Orders = new Dictionary<string, List<Order>>();
        }

        public static void InitSession()
        {
            AppointSession = new AppointSession();
            MiaoSession = new MiaoSession();
        }

        private static List<string> GetDateList()
        {
            if (PlatformSession.ContainsKey("DateList"))
            {
                var dateList = PlatformSession["DateList"] as List<DspVal>;
                return dateList.Select(d => d.Value).ToList();
            }

            return new List<string>();
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
