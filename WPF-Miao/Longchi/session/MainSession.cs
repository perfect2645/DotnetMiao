using Base.session;
using Base.viewmodel.status;
using Longchi.appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Longchi.session
{
    public class MainSession : MainSessionBase, ISessionContainer
    {
        public static Dictionary<string, object> PlatformSession { get; private set; }
        public static AppointSession AppointSession { get; private set; }
        internal static VerifyYuyueSession VerifyYuyueSession { get; private set; }
        internal static List<string> TimeIdList { get; set; }
        internal static List<string> DeptList { get; set; }
        internal static List<LongchiLogin> Users { get; set; }
        internal static Dictionary<string, List<Order>> Orders { get; set; }

        public static AppointEvent AppointEvent { get; }

        static MainSession()
        {
            PlatformSession = new Dictionary<string, object>();
            AppointEvent = new AppointEvent();
            Users = new List<LongchiLogin>();
            Orders = new Dictionary<string, List<Order>>();
            TimeIdList = new List<string>();
            DeptList = new List<string>();
        }
        
        public static void InitSession()
        {
            AppointSession = new AppointSession();
            VerifyYuyueSession = new VerifyYuyueSession();
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
