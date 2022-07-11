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
        public Action<string> WriteLineAction { get; set; }

        public ICommand AppointmentCommand { get; set; }

        public YunnanViewModel()
        {
            AppointmentCommand = new DelegateCommand(ExecuteAppointment, CanExecuteAppointment);
        }


        #region Appointment

        private void ExecuteAppointment()
        {
        }

        private bool CanExecuteAppointment()
        {
            return true;
        }

        #endregion Appointment
    }
}
