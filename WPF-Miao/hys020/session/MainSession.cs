using Base.session;
using Base.viewmodel.status;
using hys020.appointment;
using System.Collections.Generic;
using Utils;

namespace hys020.session
{
    internal class MainSession : MainSessionBase, ISessionContainer
    {
        public static string Cookie { get; set; }
        public static Dictionary<string, object> PlatformSession { get; private set; }
        public static Dictionary<string, object> MiaoSession { get; private set; }

        public static AppointEvent AppointEvent { get; }

        static MainSession()
        {
            PlatformSession = new Dictionary<string, object>();
            MiaoSession = new Dictionary<string, object>();
            AppointEvent = new AppointEvent();
        }

        #region MiaoSession

        public static void AddMiaoSession(Dictionary<string, object> dicValue)
        {
            MiaoSession.AddOrUpdate(dicValue);
        }

        public static void AddMiaoSession(string key, object value)
        {
            MiaoSession.AddOrUpdate(key, value);
        }

        #endregion MiaoSession

        #region MiaoStatus

        public static void SetStatus(MiaoProgress miaoProgress)
        {
            MiaoStatus.MiaoProgress = miaoProgress;
        }

        public static void SetStatus(MiaoProgress miaoProgress, object data)
        {
            MiaoStatus.OnStatusUpdate(miaoProgress, data);
        }

        #endregion MiaoStatus
    }
}
