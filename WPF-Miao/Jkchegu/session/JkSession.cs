using Base.Events;
using Base.session;
using Jkchegu.appointment;
using System;
using System.Collections.Generic;

namespace Jkchegu.session
{
    internal class JkSession : MainSessionBase, ISessionContainer
    {
        public static string Cookie { get; set; }
        public static Dictionary<string, object> MiaoSession { get; set; }
        public static Dictionary<string, object> PlatformSession { get; set; }
        public static AppointEvent AppointEvent { get; }

        static JkSession()
        {
            PlatformSession = new Dictionary<string, object>();
            MiaoSession = new Dictionary<string, object>();
            AppointEvent = new AppointEvent();
        }

    }
}
