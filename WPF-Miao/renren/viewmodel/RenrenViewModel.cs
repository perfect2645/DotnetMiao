using Base.Events;
using Base.model;
using Base.viewmodel.status;
using Base.viewModel;
using Base.viewModel.hospital;
using CommunityToolkit.Mvvm.Input;
using CoreControl.LogConsole;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
using renren.appointment;
using renren.search;
using renren.search.miao;
using renren.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Utils;
using Utils.datetime;
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

        private string _medicToken;
        public string MedicToken
        {
            get { return _medicToken; }
            set
            {
                _medicToken = value;
                MainSession.PlatformSession.AddOrUpdate(Constants.MedicToken, value);
                NotifyUI(() => MedicToken);
                CheckInitStatus();
            }
        }

        private string _openId;
        public string OpenId
        {
            get { return _openId; }
            set
            {
                _openId = value;
                MainSession.PlatformSession.AddOrUpdate(Constants.OpenId, value);
                NotifyUI(() => OpenId);
                CheckInitStatus();
            }
        }

        private string _publicKey;
        public string PublicKey
        {
            get { return _publicKey; }
            set
            {
                _publicKey = value;
                MainSession.PlatformSession.AddOrUpdate(Constants.PublicKey, value);
                NotifyUI(() => PublicKey);
                CheckInitStatus();
            }
        }

        private readonly object OrderLock = new object();

        private ScheduleController _scheduleController;

        #endregion Properties

        #region Constructor

        public RenrenViewModel(LogPanel logPanel) : base(logPanel)
        {
            InitCommands();
            InitStaticData();
            InitController();

            TestData();
        }

        private void TestData()
        {
            MedicToken = "5CEHBHJ96OMDAHSA146NVQ==";
            OpenId = "oYSgi1AC5pqly_Brb2aLM7mnpLUU";
            PublicKey = "A51B253E-04A9-4E2E-8DA3-428BAF1E1F7B";
        }

        private void InitStaticData()
        {
            //MainSession.MiaoSession.AddOrUpdate("StartTime", new DateTime(2022, 10, 7, 8, 57, 0));
            MainSession.PlatformSession.AddOrUpdate(Constants.AppId, "wx8320e743a5db7bff");

            var dayToday = (int)DateTime.Today.DayOfWeek;
            if (dayToday < 6)
            {
                MainSession.PlatformSession.AddOrUpdate(Constants.ScheduleFrom, DateTimeUtil.GetDayOfWeek(DateTime.Today.DayOfWeek));
                MainSession.PlatformSession.AddOrUpdate(Constants.ScheduleTo, DateTimeUtil.GetDayOfWeek(DateTime.Today.DayOfWeek + 1));
            }
            else
            {
                MainSession.PlatformSession.AddOrUpdate(Constants.ScheduleFrom, DateTimeUtil.GetDayOfNextWeek(DayOfWeek.Sunday));
                MainSession.PlatformSession.AddOrUpdate(Constants.ScheduleTo, DateTimeUtil.GetDayOfNextWeek(DayOfWeek.Saturday));
            }

            Departments = new List<HospitalDept>
            {
                new RenrenHospital
                {
                    UserHospitalId = "2c92808a83597c0c0183c552cfb2585f",
                    HospitalId = "2c924b1061e108200161e5bae2c031e8",
                    HospitalName = "广州市黄浦区联和街社区卫生服务中心",
                    DepartmentId = "2c9280977a0d16c4017a13a0de5310bf",
                    DepartmentName = "儿童入托体检",
                    ServiceId = "2c9280977a66dc42017a7b17b1f52b5b",
                },
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

            SelectedDepartment = Departments.FirstOrDefault();

        }

        private void InitController()
        {
            _scheduleController = HttpServiceController.GetService<ScheduleController>();
        }

        private void InitCommands()
        {
            MainSession.PrintLogEvent = PrintLogEvent;

            SearchCommand = new RelayCommand(OnSearchClick);
            AppointCommand = new RelayCommand(ExecuteAppointAsync);
            YzmCommand = new AsyncRelayCommand(ExecuteYzmAsync);
            SessionEvents.Instance.Subscribe(LogSession);

            SelectedDepartmentChanged = new Action(OnSelectedDepartmentChanged);

            MainSession.AppointEvent.Subscribe(OnAppointment);
        }

        #endregion Constructor

        #region Status Control

        private void CheckInitStatus()
        {
            if (StringUtil.NotEmpty(MedicToken, OpenId, PublicKey))
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

        protected override void OnMiaoGetAsync(object data)
        {
            StopAutoRunTimer();
        }

        #endregion Status Control

        #region Search

        private void OnSearchClick()
        {
            Task.Factory.StartNew(async () =>
            {
                await SearchAsync();
                if (MainSession.MiaoStatus.MiaoProgress < MiaoProgress.GettingMiao)
                {
                    MainSession.SetStatus(MiaoProgress.GettingMiao);
                }
                var miaoSchedule = HttpServiceController.GetService<ScheduleController>();
                await miaoSchedule.GetServiceScheduleAsync();
            });
        }

        private async Task SearchAsync()
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

        private void OnAppointment(object? sender, AppointEventArgs e)
        {
            lock (OrderLock)
            {
                var orderList = e.OrderList;
                ConsumeOrdersAsync(orderList);
            }
        }

        private void ConsumeOrdersAsync(List<Order> orderList)
        {
            try
            {
                Task.Factory.StartNew(() =>
                {
                    Yuyue(orderList);
                });
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        internal void Yuyue(List<Order> orderList)
        {
            MainSession.PrintLogEvent.Publish(this, $"开始预约：");
            if (MainSession.MiaoStatus.MiaoProgress == MiaoProgress.AppointEnd)
            {
                MainSession.PrintLogEvent.Publish(this, $"预约结束，退出循环");
                return;
            }
            foreach (var order in orderList)
            {
                if (MainSession.MiaoStatus.MiaoProgress == MiaoProgress.AppointEnd)
                {
                    order.IntervalOnTime.StopInterval();
                    MainSession.PrintLogEvent.Publish(this, $"预约结束，退出循环");
                    return;
                }
                MainSession.PrintLogEvent.Publish(this, order.ToLogString());
                var content = new AppointContent(order);

                var appointController = HttpServiceController.GetService<AppointController>();
                Task.Factory.StartNew(() => appointController.AppointAsync(content));
                order.IntervalOnTime.StartIntervalOntime(() =>
                {
                    Task.Factory.StartNew(() => appointController.AppointAsync(content));
                });
            }
        }

        private async void ExecuteAppointAsync()
        {
            try
            {
                //MainSession.Cookie = Cookie;
                //var appointController = HttpServiceController.GetService<AppointController>();
                //await appointController.AppointAsync();
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

        protected override async void StartAutoRun()
        {
            await SearchAsync();
            StartAutoRunTimer();
        }

        protected override void AutoRun()
        {
            Task.Factory.StartNew(async () =>
            {
                PrintLogEvent.Publish(this, "开始了");
                if (MainSession.MiaoStatus.MiaoProgress != MiaoProgress.GettingMiao)
                {
                    MainSession.SetStatus(MiaoProgress.GettingMiao);
                }

                await _scheduleController.GetServiceScheduleAsync();
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
            MainSession.PlatformSession.AddOrUpdate(Constants.HospitalId, selectedDept.HospitalId);
            MainSession.PlatformSession.AddOrUpdate(Constants.UserHospitalId, selectedDept.UserHospitalId);
            MainSession.PlatformSession.AddOrUpdate(Constants.TeamId, selectedDept.DepartmentId);
            MainSession.PlatformSession.AddOrUpdate(Constants.ServiceId, selectedDept.ServiceId);

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
