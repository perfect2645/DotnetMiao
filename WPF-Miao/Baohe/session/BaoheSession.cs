using Base.viewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Baohe.session
{
    public static class BaoheSession
    {
        public static Dictionary<string, string> PlatformSesstion { get; private set; }
        public static Dictionary<string, ISessionItem> Session { get; private set; }

        static BaoheSession() 
        {
            PlatformSesstion = new Dictionary<string, string>();
            Session = new Dictionary<string, ISessionItem>();
        }

        #region AddOrUpdate

        public static void AddOrUpdate(ISessionItem sessionItem, Dictionary<string, object> dicValue)
        {
            sessionItem.SessionDic.AddOrUpdate(dicValue);
            if (!Session.ContainsKey(sessionItem.Key))
            {
                Session.AddOrUpdate(sessionItem.Key, sessionItem);
            }
        }

        #endregion AddOrUpdate

        public static void Clear(string key)
        {
            Session.Remove(key);
        }
    }
}
