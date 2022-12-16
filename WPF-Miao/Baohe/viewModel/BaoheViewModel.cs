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
using System.Linq;
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
            BaoheSession.UpdateUiEvent = UpdateUiEvent;

            TestData();
        }

        private void TestData()
        {
            Cookie = "jkzlAn_uuid=88844286-2FA2-443E-B7C7-1F2C45BE03DE; YiHu_OpenId=eyJPcGVuSUQiOiJvZmY2dHMwZFZaRWRadGJqZGppT1FBaFpuUVIwIiwiU2VjU3RyIjoiNDNGNTY4NkVBNUVGQjQwQjUzRkE1OTlGNTZBQ0JFMjMifQ%3D%3D; logintype=62; loginid=off6ts0dVZEdZtbjdjiOQAhZnQR0; OpenID=off6ts0dVZEdZtbjdjiOQAhZnQR0; LoginChannel=1000031; TOKEN_64EE5E159F480EFBF3D478ED129BE688=413AC2CFAC884EA6A7AFC3EA5D764B1F; jkzlAn_sid=DF6D5676-BA19-4473-A99E-25AE58F1F568; jkzlAn_channelid=1000031; jkzlAn_userid=152468919; jkzlAn_p=-1; jkzlAn_c=-1; loginprovinceid=0; logincityid=0; BaseDoctorUid=0; BaseUserType=0; YiHu_UserJosn=eyJBY2NvdW50U24iOiIxNTI0Njg5MTkiLCJDYXJkTnVtYmVyIjoiMjA4MjU1ODQ4OCIsIkxvZ2luSWQiOiJvZmY2dHMwZFZaRWRadGJqZGppT1FBaFpuUVIwIiwiVXNlck5hbWUiOiIiLCJTZWNTdHIiOiI1NkZBMDM3QjgzMzAzNjM2Qzk4RTYzMzNCQkM5OTUzRCJ9; _YyghSysTrackUUID=16000707162; jkzlAn_utm_source=0.0.h.1026.bus010.0; jkzlAn_ct=1671120437882";

            SessionItem.Referer = "https://appoint.yihu.com/appoint/doctor/doctorArrange.html?deptId=7235364&doctorId=710796399&hospitalInternal=1&showMultiDept=0&platformType=1000031&exConsult=&consultHosId=1099108&utm_source=0.0.h.1026.bus010.0";
        }

        private void InitStaticData()
        {
            StartTime = new DateTime(2022, 12, 16, 7, 59, 59);

            Departments = new List<HospitalDept>();

            Departments.Add(new Jiankangzhilu("1000031", "江苏南京雨花台区",
                "1099108", "雨花经济开发区社区卫生服务中心",
                "7235364", "九价Hpv")
            {
                DoctorSn = "711230166"
            });

            Departments.Add(new Jiankangzhilu("1000031", "江苏南京雨花台区",
                "1099108", "雨花经济开发区社区卫生服务中心",
                "7235355", "预防接种门诊（测试）")
            {
                DoctorSn = "711230106",
                HasYzm = false
            });

            Departments.Add(new Jiankangzhilu("9000393", "包河区包公街道",
                "1039346", "包河区包公街道社区服务中心",
                "7229969", "九价Hpv")
            {
                DoctorSn = "711199332",
            });
            Departments.Add(new Jiankangzhilu("9000393", "包河区包公街道",
                "1039346", "包河区包公街道社区服务中心",
                "7230002", "四价Hpv")
            {
                DoctorSn = "711199340",
            });
            Departments.Add(new Jiankangzhilu("9000393", "包河区包公街道",
                "1039346", "包河区包公街道社区服务中心",
                "7230344", "带疱疹病毒")
            {
                DoctorSn = "711202108",
                HasYzm = false
            });

            Departments.Add(new Jiankangzhilu("9001026", "蜀山区井岗中心服务号",
                "1047063", "蜀山井岗镇社区卫生服务中心",
                "7229195", "九价Hpv")
                {
                    DoctorSn = "711188793"
                }
            );

            Departments.Add(new Jiankangzhilu("9001026", "蜀山区井岗中心服务号",
                "1047063", "蜀山井岗镇社区卫生服务中心",
                "7229244", "四价Hpv")
                {
                    DoctorSn = "711188785"
                }
            );

            Departments.Add(new Jiankangzhilu("9001026", "蜀山区井岗中心服务号",
                "1047063", "蜀山井岗镇社区卫生服务中心",
                "7209050", "(测试)儿童保健科")
            {
                DoctorSn = "71106853"
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


            SelectedDepartment = Departments.FirstOrDefault();
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

        #region Update UI

        protected override void UpdateUI(UiEventArgs e)
        {
            var field = e.Field;
            if ("yzm".Equals(field, StringComparison.OrdinalIgnoreCase))
            {
                VerifyCode.Yzm = e.Value.NotNullString();
                VerifyCode.ExecuteVerifyYzmAsync();
                return;
            }

            if ("phone".Equals(field, StringComparison.OrdinalIgnoreCase))
            {
                VerifyCode.Phone = e.Value.NotNullString();
                return;
            }
        }

        #endregion Update UI

    }
}
