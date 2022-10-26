using Base.Events;
using Base.model;
using Base.viewmodel.status;
using Base.viewModel;
using Base.viewModel.hospital;
using CommunityToolkit.Mvvm.Input;
using CoreControl.LogConsole;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
using renren.search;
using renren.session;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Utils;
using Utils.stringBuilder;

namespace renren.viewmodel
{
    internal class RenrenViewModel : OnTimeViewModel
    {
        #region Properties

        public ICommand SearchCommand { get; set; }
        public ICommand AppointCommand { get; set; }
        public ICommand YzmCommand { get; set; }

        private List<DspVal> _dateList;
        public List<DspVal> DateList
        {
            get { return _dateList; }
            set
            {
                _dateList = value;
                NotifyUI(() => DateList);
            }
        }

        private DspVal _selectedDate;
        public DspVal SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                //MainSession.MiaoSession.AddOrUpdate("Date", value.Value);
                NotifyUI(() => SelectedDate);
            }
        }

        private List<DspVal> _timeList;
        public List<DspVal> TimeList
        {
            get { return _timeList; }
            set
            {
                _timeList = value;
                NotifyUI(() => TimeList);
            }
        }

        private DspVal _selectedTime;
        public DspVal SelectedTime
        {
            get { return _selectedTime; }
            set
            {
                _selectedTime = value;
                //MainSession.MiaoSession.AddOrUpdate("Time", value.Value);
                NotifyUI(() => SelectedTime);
            }
        }

        private string _medicToken = "CK5I1F9BUCUUSTG1+BSOXA==";
        public string MedicToken
        {
            get { return _medicToken; }
            set
            {
                _medicToken = value;
                MainSession.PlatformSesstion.AddOrUpdate(Constants.MedicToken, value);
                NotifyUI(() => MedicToken);
                CheckInitStatus();
            }
        }

        private string _openId = "oYSgi1AC5pqly_Brb2aLM7mnpLUU";
        public string OpenId
        {
            get { return _openId; }
            set
            {
                _openId = value;
                MainSession.PlatformSesstion.AddOrUpdate(Constants.OpenId, value);
                NotifyUI(() => OpenId);
                CheckInitStatus();
            }
        }

        #endregion Properties

        #region Constructor

        public RenrenViewModel(LogPanel logPanel) : base(logPanel)
        {
            InitStaticData();
            InitCommands();

            TestData();
        }

        private void TestData()
        {
            MedicToken = "CK5I1F9BUCUUSTG1+BSOXA==";
            OpenId = "oYSgi1AC5pqly_Brb2aLM7mnpLUU";
        }

        private void InitStaticData()
        {
            //MainSession.MiaoSession.AddOrUpdate("StartTime", new DateTime(2022, 10, 7, 8, 57, 0));
            MainSession.PlatformSesstion.AddOrUpdate(Constants.AppId, "wx8320e743a5db7bff");
            Departments = new List<HospitalDept>
            {
                new RenrenHospital
                {
                    UserHospitalId = "2c92808a83597c0c0183c552cfb2585f",
                    HospitalId = "2c924b1061e108200161e5bae2c031e8",
                    HospitalName = "广州市黄浦区联和街社区卫生服务中心",
                    DepartmentId = "2c9280977a0d16c4017a13a0de5310bf",
                    DepartmentName = "HPV-富春卫生服务站",
                    ServiceId = "2c9280978201614d01821f73cc5c7da5",
                },
            };

            DateList = new List<DspVal>
            {
                new DspVal("2022-10-10"),
                new DspVal("2022-10-11"),
                new DspVal("2022-10-12"),
                new DspVal("2022-10-13"),
                new DspVal("2022-10-14"),
            };

            TimeList = new List<DspVal>
            {
                new DspVal("8:00-8:30", "DATE1_COUNT"),
                new DspVal("8:30-9:00", "DATE2_COUNT"),
                new DspVal("9:00-9:30", "DATE3_COUNT"),
                new DspVal("9:30-10:00", "DATE4_COUNT"),
                new DspVal("10:00-10:30", "DATE5_COUNT"),
                new DspVal("10:30-11:00", "DATE6_COUNT"),
            };
        }

        private void InitCommands()
        {
            MainSession.PrintLogEvent = PrintLogEvent;

            SearchCommand = new RelayCommand(ExecuteSearchAsync);
            AppointCommand = new RelayCommand(ExecuteAppointAsync);
            YzmCommand = new AsyncRelayCommand(ExecuteYzmAsync);
            SessionEvents.Instance.Subscribe(LogSession);

            SelectedDepartmentChanged = new Action(OnSelectedDepartmentChanged);

        }

        #endregion Constructor

        #region Status Control

        private void CheckInitStatus()
        {
            if (StringUtil.NotEmpty(MedicToken, OpenId))
            {
                MainSession.SetStatus(MiaoProgress.ReadyForSearch);
            }
            else
            {
                MainSession.SetStatus(MiaoProgress.Init);
            }
        }

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

        #endregion Status Control

        #region Search

        private async void ExecuteSearchAsync()
        {
            if (MainSession.MiaoStatus.MiaoProgress == MiaoProgress.Init)
            {
                PrintLogEvent.Publish(this, "请补全必须的信息");
                return;
            }

            try
            {
                MainSession.SetStatus(MiaoProgress.Searching);
                var searchController = HttpServiceController.GetService<SearchController>();
                await searchController.SearchAsync();
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

        #region Appoint

        private async void ExecuteAppointAsync()
        {
            try
            {
                MainSession.Cookie = Cookie;
                var searchController = HttpServiceController.GetService<SearchController>();
                await searchController.SearchAsync();
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

        #endregion Appoint

        #region AutoRun

        protected override void StartAutoRun()
        {
            ExecuteSearchAsync();
            StartAutoRunTimer();
        }

        protected override void AutoRun()
        {
            Task.Factory.StartNew(() =>
            {
                PrintLogEvent.Publish(this, "开始了");
            });
        }

        #endregion AutoRun

        #region 验证码

        private async Task ExecuteYzmAsync()
        {
            //var yzmController = HttpServiceController.GetService<YzmController>();
            //await yzmController.GetYzmAsync();
        }

        #endregion 验证码

        #region Hospital Dept

        private void OnSelectedDepartmentChanged()
        {
            var selectedDept = SelectedDepartment as RenrenHospital;
            MainSession.PlatformSesstion.AddOrUpdate(Constants.HospitalId, selectedDept.HospitalId);
            MainSession.PlatformSesstion.AddOrUpdate(Constants.UserHospitalId, selectedDept.UserHospitalId);
            MainSession.PlatformSesstion.AddOrUpdate(Constants.TeamId, selectedDept.DepartmentId);

            Log(selectedDept.ToLogString());

            //MainSession.BuildMiaoSession(MainSession.PlatformSesstion[Constant.DeptId].NotNullString());
        }

        #endregion Hospital Dept

        #region Session

        private void LogSession(object? sender, SesstionEventArgs args)
        {

        }

        #endregion Session
    }
}
