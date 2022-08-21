using Base.container;
using Base.Events;
using Base.logging;
using Base.viewModel.hospital;
using CoreControl.LogConsole;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
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

        public Action SelectedDepartmentChanged { get; set; }

        public LogEvents PrintLogEvent { get; set; }

        private string _title = "请先选择医院";
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                NotifyUI(() => Title);
            }
        }

        private List<HospitalDept> _departments;
        public List<HospitalDept> Departments
        {
            get { return _departments; }
            set
            {
                _departments = value;
                NotifyUI(() => Departments);
            }
        }

        private HospitalDept _selectedDepartment;
        public HospitalDept SelectedDepartment
        {
            get { return _selectedDepartment; }
            set
            {
                _selectedDepartment = value;
                NotifyUI(() => SelectedDepartment);
                SelectedDepartmentChanged?.Invoke();
            }
        }

        #endregion Properties

        #region Constructor

        public ViewModelBase(LogPanel logPanel)
        {
            LogPanel = logPanel;
            SessionItem = ContainerBase.ServiceProvider.GetService<ISessionItem>();
            PrintLogEvent = new LogEvents();
            PrintLogEvent.Subscribe(PrintLog);
        }

        #endregion Constructor

        #region Log

        public void ClearLogs()
        {
            //LogPanel.ClearValue();
        }

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
