using Base.container;
using Base.Events;
using Base.logging;
using Base.viewModel.hospital;
using CoreControl.LogConsole;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Windows.Input;
using System.Windows.Threading;
using Utils;
using System.Windows.Controls;

namespace Base.viewModel
{
    public abstract class ViewModelBase : NotifyChanged
    {
        #region Properties

        public ICommand SaveLogCommand { get; set; }
        public LogPanel LogPanel { get; set; }

        public ISessionItem SessionItem { get; private set; }

        public Action SelectedDepartmentChanged { get; set; }
        public Action CookieChanged { get; set; }

        public Action<DateTime?> StartTimeChanged { get; set; }

        public LogEvents PrintLogEvent { get; set; }

        public UpdateUiEvent UpdateUiEvent { get; set; }

        private string _cookie;
        public string Cookie
        {
            get { return _cookie; }
            set
            {
                _cookie = value;
                NotifyUI(() => Cookie);
                CookieChanged?.Invoke();
            }
        }

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

        private DateTime? _startTime = DateTime.Now;
        public DateTime? StartTime
        {
            get { return _startTime; }
            set
            {
                if (value == _startTime)
                {
                    return;
                }
                _startTime = value;
                StartTimeChanged?.Invoke(value);
                NotifyUI(() => StartTime);
            }
        }

        #endregion Properties

        #region Constructor

        public ViewModelBase(LogPanel logPanel)
        {
            LogPanel = logPanel;
            SessionItem = ContainerBase.ServiceProvider?.GetService<ISessionItem>();
            PrintLogEvent = new LogEvents();
            PrintLogEvent.Subscribe(PrintLog);

            UpdateUiEvent = new UpdateUiEvent();
            UpdateUiEvent.Subscribe(OnUpdateUI);
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

        protected void PrintLog(string log)
        {
            var args = new LogEventArgs(log);
            LogHelper.PrintLog(LogPanel.WriteLogAction, args);
        }

        public void Log(Exception ex)
        {
            if (ex.InnerException != null)
            {
                Log(ex.InnerException);
                return;
            }

            LogHelper.PrintErr(LogPanel.WriteLogAction, ex.Message, ex.StackTrace ?? "No Stacktrace");
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

        #region Update UI

        private void OnUpdateUI(object? sender, UiEventArgs e)
        {
            UpdateUI(e);
        }

        protected virtual void UpdateUI(UiEventArgs e)
        {
        }

        #endregion Update UI

        #region IP

        public string GetIP()
        {
            IPAddress[] ip = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress address in ip)
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    return address.ToString();
                }
            }

            return string.Empty;
        }

        #endregion IP
    }
}
