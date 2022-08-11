using Base.Events;
using System;
using System.Text;

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

        internal static void PrintLog(Action<string> writeLogAction, LogEventArgs e)
        {
            if (e?.Items == null)
            {
                return;
            }

            var sb = new StringBuilder();
            sb.AppendLine("Printing Start*********");
            foreach (var item in e.Items)
            {
                sb.AppendLine($"{item.Key}:{item.Value}");
            }
            sb.AppendLine("Printing End*********");
            Logging.GLog.Logger.Info(sb.ToString());
        }
    }
}
