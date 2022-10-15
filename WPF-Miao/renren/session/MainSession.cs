using Base.Events;
using Base.session;

namespace renren.session
{
    internal class MainSession : ISessionContainer
    {
        public static string Cookie { get; set; }
        public static LogEvents PrintLogEvent { get; set; }
    }
}
