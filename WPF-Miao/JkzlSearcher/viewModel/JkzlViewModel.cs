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
            Cookie = "jkzlAn_uuid=06063330-A203-4D12-8280-B2E000B2FD50; jkzlAn_p=-1; jkzlAn_c=-1; TOKEN_C3FF08123D78ED0CA7FA8F8ECE5DAF06=0C6612E13027410F88CE323C45A2EA3C; TOKEN_7EE496F7BAFB3649538009DFF1BAC410=6AE5B2C824354B57ABCD11F79EAE6F40; TOKEN_A882380A836B23C6BB9C9287B0F6A99B=13FFE180EE0D42A5BB5B834DDDF0CCC9; TOKEN_F7017963A760856262A83086081B7A7F=53F6A393CA8F408EAE6BF10FA8037CEA; TOKEN_35F7B217C8064209073A443A114368EE=7BE1EBB5983247F69D1E2A17ED4306EF; TOKEN_2FA74D100E25D8A8C928D1F1C45CCDD5=97221723B9E04E8CA5BE666A2C05EF72; logintype=62; loginprovinceid=0; logincityid=0; BaseDoctorUid=0; BaseUserType=0; TOKEN_C7468BDAF6775C16167024C13B05DA1E=46F94EC04B494790A11163DE5B276B49; TOKEN_814953DC967D0DD9ABCD951393D78A8C=3998D464A05D441FA6D3334FBBB7B8E0; LoginChannel=9001616; TOKEN_D2600781F19BD197C631DA3C3C0D793D=0297E96B203543EF941C9486C0BB6CA4; platformJson=%7bplatformType%3a9001616%2csourceType%3a0%2csourceId%3a0%2cclientId%3a9001616%7d; jkzlAn_channelid=9001616; YiHu_OpenId=eyJPcGVuSUQiOiJvc3VOVjZudERnOW1UdEE1dE14LTA3WUQ1WGNNIiwiU2VjU3RyIjoiRjVGRjY1QzJERTZCNkYzOEVBRDE1RUM2QjM0NTczNTAifQ%3D%3D; loginid=osuNV6ntDg9mTtA5tMx-07YD5XcM; OpenID=osuNV6ntDg9mTtA5tMx-07YD5XcM; TOKEN_8FD17FD425DAC79560E732D409467489=1D501962A6AE47979815516EF4286C1D; jkzlAn_sid=EE2EADBC-5B70-4E67-A501-37CEDFC15624; jkzlAn_userid=157847312; YiHu_UserJosn=eyJBY2NvdW50U24iOjE1Nzg0NzMxMiwiTG9naW5JZCI6Im9zdU5WNm50RGc5bVR0QTV0TXgtMDdZRDVYY00iLCJPcmdVc2VyVHlwZSI6IiIsIk9yZ1NlY1N0ciI6IiIsIlNlY1N0ciI6IkM0NDM5MzU5N0NDREFDRTZGQTExMTFBMzE1RDA1MTAyIiwiVXNlck5hbWUiOiIiLCJDYXJkTnVtYmVyIjoiMjA4NzkyOTQ1NyIsIk9yZ0lkIjoiIiwiUCI6IlVDIn0%3d; jkzlAn_ct=1685689649407";

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
