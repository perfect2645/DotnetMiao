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

        static MainSession()
        {
            PlatformSesstion = new Dictionary<string, object>();
            UserSession = new UserSession("2645");
        }

        #region UserSession

        public static void BuildUserSession(string userid, Dictionary<string, object> dicValue)
        {
            UserSession = new UserSession(userid);
            UserSession.SessionItem.SessionDic.AddOrUpdate(dicValue);
        }

        public static void AddUserSession(Dictionary<string, object> dicValue)
        {
            UserSession.SessionItem.SessionDic.AddOrUpdate(dicValue);
        }

        public static void AddUserSession(string key, object value)
        {
            UserSession.SessionItem.SessionDic.AddOrUpdate(key, value);
        }

        #endregion UserSession

        #region MiaoStatus

        public static void SetStatus(MiaoProgress miaoProgress)
        {
            MiaoStatus.MiaoProgress = miaoProgress;
        }

        #endregion MiaoStatus
    }
}
