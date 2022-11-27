using Base.session;
using System.Collections.Generic;

namespace JkzlSearcher.session
{
    internal class MainSession : MainSessionBase
    {
        public static string Cookie { get; set; }
        public static int CurrentHospitalId { get; private set; } = 1000000;
        private static readonly object _hospitalIdLock = new object();
        public static Dictionary<string, object> PlatformSession { get; private set; }
        static MainSession()
        {
            PlatformSession = new Dictionary<string, object>();
        }

        public static string GetNextHospitalId()
        {
            lock(_hospitalIdLock)
            {
                CurrentHospitalId++;
                return CurrentHospitalId.ToString();
            }
        }
    }
}
