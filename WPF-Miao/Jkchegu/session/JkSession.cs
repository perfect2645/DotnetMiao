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

        static JkSession()
        {
            ClearMiaoSession();
        }

        public static void ClearMiaoSession()
        {
            MiaoSession = new Dictionary<string, object>();
        }
    }
}
