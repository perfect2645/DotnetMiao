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
            Cookie = "jkzlAn_uuid=732AD1F0-E8A1-41D6-9712-B7974A4EA54A; jkzlAn_p=-1; jkzlAn_c=-1; YiHu_OpenId=eyJPcGVuSUQiOiJvUUtzQ3dwLVFVY1dGRG9qRExOYlRXZHpsWGpVIiwiU2VjU3RyIjoiNjE5MjhCQ0M0NDBGM0Q0NDAzRjlCMDJERUI5RDkzRkUifQ%3D%3D; logintype=62; loginprovinceid=0; logincityid=0; loginid=oQKsCwp-QUcWFDojDLNbTWdzlXjU; OpenID=oQKsCwp-QUcWFDojDLNbTWdzlXjU; BaseDoctorUid=0; BaseUserType=0; LoginChannel=9000415; TOKEN_50C8B420B41978860B9954B801EA41BD=384E83F5F89D40E4B599ED03D7191306; _YyghSysTrackUUID=11211109343; jkzlAn_channelid=9000415; jkzlAn_sid=B6289DBF-5480-47DD-B53E-E1B271F0B6C3; TOKEN_0307ED16EF64C5709E2BA5E5765A9AC4=1CD2DCA667964B5CB3B3703574C09E8B; jkzlAn_userid=155046217; YiHu_UserJosn=eyJBY2NvdW50U24iOiIxNTUwNDYyMTciLCJDYXJkTnVtYmVyIjoiMjA4NTEzNDAyMSIsIkxvZ2luSWQiOiJvUUtzQ3dwLVFVY1dGRG9qRExOYlRXZHpsWGpVIiwiVXNlck5hbWUiOiIiLCJTZWNTdHIiOiIwNzg0Q0ZGMjZDNkE1OTY4MUUxMUNCQkRGNTI3M0QxRSJ9; jkzlAn_utm_source=0.0.h.1026.bus010.0__0.0.h.1026.bus010.0; jkzlAn_ct=1678544605827";

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
