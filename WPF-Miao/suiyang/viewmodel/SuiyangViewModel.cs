using Base.model;
using Base.viewmodel.status;
using Base.viewModel;
using Base.viewModel.hospital;
using CommunityToolkit.Mvvm.Input;
using CoreControl.LogConsole;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
using suiyang.appointment;
using suiyang.login;
using suiyang.search;
using suiyang.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Utils;
using Utils.datetime;
using Utils.number;
using Utils.stringBuilder;

namespace suiyang.viewmodel
{
    internal class SuiyangViewModel : OnTimeViewModel
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

        private string _auth;
        public string Auth
        {
            get { return _auth; }
            set
            {
                _auth = value;
                MainSession.Auth = value;
                NotifyUI(() => Auth);
            }
        }

        private string _scheduleId;
        public string ScheduleId
        {
            get { return _scheduleId; }
            set
            {
                _scheduleId = value;
                NotifyUI(() => ScheduleId);
            }
        }

        private readonly object OrderLock = new object();

        private SearchController _searchController = new SearchController();

        #endregion Properties

        #region Constructor

        public SuiyangViewModel(LogPanel logPanel) : base(logPanel)
        {
            InitCommands();
            InitStaticData();

            TestData();
        }

        private void TestData()
        {
            Interval = 1000;
            StartTime = DateTime.Now.AddSeconds(20);
            MainSession.PlatformSession.AddOrUpdate("StartTime", StartTime);
            Auth = "Bearer 2805b851-3357-4c59-abbc-b51867149dcb";
        }

        private void InitStaticData()
        {
            StartTime = new DateTime(2022, 11, 21, 8, 29, 56);
            MainSession.PlatformSession.AddOrUpdate("StartTime", StartTime);

            DateList = new List<DspVal>
            {
                //new DspVal(DateTimeUtil.GetDayOfWeek(DayOfWeek.Monday)),
                //new DspVal(DateTimeUtil.GetDayOfWeek(DayOfWeek.Tuesday)),
                //new DspVal(DateTimeUtil.GetDayOfWeek(DayOfWeek.Wednesday)),
                //new DspVal(DateTimeUtil.GetDayOfWeek(DayOfWeek.Thursday)),
                new DspVal(DateTimeUtil.GetDayOfWeek(DayOfWeek.Friday)),
            };

            MainSession.PlatformSession.AddOrUpdate("DateList", DateList);

            Departments = new List<HospitalDept>
            {
                new SuiyangHospital
                {
                    HospitalId = "514966",
                    HospitalName = "绥阳县妇幼保健院",
                    DepartmentId = "F",
                    DepartmentName = "九价宫颈癌疫苗",
                },
                new SuiyangHospital
                {
                    HospitalId = "514966",
                    HospitalName = "绥阳县妇幼保健院",
                    DepartmentId = "G",
                    DepartmentName = "四价宫颈癌疫苗",
                },
                new SuiyangHospital
                {
                    HospitalId = "514966",
                    HospitalName = "绥阳县妇幼保健院",
                    DepartmentId = "W",
                    DepartmentName = "妇科病普查",
                },
            };

            SelectedDepartment = Departments.FirstOrDefault();
            MainSession.InitSession();
        }

        private void InitCommands()
        {
            SearchCommand = new RelayCommand(ExecuteManual);
            CancelCommand = new RelayCommand(ExecuteCancel);

            MainSession.AppointEvent.Subscribe(OnYuyue);
            MainSession.PrintLogEvent = PrintLogEvent;

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

        #region AutoRun

        protected override void StartAutoRun()
        {
            Task.Factory.StartNew(async () => {
                try
                {
                    MainSession.PlatformSession.AddOrUpdate("StartTime", StartTime);
                    await _searchController.GetUserAsync();
                    StartOnTimeTimer();
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
                    _searchController.GetMiaoAsync();
                    //Yuyue();
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

        private void OnYuyue(object? sender, AppointEventArgs e)
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
                foreach (var order in orderList)
                {
                    var yuyueController = MainSession.AppointSession.GetController(order.AppointDate);
                    yuyueController.StartInterval(order);
                }
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
                    if (StringUtil.AnyEmpty(Auth))
                    {
                        MainSession.PrintLogEvent.Publish(this, "请填写用户Auth -Bearer");
                        return;
                    }
                    MainSession.PrintLogEvent.Publish(this, $"手动预约");
                    MainSession.PlatformSession.AddOrUpdate("StartTime", StartTime);

                    await _searchController.GetUserAsync();

                    if (StringUtil.NotEmpty(SelectedDate?.Value))
                    {
                        DirectlyOrder(SelectedDate.Value);
                        return;
                    }

                    _searchController.GetMiaoAsync();
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

        private void Yuyue()
        {
            var controllerList = MainSession.AppointSession.ControllerCache;
            var userInfo = MainSession.PlatformSession["userInfo"] as Dictionary<string, object>;

            var dateList = DateList.DisorderItems();

            foreach (var pair in DateList)
            {
                var date = pair.Value;
                PublishYuyue(date, userInfo);
                Thread.Sleep(60);
            }
        }

        private void PublishYuyue(string date, Dictionary<string, object> userInfo)
        {
            var orderList = new List<Order>();

            var order = new Order
            {
                BtCode = MainSession.PlatformSession.GetString(Constants.DeptId),
                AppointDate = date,
                Barcode = MainSession.Auth.Replace("Bearer ", string.Empty),
                AddressId = userInfo.GetString("id").ToInt(),
                Identity = userInfo.GetString("identity"),
                Phone = userInfo.GetString("mobile"),
                IdName = userInfo.GetString("firstName"),
            };

            orderList.Add(order);

            var appointEventArgs = new AppointEventArgs
            {
                OrderList = orderList
            };
            Task.Factory.StartNew(() =>
            {
                MainSession.AppointEvent.Publish(null, appointEventArgs);
            });
        }

        private void DirectlyOrder(string date)
        {
            var controller = MainSession.AppointSession.GetController(date);
            var userInfo = MainSession.PlatformSession["userInfo"] as Dictionary<string, object>;
            PublishYuyue(date, userInfo);
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
            var selectedDept = SelectedDepartment as SuiyangHospital;
            MainSession.PlatformSession.AddOrUpdate(Constants.DeptId, selectedDept.DepartmentId);
            MainSession.PlatformSession.AddOrUpdate(Constants.HospitalId, selectedDept.HospitalId);

            Log(selectedDept.ToLogString());
        }

        #endregion Hospital Dept

        #region ReSession

        protected override void ReSession()
        {
            Log("ression invoke");
        }

        #endregion Resession
    }
}
