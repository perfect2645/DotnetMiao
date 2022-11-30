using Baohe.appointment;
using Baohe.constants;
using Baohe.search;
using Baohe.session;
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
            Cookie = "jkzlAn_uuid=88844286-2FA2-443E-B7C7-1F2C45BE03DE; jkzlAn_p=-1; jkzlAn_c=-1; jkzlAn_userid=142687055; YiHu_OpenId=eyJPcGVuSUQiOiJvQTNaQTBhSkFnRkd6cS1LM2xmdXZBVVNqSFlvIiwiU2VjU3RyIjoiQURDODNDMjhCNkYzNEI0OUJEMDA1RTkwNTk2NDRBMjEifQ%3D%3D; logintype=62; loginprovinceid=0; logincityid=0; loginid=oA3ZA0aJAgFGzq-K3lfuvAUSjHYo; OpenID=oA3ZA0aJAgFGzq-K3lfuvAUSjHYo; BaseDoctorUid=0; BaseUserType=0; LoginChannel=9000393; YiHu_UserJosn=eyJBY2NvdW50U24iOiIxNDI2ODcwNTUiLCJDYXJkTnVtYmVyIjoiMjA3MjgwMDc2NiIsIkxvZ2luSWQiOiJvQTNaQTBhSkFnRkd6cS1LM2xmdXZBVVNqSFlvIiwiVXNlck5hbWUiOiIiLCJTZWNTdHIiOiJCQzYwQkJGNTdBQzNBQkMxQkI3M0U4MzNEQ0QxQTVENyJ9; TOKEN_74ECC05B9A8A9F247BE6CAEE69DEFB08=AC8A09CD823648AA97F6984D2EFA972F; _YyghSysTrackUUID=30181348433; jkzlAn_refercode=; jkzlAn_sid=F5C159F6-3F61-43AB-8FFA-D88533A85839; jkzlAn_channelid=9000393; jkzlAn_utm_source=0.0.h.1026.bus010.0__0.0.h.1026.bus010.0; jkzlAn_ct=1669803264963";

            SessionItem.Referer = "https://appoint.yihu.com/appoint/doctor/doctorArrange.html?deptId=7235355&doctorId=710796524&hospitalInternal=1&showMultiDept=0&platformType=1000031&exConsult=&consultHosId=1099108&utm_source=0.0.h.1026.bus010.0";

            //StartTime = DateTime.Now.AddSeconds(20);
        }

        private void InitStaticData()
        {
            StartTime = new DateTime(2022, 11, 30, 19, 59, 58);

            Departments = new List<HospitalDept>();

            Departments.Add(new Jiankangzhilu("9000393", "包河区包公街道",
                "1039346", "包河区包公街道社区服务中心",
                "7229969", "九价Hpv")
            {
                DoctorSn = "711199332",
            });
            Departments.Add(new Jiankangzhilu("9000393", "包河区包公街道",
                "1039346", "包河区包公街道社区服务中心",
                "7230344", "带疱疹病毒")
            {
                DoctorSn = "711202108",
            });

            Departments.Add(new Jiankangzhilu("1000031", "江苏南京雨花台区",
                "1094417", "雨花社区卫生服务中心",
                "7225728", "九价Hpv")
            {
                HasYzm = false,
                DoctorSn = "711170881"
            });

            Departments.Add(new Jiankangzhilu("1000031", "江苏南京雨花台区",
                "1094417", "雨花社区卫生服务中心",
                "7225728", "四价Hpv")
            {
                HasYzm = false,
                DoctorSn = "711170863"
            });

            Departments.Add(new Jiankangzhilu("1000031", "江苏南京雨花台区",
                "1099108", "雨花经济开发区社区卫生服务中心",
                "7235344", "九价Hpv"));
            Departments.Add(new Jiankangzhilu("1000031", "江苏南京雨花台区",
                "1099108", "雨花经济开发区社区卫生服务中心",
                "7235355", "预防接种门诊（测试）"));

            Departments.Add(new Jiankangzhilu("1000031", "江苏南京雨花台区",
                "1092338", "板桥社区卫生服务中心",
                "7234217", "九价Hpv")
            {
                HasYzm = false,
                DoctorSn = "710795068"
            });
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
                BaoheSession.Cookie = Cookie;
                SearchController = HttpServiceController.GetService<SearchController>();
                ;
                SetSearchTimers();
                await SearchController.SearchAllAsync();
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
            BaoheSession.Cookie = Cookie;
            Task.Factory.StartNew(async () =>
            {
                await AutoRunAsync();
            });
        }

        private async Task AutoRunAsync()
        {
            SearchController = HttpServiceController.GetService<SearchController>();
            SetSearchTimers();
            await SearchController.AutoSearchAsync();
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
            BaoheSession.PlatformSesstion.AddOrUpdate(Constant.DoctorSn, selectedDept.DoctorSn);

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
