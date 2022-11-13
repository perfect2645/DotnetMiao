using Base.model;
using Base.viewModel;
using Base.viewModel.hospital;
using chutian.appointment;
using chutian.appointment.Yuyue;
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
            //Cookie = "JSESSIONID=A228544CF6804542F272475115539CD0";
            //Etid = "7bf4400434ea4e80a6dfb331f6f6a077";
            Cookie = "JSESSIONID=4E4D3E323CDD43315499CD463460240B";

            StartTime = DateTime.Now.AddSeconds(20);
            MainSession.PlatformSession.AddOrUpdate("StartTime", StartTime);
        }

        private void InitStaticData()
        {
            StartTime = new DateTime(2022, 11, 11, 7, 59, 56);
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
                    HospitalId = "whsjjkfqzyjxmsqwsfwzx",
                    HospitalName = "新民社区服务站",
                    DepartmentId = "18",
                    DepartmentName = "九价宫颈癌疫苗（进口）",
                },
                new ChutianHospital
                {
                    HospitalId = "whsjjkfqzyjxmsqwsfwzx",
                    HospitalName = "新民社区服务站",
                    DepartmentId = "17",
                    DepartmentName = "四价宫颈癌疫苗（进口）",
                },
                //<option value="24">儿童乙肝疫苗（免费）</option>
                new ChutianHospital
                {
                    HospitalId = "whsjjkfqzyjxmsqwsfwzx",
                    HospitalName = "新民社区服务站",
                    DepartmentId = "24",
                    DepartmentName = "儿童乙肝疫苗（免费）",
                },
            };

            SelectedDepartment = Departments.FirstOrDefault();
        }

        private void InitCommands()
        {
            LoginCommand = new RelayCommand(ExecuteLogin);
            SearchCommand = new RelayCommand(ExecuteManual);
            CancelCommand = new RelayCommand(ExecuteCancel);

            MainSession.AppointEvent.Subscribe(OnAppointment);

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
        }

        #endregion Status Control

        #region Login

        private void ExecuteLogin()
        {
            var loginController = HttpServiceController.GetService<LoginController>();
            loginController.LoginAsync(UserPhone, UserPassword);
        }

        #endregion Login

        #region AutoRun

        protected override void StartAutoRun()
        {
            Task.Factory.StartNew(() => {
                try
                {
                    MainSession.Cookie = Cookie;
                    MainSession.PlatformSession.AddOrUpdate("StartTime", StartTime);
                    var searchController = new SearchController();
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
        }

        #endregion AutoRun

        #region Appoint

        private void OnAppointment(object? sender, AppointEventArgs e)
        {
            lock (OrderLock)
            {
                var orderList = e.OrderList;
                var orderType = e.OrderType;
                if ("Exchange" == orderType)
                {
                    ExchangeOrdersAsync(orderList);
                    return;
                }
                ConsumeOrdersAsync(orderList);
            }
        }

        private void ConsumeOrdersAsync(List<Order> orderList)
        {
            try
            {
                var appointController = HttpServiceController.GetService<YuyueController>();
                //appointController.Yuyue(orderList);
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
            Task.Factory.StartNew(() => {
                try
                {
                    MainSession.PrintLogEvent.Publish(this, $"手动预约{SelectedDate.Display}");
                    MainSession.Cookie = Cookie;
                    MainSession.PlatformSession.AddOrUpdate("StartTime", StartTime);

                    if (StringUtil.NotEmpty(SelectedDate?.Value, SelectedTime?.Value))
                    {

                        DirectlyOrder(SelectedDate.Value, SelectedTime.Value);
                        return;
                    }

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
            MainSession.PlatformSession.AddOrUpdate(Constants.DeptId, selectedDept.DepartmentId);

            Log(selectedDept.ToLogString());
        }

        #endregion Hospital Dept
    }
}
