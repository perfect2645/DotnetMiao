using Base.Events;
using System.Collections.Generic;

namespace Base.session
{
    public interface ISessionContainer
    {
        static string Cookie { get; set; }
        static Dictionary<string, object> PlatformSession { get; set; }
        static ISession MiaoSession { get; set; }
        static ISession UserSession { get; set; }

        static ISession OrderSession { get; set; }

        static LogEvents PrintLogEvent { get; set; }

    }
}
