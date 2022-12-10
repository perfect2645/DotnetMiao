using Base.session;
using Logging.logModel;
using NLog;
using System.Collections.Generic;

namespace JkzlSearcher.session
{
    internal class MainSession : MainSessionBase
    {
        public static string Cookie { get; set; }
        public static Logger OutputLogger { get; private set; }
        public static int CurrentHospitalId { get; set; } = 1000037;
        private static readonly object _hospitalIdLock = new object();
        public static Dictionary<string, object> PlatformSession { get; private set; }
        static MainSession()
        {
            OutputLogger = new NLogTemplate("../../../output/JkzlHospital.json").Logger;
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

        public static void SaveHospital(string hosJson)
        {
            OutputLogger.Info(hosJson);
        }
    }
}
