using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Miao.Platform.yunnan.helper
{
    internal static class LogHelper
    {
        public static void PrintLog(Action<string>? writeLogAction, string logStr)
        {
            writeLogAction?.Invoke(logStr);
            Logging.GLog.Logger.Info(logStr);
        }

        public static void PrintWarn(Action<string>? writeLogAction, string logStr)
        {
            writeLogAction?.Invoke(logStr);
            Logging.GLog.Logger.Warn(logStr);
        }

        public static void PrintErr(Action<string>? writeLogAction, string logStr)
        {
            writeLogAction?.Invoke(logStr);
            Logging.GLog.Logger.Error(logStr);
        }
    }
}
