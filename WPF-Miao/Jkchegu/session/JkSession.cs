using Base.Events;
using Base.session;
using Jkchegu.appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using Utils;
using Utils.stringBuilder;

namespace Jkchegu.session
{
    internal class JkSession : MainSessionBase, ISessionContainer
    {
        public static string Cookie { get; set; }
        public static Dictionary<string, object> MiaoSession { get; set; }
        public static Dictionary<string, object> PlatformSession { get; set; }
        public static AppointEvent AppointEvent { get; }
        public static List<User> UserList { get; }

        public static string DefaultEtid 
        { 
            get
            {
                return GetDefaultEtid();
            }
        }

        public static User DefaultUser
        {
            get
            {
                return GetDefaultUser();
            }
        }

        static JkSession()
        {
            PlatformSession = new Dictionary<string, object>();
            MiaoSession = new Dictionary<string, object>();
            AppointEvent = new AppointEvent();
            UserList = new List<User>();
            UserList.Add(new User("bd208cd181694e66b930a5f9b23fbd11", "周茉"));
            UserList.Add(new User("7bf4400434ea4e80a6dfb331f6f6a077", "张飞"));
            UserList.Add(new User("bfa272322e5d4423b3eb2bd1e86fd7ba", "周珊"));
            UserList.Add(new User("7c6529ef5b7a413eac86fa7bb6e35c8a", "周甜"));
            UserList.Add(new User("2698928a19b640aa820b80e3acab348c", "周依依"));
        }

        private static User GetDefaultUser()
        {
            return UserList.FirstOrDefault();
        }

        private static string GetDefaultEtid()
        {
            var sessionEtid = PlatformSession.GetString("Etid");
            if (!string.IsNullOrEmpty(sessionEtid))
            {
                return sessionEtid;
            }
            var defaultUser = UserList.FirstOrDefault();
            return defaultUser?.Etid;
        }

        public static void UpdateSession(string cookie)
        {
            foreach(var user in UserList)
            {
                user.Session = null;
            }

            if (string.IsNullOrEmpty(cookie))
            {
                PrintLogEvent.Publish(null, $"请设置Session");
            }

            var sessionList = cookie.SplitToList(";");
            if (!sessionList.HasItem())
            {
                PrintLogEvent.Publish(null, $"解析Session错误");
                return;
            }

            for (int i = 0; i < sessionList.Count; i++)
            {
                var userCount = UserList.Count;
                if (i >= userCount)
                {
                    PrintLogEvent.Publish(null, $"设置UserSession完成");
                    return;
                }
                UserList[i].Session = sessionList[i];
                PrintLogEvent.Publish(null, $"{UserList[i].ToLogString()}");
            }
        }
    }
}
