using Base.viewModel;
using System.Collections.Generic;
using Utils;

namespace Baohe.session
{
    public static class BaoheSession
    {
        public static Dictionary<string, object> PlatformSesstion { get; private set; }
        public static Dictionary<string, ISessionItem> MiaoSession { get; private set; }
        public static Dictionary<string, ISessionItem> UserSession { get; private set; }

        static BaoheSession() 
        {
            PlatformSesstion = new Dictionary<string, object>();
            MiaoSession = new Dictionary<string, ISessionItem>();
            UserSession = new Dictionary<string, ISessionItem>();
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

        #endregion AddOrUpdate

        public static void ClearMiaoSession(string key)
        {
            MiaoSession.Remove(key);
        }
    }
}
