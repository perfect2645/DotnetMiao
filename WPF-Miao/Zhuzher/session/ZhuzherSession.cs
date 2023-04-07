using Base.Events;
using Base.session;

namespace Zhuzher.session
{
    internal class ZhuzherSession : ISessionContainer
    {
        public static string Cookie { get; set; }
        public static string ActivityId { get; set; }
        public static string InviteCode { get; set; }
        public static LogEvents PrintLogEvent { get; set; }
    }
}
