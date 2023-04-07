using Base.viewmodel.status;
using Base.viewModel;
using Base.viewModel.hospital;
using CommunityToolkit.Mvvm.Input;
using CoreControl.LogConsole;
using HttpProcessor.ExceptionManager;
using JkzlSearcher.search;
using JkzlSearcher.session;
using Logging;
using Logging.logModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Utils;
using Utils.stringBuilder;
using Utils.timerUtil;

namespace JkzlSearcher.viewModel
{
    internal class JkzlViewModel : OnTimeViewModel
    {

        #region Properties

        public ICommand SearchCommand { get; set; }
        public ICommand ChekAuthCommand { get; set; }

        private SearchController _searchController;

        public JkzlGridViewModel GridViewModel { get; set; }

        private string _hospitalId;
        public string HospitalId
        {
            get { return _hospitalId; }
            set
            {
                _hospitalId = value;
                NotifyUI(() => HospitalId);
            }
        }

        private string _deptId;
        public string DeptId
        {
            get { return _deptId; }
            set
            {
                _deptId = value;
                NotifyUI(() => DeptId);
            }
        }

        private string _doctorSn;
        public string DoctorSn
        {
            get { return _doctorSn; }
            set
            {
                _doctorSn = value;
                NotifyUI(() => DoctorSn);
            }
        }

        #endregion Properties

        #region Constructor

        public JkzlViewModel(LogPanel logPanel) : base(logPanel)
        {
            InitCommands();
            InitStaticData();
            InitGridViewModel();
            TestData();
        }

        private void TestData()
        {
            StartTime = DateTime.Now.AddSeconds(10);
            Cookie = "jkzlAn_uuid=06063330-A203-4D12-8280-B2E000B2FD50; jkzlAn_c=-1; jkzlAn_p=-1; logintype=62; TOKEN_5D2D0944278AAB2A359ACF9E78F1288C=FB81BD209F034108BF7E5E2AC70402F3; TOKEN_02BE5C736779850386966D52BB1FF14D=DD511EEC945C43F0B96614B1BE6EFF57; TOKEN_7885A3C9DE3B2C52E47B7F4C97B3FD65=57836D643AE94DB1B346A413CF5A04AA; TOKEN_B1C9ADE7CC2C3DB6202662F4D340B2BF=D231FA0F5B0F4436A1215C28E925C47D; TOKEN_F8742A179EBD2919C32890913E429949=E94D941D8801486694CCA5EC029ACF54; TOKEN_C332E72E6DE11BDB95B80478C4822C85=69C18DD524DF430D9069F64601D4ECA8; TOKEN_22934A75D8B800A221370B7EB397A328=42349FED5A7D48E0B856EC4973C78BD3; TOKEN_51B8287A075B81DB20DA0CC0665893AD=022EC8AFA639448E846F30FF87CD903B; TOKEN_BA8CBA0FBFD1B4C3E8158D12EF7189AA=A416C20B0CD64A3AACDF1665827B34F4; TOKEN_BF82997485C7835E20A9048B3E4F8D5D=D0D8FD677486452CADA8AD7CAF07AA38; TOKEN_3DC1B35E22D6DA3B2F7E56C05DAE6FE2=977EAFFB07F14CDD9C8162E67509990E; TOKEN_97F31BCC98BB3E07FF5D29640281D01C=BE16AF39B4D349979434243E4B13BD89; TOKEN_9035FFDC0F1277D2BA6B034DA0D225AE=CCE7B25C781D47AEA70D9611AC740B5B; TOKEN_D16A4F185BE384D12843227AD417307E=ECFD5FE845824CA79437485F64B1B6A0; _YyghSysTrackUUID=07193706300; jkzlAn_sid=DDD1799C-40BB-4EEE-9906-DF3FD96965E4; YiHu_OpenId=eyJPcGVuSUQiOiJvdlRhNTVnQXRYNC1KbXpVdWktOHVPYkJCcjNrIiwiU2VjU3RyIjoiMDVFRTE4QjA1ODdBRDAxNUU3NDI5QUM5NDAxRjNEMkEifQ%3D%3D; loginprovinceid=0; logincityid=0; loginid=ovTa55gAtX4-JmzUui-8uObBBr3k; OpenID=ovTa55gAtX4-JmzUui-8uObBBr3k; BaseDoctorUid=0; BaseUserType=0; LoginChannel=9001567; TOKEN_4668C7AC83D7360A52313853CD93F149=5D8E0ACC7F54472F89988335B1F5EB06; jkzlAn_channelid=9001567; jkzlAn_utm_source=0.0.h.1026.bus010.0__0.0.h.1026.bus010.0; platformJson=%7bplatformType%3a9001567%2csourceType%3a0%2csourceId%3a0%2cclientId%3a9001567%7d; YiHu_UserJosn=eyJBY2NvdW50U24iOjE1NTkxMzYzNywiTG9naW5JZCI6Im92VGE1NWdBdFg0LUptelV1aS04dU9iQkJyM2siLCJPcmdVc2VyVHlwZSI6IiIsIk9yZ1NlY1N0ciI6IiIsIlNlY1N0ciI6IkZCREJFOUJDMDY4NEQzQUU5QTJFNzRDMDY5Q0EyNTM2IiwiVXNlck5hbWUiOiIiLCJDYXJkTnVtYmVyIjoiMjA4NTk5OTQ1NyIsIk9yZ0lkIjoiIiwiUCI6IlVDIn0%3d; jkzlAn_userid=155913637; jkzlAn_ct=1680868510466";

            MainSession.Cookie = Cookie;

            Interval = 500;
        }

        private void InitStaticData()
        {
            _searchController = new SearchController();
            Departments = new List<HospitalDept>();
            Departments.Add(new Jiankangzhilu("9001026", "蜀山区井岗中心服务号",
                "1047063", "蜀山井岗镇社区卫生服务中心",
                "7229195", "九价Hpv")
                        {
                            DoctorSn = "711188793"
                        }
            );
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

            SelectedDepartment = Departments.FirstOrDefault();
        }

        private void InitCommands()
        {
            SearchCommand = new RelayCommand(ExecuteSearch);
            ChekAuthCommand = new RelayCommand(ExecuteChekAuth);
            MainSession.PrintLogEvent = PrintLogEvent;
            SelectedDepartmentChanged = new Action(OnSelectedDepartmentChanged);
        }

        private void InitGridViewModel()
        {
            GridViewModel = new JkzlGridViewModel();
        }

        #endregion Constructor

        #region Status Control

        protected override void OnInitAsync()
        {

        }

        protected override void OnReadyForSearchAsync()
        {
            StartIntervalTimer();
        }

        protected override void OnSearchingAsync()
        {

        }

        protected override void OnSearchendAsync()
        {
            StopIntervalTimer();
            var resumeTImer = new ActionOnTime("resume search", 1000*60*3)
            {
                TargetAction = () =>
                {
                    MainSession.SetStatus(MiaoProgress.ReadyForSearch);
                }
            };
        }

        protected override void OnMiaoGetAsync(object data)
        {
        }

        protected override void ReSession()
        {
        }

        #endregion Status Control

        #region Hospital Dept

        private void OnSelectedDepartmentChanged()
        {
            var selectedDept = SelectedDepartment as Jiankangzhilu;
            MainSession.PlatformSession.AddOrUpdate(Constants.PlatformType, selectedDept.PlatformId);
            MainSession.PlatformSession.AddOrUpdate(Constants.HospitalId, selectedDept.HospitalId);
            MainSession.PlatformSession.AddOrUpdate(Constants.DeptId, selectedDept.DepartmentId);
            MainSession.PlatformSession.AddOrUpdate(Constants.Department, selectedDept);
            MainSession.PlatformSession.AddOrUpdate(Constants.DoctorSn, selectedDept.DoctorSn);

            Log(selectedDept.ToLogString());
        }

        #endregion Hospital Dept

        #region AutoRun

        protected override void StartAutoRun()
        {
            Task.Factory.StartNew(() => {
                try
                {
                    MainSession.SetStatus(MiaoProgress.ReadyForSearch);
                }
                catch (HttpException ex)
                {
                    Log(ex);
                }
                catch (Exception ex)
                {
                    Log(ex);
                }
            });
        }

        protected override void AutoRun()
        {
            Task.Factory.StartNew(() => {
                try
                {
                    _searchController.SearchByHospitalIdAsync();
                }
                catch (HttpException ex)
                {
                    Log(ex);
                }
                catch (Exception ex)
                {
                    Log(ex);
                }
            });
        }

        #endregion AutoRun

        #region CheckAuth

        private void ExecuteChekAuth()
        {
            if (StringUtil.AnyEmpty(HospitalId, DeptId, DoctorSn))
            {
                PrintLog("请填写HospitalId, DeptId, DoctorSn");
                return;
            }

            Task.Factory.StartNew(() =>
            {
                _searchController.CheckAuthAsync(HospitalId, DeptId, DoctorSn);
            });
        }

        #endregion CheckAuth

        private void ExecuteSearch()
        {
            _searchController.SearchByHospitalIdAsync();
        }
    }
}
