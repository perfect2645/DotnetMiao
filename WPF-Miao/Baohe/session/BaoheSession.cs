using Base.Events;
using System.Collections.Generic;
using Utils;

namespace Baohe.session
{
    public static class BaoheSession
    {
        public static string Cookie { get; set; }
        public static Dictionary<string, object> PlatformSesstion { get; private set; }
        public static MiaoSession MiaoSession { get; private set; }
        public static UserSession UserSession { get; private set; }
        public static OrderSession OrderSession { get; private set; }

        public static LogEvents PrintLogEvent { get; set; }

        static BaoheSession() 
        {
            PlatformSesstion = new Dictionary<string, object>();
            OrderSession = new OrderSession();
        }

        #region AddOrUpdate

        public static void AddMiaoSession(Dictionary<string, object> dicValue)
        {
            MiaoSession.SessionItem.SessionDic.AddOrUpdate(dicValue);
        }

        public static void AddUserSession(Dictionary<string, object> dicValue)
        {
            UserSession.SessionItem.SessionDic.AddOrUpdate(dicValue);
        }

        public static void AddUserSession(string key, object value)
        {
            UserSession.SessionItem.SessionDic.AddOrUpdate(key, value);
        }

        public static void AddMiaoSession(string key, object value)
        {
            MiaoSession.SessionItem.SessionDic.AddOrUpdate(key, value);
        }

        public static void BuildUserSession(string userid, Dictionary<string, object> dicValue)
        {
            UserSession = new UserSession(userid);
            UserSession.SessionItem.SessionDic.AddOrUpdate(dicValue);
        }

        public static void BuildMiaoSession(string deptId)
        {
            MiaoSession = new MiaoSession(deptId);
        }

        #endregion AddOrUpdate
    }
}
