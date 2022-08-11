using Base.container;
using Base.Events;
using Base.logging;
using CoreControl.LogConsole;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Input;
using Utils;

namespace Base.viewModel
{
    public abstract class ViewModelBase : NotifyChanged
    {
        #region Properties

        public ICommand SaveLogCommand { get; set; }
        public LogPanel LogPanel { get; set; }

        public ISessionItem SessionItem { get; private set; }

        #endregion Properties

        #region Constructor

        public ViewModelBase(LogPanel logPanel)
        {
            LogPanel = logPanel;
            SessionItem = ContainerBase.ServiceProvider.GetService<ISessionItem>();
            SessionItem.PrintLogEvent.Subscribe(PrintLog);
        }

        #endregion Constructor

        #region Log

        protected virtual void PrintLog(object? sender, LogEventArgs e)
        {
            LogHelper.PrintLog(LogPanel.WriteLogAction, e);
        }

        public void Log(Exception ex)
        {
            if (ex.InnerException != null)
            {
                Log(ex.InnerException);
                return;
            }

            LogHelper.PrintLog(LogPanel.WriteLogAction, ex.Message);
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
