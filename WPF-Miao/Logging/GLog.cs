using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;

namespace Logging
{
    public static class GLog
    {
        #region Properties

        private static ILog _logger;

        private static ConsoleAppender _consoleAppender;
        private static RollingFileAppender _rollingFileAppender;

        private static string _loggerLayout = "%date{yyyy-MM-dd HH:mm:ss,fff}[%level][%class][%method]:%message%newline";
        public static string LoggerLayout
        {
            set {  _loggerLayout = value; }
        }

        #endregion Properties

        static GLog()
        {
            InitLog4NetConfig();
        }


        #region Init Logger

        private static void InitLog4NetConfig()
        {
            InitConsoleLogger();
            InitRolllingFileLogger();
        }

        private static void InitConsoleLogger()
        {
            var consoleLayout = new PatternLayout();
            consoleLayout.ActivateOptions();

            _consoleAppender = new ConsoleAppender()
            {
                Name = "ConsoleAppender",
                Layout = consoleLayout,
                Threshold = Level.All,
            };

            _consoleAppender.ActivateOptions();
            BasicConfigurator.Configure(_consoleAppender);
        }

        private static void InitRolllingFileLogger()
        {
            var rollingFileLayout = new PatternLayout()
            {
                ConversionPattern = _loggerLayout,
            };
            rollingFileLayout.ActivateOptions();

            _rollingFileAppender = new RollingFileAppender()
            {
                Name = "RollingFileAppender",
                Layout = rollingFileLayout,
                Threshold = Level.All,
                File = "TestLogger.log",
                AppendToFile = true,
                MaximumFileSize = "1MB",
                MaxSizeRollBackups = 20,
            };

            _rollingFileAppender.ActivateOptions();
            BasicConfigurator.Configure(_rollingFileAppender);
        }

        #endregion Init Logger

        #region Public

        public static ILog GetLogger()
        {
            if (_logger != null)
            {
                return _logger;
            }

            InitLog4NetConfig();
            //var st = new System.Diagnostics.StackTrace();
            //var frames = st.GetFrames();
            //var currentFrame = frames[1];
            //var currentType = currentFrame?.GetMethod()?.ReflectedType?.GetType();
            _logger = LogManager.GetLogger(GetLoggerType(null));

            return _logger;
        }

        public static ILog GetXmlLogger(Type type)
        {
            if (_logger != null)
            {
                return _logger;
            }

            XmlConfigurator.Configure();
            _logger = LogManager.GetLogger(type);

            return _logger;
        }

        #endregion Public

        #region Private

        private static Type GetLoggerType(Type? type)
        {
            if (type == null)
            {
                return typeof(GLog);
            }

            return type;
        }

        #endregion Private
    }
}
