using Base.Events;
using System.Collections.Generic;

namespace HuSheng.session
{
    internal class HushengSession
    {
        public static string Cookie { get; set; }
        public static LogEvents PrintLogEvent { get; set; }
        public static Dictionary<string, object> MiaoSession { get; set; }

        static HushengSession()
        {
            ClearMiaoSession();
        }

        public static void ClearMiaoSession()
        {
            MiaoSession = new Dictionary<string, object>();
        }
    }
}
