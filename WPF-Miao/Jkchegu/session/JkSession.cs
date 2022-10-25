using Base.Events;
using Base.session;
using System;
using System.Collections.Generic;

namespace Jkchegu.session
{
    internal class JkSession : ISessionContainer
    {
        public static string Cookie { get; set; }
        public static LogEvents PrintLogEvent { get; set; }
        public static Dictionary<string, object> MiaoSession { get; set; }
        public static Dictionary<string, object> PlatformSession { get; set; }

        static JkSession()
        {
            PlatformSession = new Dictionary<string, object>();
            MiaoSession = new Dictionary<string, object>();
        }

    }
}
