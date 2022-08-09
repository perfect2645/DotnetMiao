using Baohe.appointment;
using Baohe.search;
using Base.viewModel;
using CoreControl.LogConsole;
using HttpProcessor.Container;
using Prism.Commands;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Baohe.viewModel
{
    internal class BaoheViewModel: ViewModelBase
    {
        #region Properties

        public ICommand AppointmentCommand { get; set; }

        public ICommand SearchCommand { get; set; }

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
            SearchCommand = new DelegateCommand(ExecuteSearch, CanExecuteSearch);
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

        #region Search

        private bool CanExecuteSearch()
        {
            return true;
        }

        private void ExecuteSearch()
        {
            //var searchContr = HttpServiceController.GetService<SearchController>();
            var userInfoContr = HttpServiceController.GetService<UserInfoController>();
            userInfoContr.GetUserInfoAsync();
        }

        #endregion Search
    }
}
