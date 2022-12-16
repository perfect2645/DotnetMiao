using Baohe.constants;
using Base.Events;
using System;
using System.Collections.Generic;
using Utils;
using Utils.stringBuilder;

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

        public static UpdateUiEvent UpdateUiEvent { get; set; }

        public static bool IsYzmChecked { get; set; }

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

        public static DateTime GetStartTime()
        {
            var stTime = DateTime.Now;
            if (PlatformSesstion.ContainsKey(Constant.StartTime))
            {
                stTime = (PlatformSesstion[Constant.StartTime] as DateTime?) ?? DateTime.Now;
            }

            return stTime;
        }

        internal static int GetRandomSeed()
        {
            if (PlatformSesstion.ContainsKey(Constant.RandomSeed))
            {
                return PlatformSesstion[Constant.RandomSeed].NotNullString().ToInt();
            }

            return 0;
        }

        #region UpdateUI

        public static void UpdateUI(string filed, object value)
        {
            var updateUiArgs = new UiEventArgs
            {
                Field = filed,
                Value = value
            };
            UpdateUiEvent.Publish(null, updateUiArgs);
        }

        #endregion UpdateUI
    }
}
