using Base.logging;
using CoreControl.LogConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Utils;

namespace Base.viewModel
{
    public abstract class ViewModelBase : NotifyChanged
    {
        #region Properties

        public ICommand SaveLogCommand { get; set; }
        public LogPanel LogPanel { get; private set; }

        #endregion Properties

        #region Constructor

        public ViewModelBase(LogPanel logPanel)
        {
            LogPanel = logPanel;
        }

        #endregion Constructor

        #region Log

        public void Log(Exception ex)
        {
            if (ex.InnerException != null)
            {
                Log(ex.InnerException);
                return;
            }
            var logStr = string.Empty;
            LogHelper.PrintLog(LogPanel.WriteLogAction, logStr);
        }

        public void Log(string logStr)
        {
            LogHelper.PrintLog(LogPanel.WriteLogAction, logStr);
        }

        private bool CanSaveLogs()
        {
            return true;
        }

        private void SaveLogs()
        {
            var logStr = LogPanel.GetRichText();
            ExportLog(logStr);
        }

        private void ExportLog(string content)
        {

        }

        #endregion Log
    }
}
