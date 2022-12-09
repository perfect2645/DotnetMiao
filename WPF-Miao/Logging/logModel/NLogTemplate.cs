using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging.logModel
{
    public class NLogTemplate
    {
        public Logger Logger {get; private set;}
        public string FileName { get; set; }

        public NLogTemplate()
        {
            Logger = LogManager.GetCurrentClassLogger();
            Setup();
        }

        public void Setup()
        {
            LogManager.Setup().LoadConfiguration(builder =>
            {
                builder.ForLogger().FilterMinLevel(LogLevel.Info).WriteToConsole();
                builder.ForLogger().FilterMinLevel(LogLevel.Debug).WriteToFile(fileName: FileName);
            });
        }
    }
}
