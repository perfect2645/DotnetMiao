using Base.session;
using Base.viewmodel.status;
using System.Collections.Generic;
using Utils;

namespace renren.session
{
    internal class MainSession : MainSessionBase, ISessionContainer
    {
        public static string Cookie { get; set; }
        public static UserSession UserSession { get; private set; }
        public static Dictionary<string, object> PlatformSesstion { get; private set; }
        public static Dictionary<string, object> HospitalSession { get; private set; }

        static MainSession()
        {
            PlatformSesstion = new Dictionary<string, object>();
            UserSession = new UserSession("2645");
            HospitalSession = new Dictionary<string, object>();
        }

        #region UserSession

        public static void AddUserSession(Dictionary<string, object> dicValue)
        {
            UserSession.SessionItem.SessionDic.AddOrUpdate(dicValue);
        }

        public static void AddUserSession(string key, object value)
        {
            UserSession.SessionItem.SessionDic.AddOrUpdate(key, value);
        }

        #endregion UserSession

        #region HospitalSession

        public static void AddHospitalSession(Dictionary<string, object> dicValue)
        {
            HospitalSession.AddOrUpdate(dicValue);
        }

        public static void AddHospitalSession(string key, object value)
        {
            HospitalSession.AddOrUpdate(key, value);
        }

        #endregion HospitalSession

        #region MiaoStatus

        public static void SetStatus(MiaoProgress miaoProgress)
        {
            MiaoStatus.MiaoProgress = miaoProgress;
        }

        #endregion MiaoStatus
    }
}
