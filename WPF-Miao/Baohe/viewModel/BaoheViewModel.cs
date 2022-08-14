using Baohe.appointment;
using Baohe.constants;
using Baohe.search;
using Baohe.search.auth;
using Baohe.session;
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
            InitPlatformSession();
            InitCommands();
        }

        private void InitPlatformSession()
        {
            try
            {
                GetAuth();
                BaoheSession.PlatformSesstion.Add(Constant.PlatformType, "9000370");
                BaoheSession.PlatformSesstion.Add(Constant.HospitalId, "1040231");

                TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                var tsStr = Convert.ToInt64(ts.TotalMilliseconds).ToString();
                var sessionTime = tsStr.Substring(0, 10);
                BaoheSession.PlatformSesstion.Add(Constant.SessionTime, sessionTime);
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

        private void InitCommands()
        {
            AppointmentCommand = new DelegateCommand(ExecuteAppointment, CanExecuteAppointment);
            SearchCommand = new DelegateCommand(ExecuteSearchAsync, CanExecuteSearch);
            SessionEvents.Instance.Subscribe(LogSession);
        }

        #endregion Constructor


        #region Auth

        private void GetAuth()
        {
            var authController = HttpServiceController.GetService<AuthController>();

            try
            {
                authController.GetAuthAsync();
            }
            catch (AggregateException ex)
            {
                Log(ex);
            }
        }

        #endregion Auth

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
                appContr.AppointmentAsync(SessionItem).Wait();
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

                var appointNumbers = HttpServiceController.GetService<AppointNumbersController>();
                await appointNumbers.GetNumbersAsync(SessionItem);
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
