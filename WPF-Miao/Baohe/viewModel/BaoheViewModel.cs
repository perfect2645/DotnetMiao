using Baohe.appointment;
using Baohe.constants;
using Baohe.search;
using Baohe.search.auth;
using Baohe.session;
using Baohe.verification;
using Baohe.viewModel.platform;
using Base.Events;
using Base.viewModel;
using Base.viewModel.hospital;
using CoreControl.LogConsole;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Utils;
using Utils.datetime;
using Utils.stringBuilder;

namespace Baohe.viewModel
{
    internal class BaoheViewModel: ViewModelBase
    {
        #region Properties

        public ICommand AppointmentCommand { get; set; }
        public ICommand SearchCommand { get; set; }

        public ICommand AutoRunCommand { get; set; }

        public VerifyCode VerifyCode { get; set; }

        private string _retId;
        public string RetId
        {
            get { return _retId; }
            set
            {
                _retId = value;
                BaoheSession.PlatformSesstion.AddOrUpdate(Constant.RetId, value);
                NotifyUI(() => RetId);
            }
        }

        private SearchController SearchController;

        #endregion Properties

        #region Constructor

        public BaoheViewModel(LogPanel logPanel) : base(logPanel)
        {
            InitCommands();
            InitStaticData();
            VerifyCode = new VerifyCode(logPanel);
            BaoheSession.PrintLogEvent = PrintLogEvent;


            TestData();
        }

        private void TestData()
        {
            Cookie = "YiHu_OpenId=eyJPcGVuSUQiOiJvZmY2dHMxTHFHaG1RaE5PY2dKb3d4NGtQUldNIiwiU2VjU3RyIjoiMzEwMDk3MjcwNTYxRDcwNTQxNDg3MkU4RDY0MUVDMDcifQ%3D%3D; logintype=62; loginprovinceid=0; logincityid=0; loginid=off6ts1LqGhmQhNOcgJowx4kPRWM; OpenID=off6ts1LqGhmQhNOcgJowx4kPRWM; BaseDoctorUid=0; BaseUserType=0; LoginChannel=1000031; YiHu_UserJosn=eyJBY2NvdW50U24iOiIxNDg1MjgxMjkiLCJDYXJkTnVtYmVyIjoiMjA3ODYzMTUzMyIsIkxvZ2luSWQiOiJvZmY2dHMxTHFHaG1RaE5PY2dKb3d4NGtQUldNIiwiVXNlck5hbWUiOiIiLCJTZWNTdHIiOiJDNTUzQjkyQzU4MDM1QUJDRDBBRUM4QzU5MUNDRDQyQiJ9; TOKEN_5D0161F2EB225D58BD7D4CE01260C0E2=2373522205B64A25980A6103C8134EFE; _YyghSysTrackUUID=17220631711; jkzlAn_uuid=F59F6FFD-123E-4E0E-AFF7-B53DB44204BD; jkzlAn_sid=615CDAD0-4024-4B2F-AC41-6615D0CF3896; jkzlAn_channelid=1000031; jkzlAn_userid=148528129; jkzlAn_p=-1; jkzlAn_c=-1; jkzlAn_ct=1668694130373";
            SearchTime = DateTime.Now.AddSeconds(20);
        }

        private void InitStaticData()
        {
            SearchTime = new DateTime(2022, 11, 18, 7, 59, 58);

            Departments = new List<HospitalDept>();
            Departments.Add(new Jiankangzhilu("1000031", "包河区包公街道",
                "1094417", "雨花社区卫生服务中心",
                "7225728", "九价Hpv"));

            Departments.Add(new Jiankangzhilu("1000031", "江苏南京雨花台区",
                "109910", "雨花经济开发区社区卫生服务中心",
                "7235344", "九价Hpv"));
            Departments.Add(new Jiankangzhilu("1000031", "江苏南京雨花台区",
                "109910", "雨花经济开发区社区卫生服务中心",
                "7235355", "预防接种门诊（测试）"));
            //        Departments.Add(new Jiankangzhilu("9001026", "蜀山区井岗中心服务号",
            //"1047063", "蜀山区经开区井岗镇社区卫生服务中心",
            //"7229244", "四价Hpv"));

            //        Departments.Add(new Jiankangzhilu("9001026", "蜀山区井岗中心服务号",
            //            "1047063", "蜀山区经开区井岗镇社区卫生服务中心",
            //            "7209050", "(测试)儿童保健科"));

            //        Departments.Add(new Jiankangzhilu("9000370", "蜀山区南岗镇卫生院",
            //            "1040231", "蜀山区南岗镇卫生院",
            //            "7211892", "四价Hpv"));

            //        Departments.Add(new Jiankangzhilu("9000370", "蜀山区南岗镇卫生院",
            //            "1040231", "蜀山区南岗镇卫生院",
            //            "7211903", "九价Hpv"));

            //        Departments.Add(new Jiankangzhilu("9000370", "蜀山区南岗镇卫生院",
            //            "1040231", "蜀山区南岗镇卫生院",
            //            "7175975", "(测试)儿童保健科"));

            //        Departments.Add(new Jiankangzhilu("9000370", "蜀山区南岗镇卫生院",
            //            "1040231", "蜀山区南岗镇卫生院",
            //            "7215685", "(测试)国产二价"));

            //        Departments.Add(new Jiankangzhilu("9000393", "包河区包公街道",
            //            "1039346", "包河区包公街道社区服务中心",
            //            "7229969", "九价Hpv"));

            InitPlatformSession();
        }

        private void InitPlatformSession()
        {
            try
            {
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

        private void InitCommands()
        {
            AppointmentCommand = new DelegateCommand(ExecuteAppointment, CanExecuteAppointment);
            SearchCommand = new DelegateCommand(ExecuteSearchAsync, CanExecuteSearch);

            AutoRunCommand = new DelegateCommand(ExecuteAutoRun);

            SessionEvents.Instance.Subscribe(LogSession);

            SelectedDepartmentChanged = new Action(OnSelectedDepartmentChanged);
            StartTimeChanged = new Action<DateTime?>(OnStartTimeChanged);
        }

        #endregion Constructor

        #region Appointment

        private bool CanExecuteAppointment()
        {
            return true;
        }

        private void ExecuteAppointment()
        {

            var appRouter = new AppointmentRouter(SessionItem);

            appRouter.AppointTickAsync();
        }


        #endregion Appointment

        #region Search

        private void SetSearchTimers()
        {
            SearchController.SetTimer();
            VerifyCode.SetTimer();
        }

        private void StopSearchTimers()
        {
            SearchController.StopTimer();
            VerifyCode.StopTimer();
        }

        private bool CanExecuteSearch()
        {
            return true;
        }

        private async void ExecuteSearchAsync()
        {
            try
            {
                BaoheSession.Cookie = SessionItem.Cookie;
                SearchController = HttpServiceController.GetService<SearchController>();
                ;
                SetSearchTimers();
                await SearchController.SearchAllAsync(SessionItem);
            }
            catch (HttpException ex)
            {
                StopSearchTimers();
                Log(ex);
            }
            catch (Exception ex)
            {
                StopSearchTimers();
                Log(ex);
            }
        }

        #endregion Search

        #region AutoRun

        private void ExecuteAutoRun()
        {
            BaoheSession.Cookie = SessionItem.Cookie;
            Task.Factory.StartNew(async () =>
            {
                await AutoRunAsync();
            });
        }

        private async Task AutoRunAsync()
        {
            SearchController = HttpServiceController.GetService<SearchController>();
            SetSearchTimers();
            await SearchController.AutoSearchAsync(SessionItem);
        }

        #endregion AutoRun

        #region Session

        private void LogSession(object? sender, SesstionEventArgs args)
        {

        }

        #endregion Session

        #region Hospital Dept

        private void OnSelectedDepartmentChanged()
        {
            var selectedDept = SelectedDepartment as Jiankangzhilu;
            BaoheSession.PlatformSesstion.AddOrUpdate(Constant.PlatformType, selectedDept.PlatformId);
            BaoheSession.PlatformSesstion.AddOrUpdate(Constant.HospitalId, selectedDept.HospitalId);
            BaoheSession.PlatformSesstion.AddOrUpdate(Constant.DeptId, selectedDept.DepartmentId);
            BaoheSession.PlatformSesstion.AddOrUpdate(Constant.Department, selectedDept);

            Log(selectedDept.ToLogString());

            BaoheSession.BuildMiaoSession(BaoheSession.PlatformSesstion[Constant.DeptId].NotNullString());
        }

        #endregion Hospital Dept

        #region Start Time

        private void OnStartTimeChanged(DateTime? selectedTime)
        {
            BaoheSession.PlatformSesstion.AddOrUpdate(Constant.StartTime, selectedTime!);
        }

        #endregion Start Time

    }
}
