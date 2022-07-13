using HttpProcessor.Container;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_Miao.Platform.yunnan.helper;

namespace WPF_Miao.Platform.yunnan.view
{
    internal class YunnanViewModel
    {
        public ICommand AppointmentCommand { get; set; }
        public Action<string> WriteLogAction { get; }

        public YunnanViewModel(Action<string> writeLogAction)
        {
            AppointmentCommand = new DelegateCommand(ExecuteAppointment, CanExecuteAppointment);
            WriteLogAction = writeLogAction;
        }


        #region Appointment

        private void ExecuteAppointment()
        {
            Log("Appointment Start");
            Task.Factory.StartNew(AppointmentAsync);
        }

        private void Log(string logStr)
        {
            LogHelper.PrintLog(WriteLogAction, logStr);
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
    }
}
