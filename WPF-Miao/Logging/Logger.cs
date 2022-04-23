using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;

namespace Logging
{
    public static class Logger
    {

        public static ILog m_log;
        static Logger()
        {
            XmlConfigurator.Configure();
            Type type = MethodBase.GetCurrentMethod().DeclaringType;
            m_log = LogManager.GetLogger(type);
            m_log.Debug("这是一个Debug日志");
            m_log.Info("这是一个Info日志");
            m_log.Warn("这是一个Warn日志");
            m_log.Error("这是一个Error日志");
            m_log.Fatal("这是一个Fatal日志");
        }
    }
}
