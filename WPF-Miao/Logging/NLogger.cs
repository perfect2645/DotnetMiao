using NLog;


namespace Logging
{
    public static class NLogger
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        static NLogger()
        {
            //LogManager.Setup().LoadConfiguration(builder => {
            //    builder.ForLogger().FilterMinLevel(LogLevel.Info).WriteToConsole();
            //    builder.ForLogger().FilterMinLevel(LogLevel.Debug).WriteToFile(fileName: "nlog.txt");
            //});

            
        }

        public static void Info(string message)
        {
            Logger.Info(message);
        }

        public static void Debug(string message)
        {
            Logger.Debug(message);
        }
    }
}
