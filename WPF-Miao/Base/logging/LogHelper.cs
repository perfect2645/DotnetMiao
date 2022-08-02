using System;

namespace Base.logging
{
    public static class LogHelper
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
