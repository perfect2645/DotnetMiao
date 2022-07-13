using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
            WriteLogAction?.Invoke("123");
        }

        private bool CanExecuteAppointment()
        {
            return true;
        }

        #endregion Appointment
    }
}
