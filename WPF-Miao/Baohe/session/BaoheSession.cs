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
        public static Dictionary<string, SessionItem> Session { get; private set; }

        static BaoheSession() 
        {
            Session = new Dictionary<string, SessionItem>();
        }

        #region AddOrUpdate

        public static void AddOrUpdate(string sessionKey, string value)
        {

        }

        public static void AddOrUpdate(string sessionKey, Dictionary<string, object> dicValue)
        {
            if (Session.ContainsKey(sessionKey))
            {
                Session[sessionKey].SessionDic.AddOrUpdate(dicValue);
            }

            var newSessionItem = new SessionItem(sessionKey);
            newSessionItem.SessionDic.AddOrUpdate(dicValue);
        }

        #endregion AddOrUpdate

        public static void Clear(string key)
        {
            Session.Remove(key);
        }
    }
}
