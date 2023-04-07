using Base.Events;
using Base.viewmodel.status;
using System;

namespace Base.session
{
    public class MainSessionBase
    {
        public static LogEvents PrintLogEvent { get; set; }
        public static MiaoStatus MiaoStatus { get; }

        public static DateTime StartTime { get; set; }

        static MainSessionBase()
        {
            MiaoStatus = new MiaoStatus();
        }
    }
}
