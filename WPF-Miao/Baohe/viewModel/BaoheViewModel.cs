using Baohe.appointment;
using Baohe.constants;
using Baohe.login;
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
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Utils;
using Utils.datetime;
using Utils.file;
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
                MainSession.PlatformSesstion.AddOrUpdate(Constant.RetId, value);
                NotifyUI(() => RetId);
            }
        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                NotifyUI(() => UserName);
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
            MainSession.PrintLogEvent = PrintLogEvent;
            MainSession.UpdateUiEvent = UpdateUiEvent;

            TestData();
            LoginFromConfig();
            MainSession.PrintLogEvent.Publish(this, StartTime.ToString());
        }

        private void TestData()
        {
            SessionItem.Referer = "https://appoint.yihu.com/appoint/doctor/doctorArrange.html?deptId=7235364&doctorId=710796399&hospitalInternal=1&showMultiDept=0&platformType=1000031&exConsult=&consultHosId=1099108&utm_source=0.0.h.1026.bus010.0";
        }

        private void InitStaticData()
        {
            StartTime = DateTime.Today.AddHours(8).AddSeconds(3);


            if (Application.Current.Properties.Contains("UserName"))
            {
                MainSession.User.UserName = Application.Current.Properties["UserName"].ToString();
                UserName = Application.Current.Properties["UserName"].ToString();
            }
            if (Application.Current.Properties.Contains("Cookie"))
            {
                MainSession.User.Cookie = Application.Current.Properties["Cookie"].ToString();
                Cookie = Application.Current.Properties["Cookie"].ToString();
            }

            Departments = new List<HospitalDept>();

            Departments.Add(new Jiankangzhilu("1000031", "江苏南京雨花台区",
                "1099108", "雨花经济开发区社区卫生服务中心",
                "7235364", "九价Hpv")
            {
                DoctorSn = "711230166"
            });

            Departments.Add(new Jiankangzhilu("1000031", "江苏南京雨花台区",
                "1094218", "西善桥",
                "7243224", "九价Hpv")
            {
                DoctorSn = "711269136"
            });

            Departments.Add(new Jiankangzhilu("1000031", "漳州市龙文区步",
                "1101211", "漳州市龙文区步文街道社区卫生服务中心",
                "7242593", "九价Hpv")
            {
                HasYzm = false,
                DoctorSn = "711267962"
            });

            Departments.Add(new Jiankangzhilu("1000031", "漳州市龙文区步",
                "1101211", "漳州市龙文区步文街道社区卫生服务中心",
                "7242593", "四价Hpv")
            {
                HasYzm = false,
                DoctorSn = "711267961"
            });

            Departments.Add(new Jiankangzhilu("1000031", "江苏南京雨花台区",
                "1101211", "漳州市龙文区步文街道社区卫生服务中心",
                "7242593", "九价Hpv")
            {
                HasYzm = false,
                DoctorSn = "148528129"
            });

            Departments.Add(new Jiankangzhilu("1000031", "江苏南京雨花台区",
                "1094417", "雨花社区卫生服务中心",
                "7225728", "九价Hpv")
            {
                HasYzm = false,
                DoctorSn = "711170881"
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

            Departments.Add(new Jiankangzhilu("9001026", "蜀山区井岗中心服务号",
                "1047063", "蜀山井岗镇社区卫生服务中心",
                "7229195", "九价Hpv")
            {
                DoctorSn = "711188793"
            });

            Departments.Add(new Jiankangzhilu("9001026", "蜀山区井岗中心服务号",
                "1047063", "蜀山井岗镇社区卫生服务中心",
                "7229244", "四价Hpv")
            {
                DoctorSn = "711188785"
            });

            Departments.Add(new Jiankangzhilu("9001026", "蜀山区井岗中心服务号",
                "1047063", "蜀山井岗镇社区卫生服务中心",
                "7209111", "计免科")
            {
                HasYzm = false,
                DoctorSn = "711068630"
            });

            Departments.Add(new Jiankangzhilu("1000031", "江苏南京雨花台区",
                "1099108", "雨花经济开发区社区卫生服务中心",
                "7235355", "预防接种门诊（测试）")
            {
                DoctorSn = "711230106",
                HasYzm = false
            });

            Departments.Add(new Jiankangzhilu("9001026", "蜀山区井岗中心服务号",
                "1047063", "蜀山井岗镇社区卫生服务中心",
                "7209050", "(测试)儿童保健科")
            {
                DoctorSn = "71106853"
            });

            Departments.Add(new Jiankangzhilu("1000031", "江苏南京雨花台区",
                "1094417", "雨花社区卫生服务中心",
                "7208870", "预防接种预约")
            {
                HasYzm = false,
                DoctorSn = "711102799" //儿童9价宫颈癌疫苗
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
            Departments.Add(new Jiankangzhilu("9000415", "包河区常青街道社区卫生服务中心",
                "1047032", "常青街道薛河湾社区卫生服务站",
                "7237655", "九价HPV疫苗")
            {
                HasYzm = true,
                DoctorSn = "711238182"
            });
            Departments.Add(new Jiankangzhilu("9000415", "包河区常青街道社区卫生服务中心",
                "1047032", "常青街道薛河湾社区卫生服务站",
                "7237655", "四价HPV疫苗")
            {
                HasYzm = false,
                DoctorSn = "711247350"
            });

            Departments.Add(new Jiankangzhilu("9000371", "合肥蜀山五里墩",
                "1033881", "蜀山区五里墩街道社区卫生服务中心",
                "7229497", "九价HPV疫苗")
            {
                HasYzm = true,
                DoctorSn = "711190104"
            });
            Departments.Add(new Jiankangzhilu("9000371", "合肥蜀山五里墩",
                "1033881", "蜀山区五里墩街道社区卫生服务中心",
                "7229497", "四价HPV疫苗")
            {
                HasYzm = true,
                DoctorSn = "711190063"
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




            SelectedDepartment = Departments.FirstOrDefault();
            InitPlatformSession();
        }

        private void InitPlatformSession()
        {
            try
            {
                var tsStr = DateTimeUtil.GetTimeStamp();
                var sessionTime = tsStr.Substring(0, 10);
                MainSession.PlatformSesstion.Add(Constant.SessionTime, sessionTime);
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

        #region Login

        private void LoginFromConfig()
        {
            if (!string.IsNullOrWhiteSpace(MainSession.User.Cookie))
            {
                return;
            }
            var users = FileReader.DeserializeFile<List<JkzlLogin>>("Login.json");
            foreach (var user in users)
            {
                StartApp(user);
            }
        }

        private void StartApp(JkzlLogin user)
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    var processInfo = new ProcessStartInfo();
                    processInfo.FileName = "baohe.exe";
                    processInfo.ArgumentList.Add(user.UserName);
                    processInfo.ArgumentList.Add(user.Cookie);

                    var p = Process.Start(processInfo);
                }
                catch (Exception ex)
                {
                    Log(ex);
                }
            });

        }

        #endregion Login

        #region Appointment

        private bool CanExecuteAppointment()
        {
            return true;
        }

        private void ExecuteAppointment()
        {

            var appRouter = new AppointmentRouter(SessionItem, UserName);

            appRouter.AppointTickAsync(UserName);
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
                MainSession.Cookie = Cookie;
                SearchController = HttpServiceController.GetService<SearchController>();
                ;
                SetSearchTimers();
                await SearchController.SearchAllAsync(UserName);
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
            MainSession.Cookie = Cookie;
            Task.Factory.StartNew(async () =>
            {
                await AutoRunAsync();
            });
        }

        private async Task AutoRunAsync()
        {
            SearchController = HttpServiceController.GetService<SearchController>();
            SetSearchTimers();
            await SearchController.AutoSearchAsync(UserName);
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
            MainSession.PlatformSesstion.AddOrUpdate(Constant.PlatformType, selectedDept.PlatformId);
            MainSession.PlatformSesstion.AddOrUpdate(Constant.HospitalId, selectedDept.HospitalId);
            MainSession.PlatformSesstion.AddOrUpdate(Constant.DeptId, selectedDept.DepartmentId);
            MainSession.PlatformSesstion.AddOrUpdate(Constant.Department, selectedDept);
            MainSession.PlatformSesstion.AddOrUpdate(Constant.DoctorSn, selectedDept.DoctorSn);

            Log(selectedDept.ToLogString());

            MainSession.BuildMiaoSession(MainSession.PlatformSesstion[Constant.DeptId].NotNullString());
        }

        #endregion Hospital Dept

        #region Start Time

        private void OnStartTimeChanged(DateTime? selectedTime)
        {
            MainSession.PlatformSesstion.AddOrUpdate(Constant.StartTime, selectedTime!);
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
