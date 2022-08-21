using Base.viewModel;
using System.Collections.Generic;
using Utils;

namespace Baohe.session
{
    public static class BaoheSession
    {
        public static Dictionary<string, object> PlatformSesstion { get; private set; }
        public static MiaoSession MiaoSession { get; private set; }
        public static UserSession UserSession { get; private set; }

        public static OrderSession OrderSession { get; private set; }

        static BaoheSession() 
        {
            PlatformSesstion = new Dictionary<string, object>();
            MiaoSession = new MiaoSession();
            UserSession = new UserSession();
            OrderSession = new OrderSession();
        }

        #region AddOrUpdate

        public static void AddMiaoSession(ISessionItem sessionItem, Dictionary<string, object> dicValue)
        {
            sessionItem.SessionDic.AddOrUpdate(dicValue);
            if (!MiaoSession.ContainsKey(sessionItem.Key))
            {
                MiaoSession.AddOrUpdate(sessionItem.Key, sessionItem);
            }
        }

        public static void AddUserSession(ISessionItem sessionItem, Dictionary<string, object> dicValue)
        {
            sessionItem.SessionDic.AddOrUpdate(dicValue);
            if (!UserSession.ContainsKey(sessionItem.Key))
            {
                UserSession.AddOrUpdate(sessionItem.Key, sessionItem);
            }
        }

        #endregion AddOrUpdate

        public static void ClearMiaoSession(string key)
        {
            MiaoSession.Remove(key);
        }
    }
}
