using Baohe.appointment;
using Baohe.constants;
using Baohe.search;
using Baohe.search.ArrangeWater;
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
using Utils;
using Utils.datetime;

namespace Baohe.viewModel
{
    internal class BaoheViewModel: ViewModelBase
    {
        #region Properties

        public ICommand AppointmentCommand { get; set; }

        public ICommand SearchCommand { get; set; }

        public ICommand TestCommand { get; set; }

        #endregion Properties

        #region Constructor

        public BaoheViewModel(LogPanel logPanel) : base(logPanel)
        {
            Log("BaoheViewModel start");
            InitPlatformSessionAsync();
            InitCommands();
        }

        private async void InitPlatformSessionAsync()
        {
            try
            {
                await GetAuthAsync();
                SetPlatFormSession4JIa();

                var tsStr = DateTimeUtil.GetTimeStamp();
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

        protected virtual void SetPlatFormSessionTest()
        {
            Log("切换到测试科室");
            //BaoheSession.PlatformSesstion.Add(Constant.PlatformType, "9000370");
            //BaoheSession.PlatformSesstion.Add(Constant.HospitalId, "1040231");
            //BaoheSession.PlatformSesstion.Add(Constant.DeptId, "7175975");

            //https://appoint.yihu.com/appoint/hospital/ghDeptList.html?platformType=9001026&hospitalId=1047063&time=1660836185

            BaoheSession.PlatformSesstion.AddOrUpdate(Constant.PlatformType, "9001026");
            BaoheSession.PlatformSesstion.AddOrUpdate(Constant.HospitalId, "1047063");
            BaoheSession.PlatformSesstion.AddOrUpdate(Constant.DeptId, "7209050");

            Log($"{Constant.PlatformType} : {BaoheSession.PlatformSesstion[Constant.PlatformType]}");
            Log($"{Constant.HospitalId} : {BaoheSession.PlatformSesstion[Constant.HospitalId]}");
            Log($"{Constant.DeptId} : {BaoheSession.PlatformSesstion[Constant.DeptId]}");
        }

        protected virtual void SetPlatFormSession4JIa()
        {
            Log("切换到4价");
            //https://appoint.yihu.com/appoint/doctor/ghDoctorList.html?platformType=9001026&deptId=7229244&hospitalId=1047063&exConsult=&consultHosId=1047063

            BaoheSession.PlatformSesstion.AddOrUpdate(Constant.PlatformType, "9001026");
            BaoheSession.PlatformSesstion.AddOrUpdate(Constant.HospitalId, "1047063");
            BaoheSession.PlatformSesstion.AddOrUpdate(Constant.DeptId, "7229244");

            Log($"{Constant.PlatformType} : {BaoheSession.PlatformSesstion[Constant.PlatformType]}");
            Log($"{Constant.HospitalId} : {BaoheSession.PlatformSesstion[Constant.HospitalId]}");
            Log($"{Constant.DeptId} : {BaoheSession.PlatformSesstion[Constant.DeptId]}");
        }

        private void InitCommands()
        {
            AppointmentCommand = new DelegateCommand(ExecuteAppointment, CanExecuteAppointment);
            SearchCommand = new DelegateCommand(ExecuteSearchAsync, CanExecuteSearch);
            TestCommand = new DelegateCommand(ExecuteTest);
            SessionEvents.Instance.Subscribe(LogSession);
        }

        #endregion Constructor

        #region Auth

        private async Task GetAuthAsync()
        {
            var authController = HttpServiceController.GetService<AuthController>();

            try
            {
                await authController.GetAuthAsync();
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

            var appRouter = new AppointmentRouter(SessionItem);

            appRouter.AppTimer.Start();
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
                //SetPlatFormSession4JIa();
                var searchController = HttpServiceController.GetService<SearchController>();
                await searchController.SearchAllAsync(SessionItem);
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

        #region Test

        private void ExecuteTest()
        {
            SetPlatFormSessionTest();
        }

        #endregion Test
    }
}
