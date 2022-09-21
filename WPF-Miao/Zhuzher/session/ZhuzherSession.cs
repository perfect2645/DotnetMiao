using Base.Events;
using Base.session;

namespace Zhuzher.session
{
    internal class ZhuzherSession : ISessionContainer
    {
        public static LogEvents PrintLogEvent { get; set; }
    }
}
