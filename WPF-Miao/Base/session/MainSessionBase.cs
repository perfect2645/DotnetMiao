using Base.Events;
using renren.viewmodel;

namespace Base.session
{
    public class MainSessionBase
    {
        public static LogEvents PrintLogEvent { get; set; }
        public static MiaoStatus MiaoStatus { get; }

        static MainSessionBase()
        {
            MiaoStatus = new MiaoStatus();
        }
    }
}
