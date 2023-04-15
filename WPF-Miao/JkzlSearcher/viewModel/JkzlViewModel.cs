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
            Cookie = "jkzlAn_uuid=732AD1F0-E8A1-41D6-9712-B7974A4EA54A; jkzlAn_c=-1; jkzlAn_p=-1; logintype=62; loginprovinceid=0; logincityid=0; BaseDoctorUid=0; BaseUserType=0; LoginChannel=9001478; TOKEN_7FD391042985CCDBC9DE6776DD11CCC7=5884ADE6661346608BDC4947F2EA8A3C; platformJson=%7bplatformType%3a9001478%2csourceType%3a0%2csourceId%3a0%2cclientId%3a9001478%7d; jkzlAn_channelid=9001478; TOKEN_423D864CC118C50A7A4CAC4EFB94B6B8=11E5DB554B6946F693FD5AE018E48829; TOKEN_4BF9219DE781A34A7C8B2B70D51F0C5C=C31354A50E824EC58A7FAF90C8BDB74B; TOKEN_D36CD6281F5A2CFB423A30A1573464E1=2D385C0D298D46B0A731A518C1ED8013; TOKEN_32FAC51FA22C84ADE5F8397CF80B276E=13105FC605AE4F2585A2B804B31131BE; TOKEN_6B0E07E26AA943C5D44FA75836671C07=25DBCF9E0B08480A85E360224F4E918B; TOKEN_B67674D6E54FBD994C47704067EE1E40=BB9B0BFFF11A4F01B8FDAD6968F5C3C5; TOKEN_1ED887451CB73A8327EA7C7408172C06=4BDBE25515BE4FF2A172CEA7FE6CB8CA; TOKEN_BB7E079AD2A1A50AC39F154EAD6392E3=CEE68BBC7FD94F86A8E7FB9DCA4774C2; jkzlAn_sid=0B48243C-32C8-4752-BD2A-B90E88391AAC; TOKEN_275CDE8ED046C82E38292A95A46D8B38=7AB90024B0B341B888EBD7600660E301; YiHu_OpenId=eyJPcGVuSUQiOiJvWTVUcDV3N2w0bkxzMnMwb0piOXZIeXAxbHZnIiwiU2VjU3RyIjoiMzVEODY4Njc0QjNBMDUwNDgwQzI4OUY0MkM4NEFGRDUifQ%3D%3D; loginid=oY5Tp5w7l4nLs2s0oJb9vHyp1lvg; OpenID=oY5Tp5w7l4nLs2s0oJb9vHyp1lvg; YiHu_UserJosn=eyJBY2NvdW50U24iOiIxNTYxMzk5NzAiLCJDYXJkTnVtYmVyIjoiMjA4NjIyNTM3MCIsIkxvZ2luSWQiOiJvWTVUcDV3N2w0bkxzMnMwb0piOXZIeXAxbHZnIiwiVXNlck5hbWUiOiIiLCJTZWNTdHIiOiJDMEMwMzhGRDFBMDhGRThBRjFCM0Y5NDNGNDAyOTc5RSJ9; TOKEN_51DC7045E6F8EE571754DA3801A95262=26BDA07C2F9F4446AEDE2735DEC06B43; jkzlAn_userid=156139970; jkzlAn_ct=1681566295958";

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
