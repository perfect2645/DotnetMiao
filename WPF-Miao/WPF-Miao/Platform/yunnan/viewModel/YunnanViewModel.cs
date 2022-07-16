﻿using CoreControl.LogConsole;
using HttpProcessor.Container;
using Prism.Commands;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Utils;
using WPF_Miao.Platform.yunnan.helper;

namespace WPF_Miao.Platform.yunnan.viewModel
{
    internal class YunnanViewModel : NotifyChanged
    {
        #region Properties
        public ICommand AppointmentCommand { get; set; }
        public ICommand SaveLogCommand { get; set; }
        //public Action<string> WriteLogAction { get; }
        public LogPanel LogPanel { get; set; }

        public ISessionItem SessionItem { get; set;}

        #endregion Properties

        #region Constructor

        public YunnanViewModel(LogPanel logPanel, ISessionItem sessionItem)
        {
            SessionItem = sessionItem;
            LogPanel = logPanel;
            AppointmentCommand = new DelegateCommand(ExecuteAppointment, CanExecuteAppointment);
            SaveLogCommand = new DelegateCommand(SaveLogs, CanSaveLogs);
        }

        #endregion Constructor

        #region Appointment

        private void ExecuteAppointment()
        {
            Log("Appointment Start");
            Task.Factory.StartNew(AppointmentAsync);
        }

        private void AppointmentAsync()
        {
            var appContr = HttpServiceController.GetService<AppointmentController>();
            appContr.AppointmentAsync().Wait();
        }

        private bool CanExecuteAppointment()
        {
            return true;
        }

        #endregion Appointment

        #region Log

        private void Log(string logStr)
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
