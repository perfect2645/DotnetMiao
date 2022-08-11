using Baohe.appointment;
using Baohe.search;
using Base.Events;
using Base.viewModel;
using CoreControl.LogConsole;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
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
            SearchCommand = new DelegateCommand(ExecuteSearchAsync, CanExecuteSearch);
            SessionEvents.Instance.Subscribe(LogSession);
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

        private async void ExecuteSearchAsync()
        {
            try
            {
                //var searchContr = HttpServiceController.GetService<SearchController>();
                var userInfoContr = HttpServiceController.GetService<UserInfoController>();
                await userInfoContr.GetUserInfoAsync(SessionItem);
            }
            catch (HttpException ex)
            {
                Log(ex);
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        #endregion Search

        #region Session

        private void LogSession(object? sender, SesstionEventArgs args)
        {

        }

        #endregion Session
    }
}
