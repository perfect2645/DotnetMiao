using Baohe.appointment;
using Base.viewModel;
using CoreControl.LogConsole;
using HttpProcessor.Container;
using Prism.Commands;
using System;
using System.Windows.Input;

namespace Baohe.viewModel
{
    internal class BaoheViewModel: ViewModelBase
    {
        #region Properties

        public ICommand AppointmentCommand { get; set; }

        #endregion Properties

        #region Constructor

        public BaoheViewModel(LogPanel logPanel) : base(logPanel)
        {
            Log("BaoheViewModel start");
            InitCommands();
        }

        private void InitCommands()
        {
            AppointmentCommand = new DelegateCommand(ExecuteAppointment, CanExecuteAppointment);
        }


        #endregion Constructor

        #region Appointment

        private bool CanExecuteAppointment()
        {
            return true;
        }

        private void ExecuteAppointment()
        {
            var appContr = HttpServiceController.GetService<AppointmentController>();

            try
            {
                appContr.AppointmentAsync().Wait();
            }
            catch (AggregateException ex)
            {
                Log(ex);
            }
        }


        #endregion Appointment
    }
}
