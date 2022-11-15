using Base.model;
using Base.viewmodel.status;
using Base.viewModel;
using Base.viewModel.hospital;
using chutian.appointment;
using chutian.login;
using chutian.search;
using chutian.session;
using CommunityToolkit.Mvvm.Input;
using CoreControl.LogConsole;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Utils;
using Utils.stringBuilder;

namespace chutian.viewmodel
{
    internal class ChutianViewModel : OnTimeViewModel
    {
        #region Properties

        public ICommand SearchCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand CancelCommand { get; set; }
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

        private string _userPhone;
        public string UserPhone
        {
            get { return _userPhone; }
            set
            {
                _userPhone = value;
                NotifyUI(() => UserPhone);
            }
        }

        private string _userPassword;
        public string UserPassword
        {
            get { return _userPassword; }
            set
            {
                _userPassword = value;
                NotifyUI(() => UserPassword);
            }
        }

        private readonly object OrderLock = new object();

        private SearchController _searchController = new SearchController();

        #endregion Properties

        #region Constructor

        public ChutianViewModel(LogPanel logPanel) : base(logPanel)
        {
            InitCommands();
            InitStaticData();
            MainSession.PrintLogEvent = PrintLogEvent;

            TestData();
        }

        private void TestData()
        {
            StartTime = DateTime.Now.AddSeconds(20);
            MainSession.PlatformSession.AddOrUpdate("StartTime", StartTime);
            UserPhone = "13940897525";
            _userPassword = "yinhen2645";
        }

        private void InitStaticData()
        {
            StartTime = new DateTime(2022, 11, 16, 7, 59, 56);
            MainSession.PlatformSession.AddOrUpdate("StartTime", StartTime);

            //DateList = new List<DspVal>
            //{
            //    new DspVal(DateTimeUtil.GetDayOfNextWeek(DayOfWeek.Monday)),
            //    new DspVal(DateTimeUtil.GetDayOfNextWeek(DayOfWeek.Tuesday)),
            //    new DspVal(DateTimeUtil.GetDayOfNextWeek(DayOfWeek.Wednesday)),
            //    new DspVal(DateTimeUtil.GetDayOfNextWeek(DayOfWeek.Thursday)),
            //    new DspVal(DateTimeUtil.GetDayOfNextWeek(DayOfWeek.Friday)),
            //};

            //MainSession.PlatformSession.AddOrUpdate("PreDateList", DateList);

            //TimeList = new List<DspVal>
            //{
            //    new DspVal("8:30-9:00", "DATE1_COUNT"),
            //    new DspVal("9:00-9:30", "DATE2_COUNT"),
            //    new DspVal("9:30-10:00", "DATE3_COUNT"),
            //    new DspVal("10:00-10:30", "DATE4_COUNT"),
            //    new DspVal("10:30-11:00", "DATE5_COUNT"),
            //};

            Departments = new List<HospitalDept>
            {
                new ChutianHospital
                {
                    HospitalId = "100012",
                    HospitalName = "武汉市汉阳区妇幼保健院",
                    DepartmentId = "646202",
                    DepartmentName = "九价HPV疫苗",
                },
                new ChutianHospital
                {
                    HospitalId = "100012",
                    HospitalName = "武汉市汉阳区妇幼保健院",
                    DepartmentId = "646191",
                    DepartmentName = "四价HPV疫苗",
                },
                //<option value="24">儿童乙肝疫苗（免费）</option>
                new ChutianHospital
                {
                    HospitalId = "100012",
                    HospitalName = "武汉市汉阳区妇幼保健院",
                    DepartmentId = "646441",
                    DepartmentName = "带状疱疹疫苗",
                },
            };

            SelectedDepartment = Departments.FirstOrDefault();
        }

        private void InitCommands()
        {
            LoginCommand = new AsyncRelayCommand(ExecuteLogin);
            SearchCommand = new RelayCommand(ExecuteManual);
            CancelCommand = new RelayCommand(ExecuteCancel);

            //MainSession.AppointEvent.Subscribe(OnAppointment);
            MainSession.ScheduleEvent.Subscribe(OnSchedule);

            SelectedDepartmentChanged = new Action(OnSelectedDepartmentChanged);
        }

        #endregion Constructor

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

        #endregion Status Control

        #region Login

        private async Task ExecuteLogin()
        {
            if (StringUtil.AnyEmpty(UserPhone, UserPassword))
            {
                MainSession.PrintLogEvent.Publish(this, "请填写用户手机和密码");
                return;
            }
            var loginController = HttpServiceController.GetService<LoginController>();
            var userId = await loginController.LoginAsync(UserPhone, UserPassword);
            var familyController = HttpServiceController.GetService<FamilyController>();
            await familyController.GetFamilyAsync(userId);
        }

        #endregion Login

        #region AutoRun

        protected override void StartAutoRun()
        {
            Task.Factory.StartNew(async () => {
                try
                {
                    await ExecuteLogin();
                    MainSession.PlatformSession.AddOrUpdate("StartTime", StartTime);
                    StartIntervalTimer();
                    //var searchController = new SearchController();
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
                    _searchController.SearchAsync();
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

        #region Appoint

        private void OnSchedule(object? sender, ScheduleEventArgs e)
        {
            lock (OrderLock)
            {
                var orderList = e.OrderList;
                Task.Factory.StartNew(() => OnAppointment(orderList));
            }
        }

        private void OnAppointment(List<Order> orderList)
        {
            try
            {
                var preOrderController = HttpServiceController.GetService<PreOrderController>();
                var preContent = new PreOrderContent();
                preOrderController.BuildHeaders(preContent);

                foreach (var order in orderList)
                {
                    var content = new PreOrderContent(order);
                    preOrderController.PreOrderAsync(content);
                    order.IntervalOnTime.StartIntervalOntime(() =>
                    {
                        Task.Factory.StartNew(() => preOrderController.PreOrderAsync(content));
                    });
                }
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        private void ExchangeOrdersAsync(List<Order> orderList)
        {
            try
            {
                //var appointController = HttpServiceController.GetService<AppointController>();
                //appointController.Exchange(orderList);
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        private void ExecuteManual()
        {
            Task.Factory.StartNew(async () => {
                try
                {
                    if (StringUtil.AnyEmpty(UserPhone, UserPassword))
                    {
                        MainSession.PrintLogEvent.Publish(this, "请填写用户手机和密码");
                        return;
                    }
                    MainSession.PrintLogEvent.Publish(this, $"手动预约");
                    MainSession.PlatformSession.AddOrUpdate("StartTime", StartTime);

                    await ExecuteLogin();

                    //if (StringUtil.NotEmpty(SelectedDate?.Value, SelectedTime?.Value))
                    //{
                    //    DirectlyOrder(SelectedDate.Value, SelectedTime.Value);
                    //    return;
                    //}
                    if (MainSession.GetStatus() != MiaoProgress.ReadyForSearch)
                    {
                        return;
                    }
                    _searchController.SearchAsync();
                    //var dateCountController = HttpServiceController.GetService<DateCountController>();
                    //Task.Factory.StartNew(() =>
                    //{
                    //    var result = dateCountController.SearchBydate(SelectedDate.Value);
                    //    MainSession.PrintLogEvent.Publish(this, $"{result}");
                    //});
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

        private void DirectlyOrder(string date, string time)
        {
            //var dateCountController = HttpServiceController.GetService<DateCountController>();
            //Task.Factory.StartNew(() =>
            //{
            //    var result = dateCountController.SearchBydateTime(date, time);
            //    MainSession.PrintLogEvent.Publish(this, $"{result}");
            //});
        }

        #endregion Appoint

        #region Cancel

        private async void ExecuteCancel()
        {
            try
            {
                //var appointController = HttpServiceController.GetService<CancelController>();
                //await appointController.CancelAsync();
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        #endregion Cancel

        #region Hospital Dept

        private void OnSelectedDepartmentChanged()
        {
            var selectedDept = SelectedDepartment as ChutianHospital;
            MainSession.PlatformSession.AddOrUpdate(Constants.HospitalId, selectedDept.HospitalId);
            MainSession.PlatformSession.AddOrUpdate(Constants.DoctorId, selectedDept.DepartmentId);

            Log(selectedDept.ToLogString());
        }

        #endregion Hospital Dept
    }
}
