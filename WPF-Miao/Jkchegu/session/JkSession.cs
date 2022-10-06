using Base.Events;
using Base.session;

namespace Jkchegu.session
{
    internal class JkSession : ISessionContainer
    {
        public static string Cookie { get; set; }
        public static LogEvents PrintLogEvent { get; set; }
    }
}
