using NLog;
using NLog.Layouts;
using NLog.Targets;
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
        public string FileName { get; private set; }
        public string LogLayout { get; private set; } = "${message}";

        public NLogTemplate(string fileName)
        {
            FileName = fileName;
            Setup();
            Logger = LogManager.GetCurrentClassLogger();
        }

        public NLogTemplate(string fileName, string layout)
        {
            FileName = fileName;
            LogLayout = layout;
            Setup();
            Logger = LogManager.GetCurrentClassLogger();
        }

        public void Setup()
        {
            LogManager.Setup().LoadConfiguration(builder =>
            {
                builder.ForLogger().FilterMinLevel(LogLevel.Info).WriteToFile(
                    fileName: FileName,
                    layout: Layout.FromString(LogLayout));
                //builder.ForLogger().FilterMinLevel(LogLevel.Debug).WriteToFile(fileName: "NLogTemplate.log");
            });
        }
    }
}
