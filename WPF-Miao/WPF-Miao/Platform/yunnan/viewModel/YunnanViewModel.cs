using CoreControl.LogConsole;
using HttpProcessor.Container;
using Prism.Commands;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Utils;
using WPF_Miao.Platform.yunnan.helper;
using WPF_Miao.Platform.yunnan.session;

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

        private string _contentMD5;

        public string ContentMD5
        {
            get { return _contentMD5; }
            set 
            {
                if (_contentMD5 == value)
                {
                    return;
                }
                _contentMD5 = value;
                NotifyUI(() => ContentMD5);
                ComputeMD5(_contentMD5);
            }
        }

        private string _md5Result;

        public string MD5Result
        {
            get { return _md5Result; }
            set
            {
                _md5Result = value;
                NotifyUI(() => MD5Result);
            }
        }


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
            YunnanSession.AddOrUpdate((SessionItem as SessionItem)!);
            Task.Factory.StartNew(AppointmentAsync);
        }

        private void AppointmentAsync()
        {
            var appContr = HttpServiceController.GetService<AppointmentController>();
            appContr.AppointmentAsync(SessionItem).Wait();
        }

        private bool CanExecuteAppointment()
        {
            return true;
        }

        #endregion Appointment

        #region Tool

        private void ComputeMD5(string contentMD5)
        {
            MD5Result = Encryptor.ToBase64Md5(contentMD5);
        }

        #endregion Tool

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
