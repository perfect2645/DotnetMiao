using Base.viewModel;
using Base.viewModel.hospital;
using CommunityToolkit.Mvvm.Input;
using CoreControl.LogConsole;
using HttpProcessor.ExceptionManager;
using JkzlSearcher.search;
using JkzlSearcher.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Utils;

namespace JkzlSearcher.viewModel
{
    internal class JkzlViewModel : OnTimeViewModel
    {

        #region Properties

        public ICommand SearchCommand { get; set; }
        public ICommand ChekAuthCommand { get; set; }

        private SearchController _searchController;

        #endregion Properties

        public JkzlViewModel(LogPanel logPanel) : base(logPanel)
        {
            InitCommands();
            InitStaticData();

            TestData();
        }

        private void TestData()
        {
            StartTime = DateTime.Now.AddSeconds(10);
            Cookie = "YiHu_OpenId=eyJPcGVuSUQiOiJvZmY2dHMxTHFHaG1RaE5PY2dKb3d4NGtQUldNIiwiU2VjU3RyIjoiMzEwMDk3MjcwNTYxRDcwNTQxNDg3MkU4RDY0MUVDMDcifQ%3D%3D; logintype=62; loginprovinceid=0; logincityid=0; loginid=off6ts1LqGhmQhNOcgJowx4kPRWM; OpenID=off6ts1LqGhmQhNOcgJowx4kPRWM; BaseDoctorUid=0; BaseUserType=0; LoginChannel=1000031; YiHu_UserJosn=eyJBY2NvdW50U24iOiIxNDg1MjgxMjkiLCJDYXJkTnVtYmVyIjoiMjA3ODYzMTUzMyIsIkxvZ2luSWQiOiJvZmY2dHMxTHFHaG1RaE5PY2dKb3d4NGtQUldNIiwiVXNlck5hbWUiOiIiLCJTZWNTdHIiOiJDNTUzQjkyQzU4MDM1QUJDRDBBRUM4QzU5MUNDRDQyQiJ9; TOKEN_5D0161F2EB225D58BD7D4CE01260C0E2=46870C40778143138D8DB7F18F652E0A; _YyghSysTrackUUID=04181256212; jkzlAn_uuid=C7FAC527-D8E3-4D62-A29C-885A74DB792A; jkzlAn_refercode=; jkzlAn_sid=A8D4AED1-67E8-40DD-B320-2CC3B69D81B8; jkzlAn_channelid=1000031; jkzlAn_userid=148528129; jkzlAn_p=-1; jkzlAn_c=-1; jkzlAn_utm_source=0.0.h.1026.bus010.0; jkzlAn_ct=1670148785836";

            MainSession.Cookie = Cookie;
        }

        private void InitStaticData()
        {
            _searchController = new SearchController();
            Departments = new List<HospitalDept>();
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
                "7230344", "带疱疹病毒")
            {
                DoctorSn = "711202108",
                HasYzm = false
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

        #region Status Control

        protected override void OnInitAsync()
        {

        }

        protected override void OnReadyForSearchAsync()
        {
        }

        protected override void OnSearchingAsync()
        {

        }

        protected override void OnSearchendAsync()
        {

        }

        protected override void OnMiaoGetAsync(object data)
        {
            StopIntervalTimer();
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
                    StartIntervalTimer();
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
                    _searchController.SearchByHospitalId();
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
            Task.Factory.StartNew(() =>
            {
                _searchController.CheckAuthAsync();
            });
        }

        #endregion CheckAuth

        private void ExecuteSearch()
        {
            _searchController.SearchByHospitalId();
        }
    }
}
