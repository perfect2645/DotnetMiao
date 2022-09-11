using Base.Events;
using System;
using System.Text;
using Utils;

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
            if (e?.Items == null && string.IsNullOrWhiteSpace(e.Message))
            {
                return;
            }

            if (!e.Items.HasItem() && !string.IsNullOrWhiteSpace(e.Message))
            {
                PrintLog(writeLogAction, e.Message);
                return;
            }

            var title = string.IsNullOrWhiteSpace(e.Message) ? string.Empty : e.Message;

            var sb = new StringBuilder();
            sb.AppendLine($"Printing {title} Start*********");
            foreach (var item in e.Items)
            {
                sb.AppendLine($"{item.Key}:{item.Value}");
            }
            sb.AppendLine($"Printing {title} End*********");

            var logStr = sb.ToString();
            writeLogAction?.Invoke(logStr);
            Logging.GLog.Logger.Info(logStr);
        }
    }
}
