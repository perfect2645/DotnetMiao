using Base.model;
using Base.viewmodel.status;
using Base.viewModel;
using Base.viewModel.hospital;
using CommunityToolkit.Mvvm.Input;
using CoreControl.LogConsole;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
using hys020.appointment;
using hys020.appointment.Yuyue;
using hys020.search;
using hys020.session;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Utils;
using Utils.datetime;
using Utils.stringBuilder;

namespace hys020.viewmodel
{
    internal class HysViewModel : OnTimeViewModel
    {
        #region Properties

        public ICommand SearchCommand { get; set; }
        public ICommand AppointCommand { get; set; }
        public ICommand YzmCommand { get; set; }
        public ICommand StartAppCommand { get; set; }

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

        private string _location;
        public string Location
        {
            get { return _location; }
            set
            {
                _location = value;
                SaveLocation(value);
                NotifyUI(() => Location);
            }
        }

        private int _processCount = 5;
        public int ProcessCount
        {
            get { return _processCount; }
            set
            {
                _processCount = value;
                NotifyUI(() => ProcessCount);
            }
        }

        private readonly object OrderLock = new object();

        private SearchController _searchController;

        #endregion Properties

        #region Constructor

        public HysViewModel(LogPanel logPanel) : base(logPanel)
        {
            InitCommands();
            InitStaticData();
            InitControllers();

            TestData();
        }

        private void TestData()
        {
            Cookie = "JSESSIONID=1C45E2B16593B435963276DBF65CE99E";
            Location = "http://www.hys020.com/home/yyghDorMobile?userId=E8A53E4EFFDE46F7B8B15A44230C2524&wechatid=gh_868741944de3&openid=o1_Liw34_q5dnOFrOCRDK7jQn5E0&Timestamp=1tclc4SgvKiCId8FY8vdHd1pqq7GzFeM";

            //StartTime = DateTime.Now.AddSeconds(20);
        }

        private void InitControllers()
        {
            _searchController = HttpServiceController.GetService<SearchController>();
        }

        private void InitStaticData()
        {
            StartTime = new DateTime(2022, 12, 19, 10, 59, 50);

            if (Application.Current.Properties.Contains("Cookie"))
            {
                Cookie = Application.Current.Properties["Cookie"].ToString();
            }
            if (Application.Current.Properties.Contains("Cookie"))
            {
                Location = Application.Current.Properties["Location"].ToString();
            }

            //盆底修复 deptid 42CB58972CD44CD9945775814C00CA41
            Departments = new List<HospitalDept>
            {
                new HysHospital
                {
                    HospitalId = "doctorYyghMobileDate",
                    HospitalName = "肇庆市鼎湖区",
                    DepartmentId = "E8A53E4EFFDE46F7B8B15A44230C2524",
                    DepartmentName = "九价宫颈癌疫苗（进口）",
                },
                //<option value="24">儿童乙肝疫苗（免费）</option>
                new HysHospital
                {
                    HospitalId = "doctorYyghMobileDate",
                    HospitalName = "肇庆市鼎湖区",
                    DepartmentId = "42CB58972CD44CD9945775814C00CA41",
                    DepartmentName = "盆底修复",
                },
                new HysHospital
                {
                    HospitalId = "doctorYyghMobileDate",
                    HospitalName = "肇庆市鼎湖区",
                    DepartmentId = "6334C2A529E1423881ADDA4E3A737076",
                    DepartmentName = "黄针妹",
                },
            };

            DateList = new List<DspVal>
            {
                //new DspVal(DateTimeUtil.GetDayOfWeek(DayOfWeek.Monday)),
                //new DspVal(DateTimeUtil.GetDayOfWeek(DayOfWeek.Tuesday)),
                new DspVal(DateTimeUtil.GetDayOfWeek(DayOfWeek.Wednesday)),
                new DspVal(DateTimeUtil.GetDayOfWeek(DayOfWeek.Thursday)),
                new DspVal(DateTimeUtil.GetDayOfWeek(DayOfWeek.Friday)),
                //new DspVal(DateTimeUtil.GetDayOfNextWeek(DayOfWeek.Monday)),
                //new DspVal(DateTimeUtil.GetDayOfNextWeek(DayOfWeek.Tuesday)),
                //new DspVal(DateTimeUtil.GetDayOfNextWeek(DayOfWeek.Wednesday)),
                //new DspVal(DateTimeUtil.GetDayOfNextWeek(DayOfWeek.Thursday)),
                //new DspVal(DateTimeUtil.GetDayOfNextWeek(DayOfWeek.Friday)),
            };

            MainSession.PlatformSession.AddOrUpdate("DateList", DateList);

            TimeList = new List<DspVal>
            {
                new DspVal("08:30 - 09:00"),
                new DspVal("09:00 - 09:30"),
                new DspVal("09:30 - 10:00"),
                new DspVal("10:00 - 10:30"),
                new DspVal("10:30 - 11:00"),
                new DspVal("14:30 - 15:00"),
                new DspVal("15:00 - 15:30"),
                new DspVal("15:30 - 16:00"),
                new DspVal("16:00 - 16:30"),
                new DspVal("16:30 - 17:00"),
            };

            MainSession.PlatformSession.AddOrUpdate("TimeList", TimeList);

            SelectedDepartment = Departments.FirstOrDefault();

            AppointSession.Init();
        }

        private void InitCommands()
        {
            MainSession.PrintLogEvent = PrintLogEvent;

            SearchCommand = new RelayCommand(OnSearchClick);
            AppointCommand = new RelayCommand(ExecuteAppointAsync);
            StartAppCommand = new RelayCommand(StartApp);
            SelectedDepartmentChanged = new Action(OnSelectedDepartmentChanged);

            MainSession.AppointEvent.Subscribe(OnAppointment);
        }

        private void SaveLocation(string location)
        {
            if (string.IsNullOrEmpty(location))
            {
                return;
            }
            var queryDic = location.UrlToDic();
            MainSession.PlatformSession.AddOrUpdate(queryDic);
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
            var attIdList = data as List<string>;
            if (attIdList == null)
            {
                return;
            }

            var miaoDetailController = HttpServiceController.GetService<MiaoDetailController>();

            foreach (var attId in attIdList)
            {
                Task.Factory.StartNew(() =>
                {
                    miaoDetailController.SearchMiaoDetailAsync(attId);
                });
            }
        }

        #endregion Status Control

        #region Search

        private void OnSearchClick()
        {
            Task.Factory.StartNew(async () =>
            {
                if (MainSession.MiaoStatus.MiaoProgress != MiaoProgress.GettingMiao)
                {
                    MainSession.SetStatus(MiaoProgress.GettingMiao);
                }
                await SearchAsync();
            });
        }

        private async Task SearchAsync()
        {
            MainSession.Cookie = Cookie;

            try
            {
                MainSession.SetStatus(MiaoProgress.Searching);
                await _searchController.SearchAsync();
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
                if (MainSession.MiaoStatus.MiaoProgress == MiaoProgress.AppointStart)
                {
                    lock (OrderLock)
                    {
                        var orderList = e.OrderList;
                        ConsumeOrdersAsync(orderList);
                    }
                }
            }
        }

        private void ConsumeOrdersAsync(List<Order> orderList)
        {
            try
            {
                foreach (var order in orderList)
                {
                    var yuyueController = AppointSession.GetController(order.Date, order.Time);
                    yuyueController.StartInterval(order);
                }
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        private void ExecuteAppointAsync()
        {
            try
            {
                MainSession.Cookie = Cookie;
                var appointController = HttpServiceController.GetService<AppointController>();
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
            StartIntervalTimer();
        }

        protected override void AutoRun()
        {
            Task.Factory.StartNew(async () =>
            {
                if (MainSession.MiaoStatus.MiaoProgress < MiaoProgress.GettingMiao)
                {
                    MainSession.SetStatus(MiaoProgress.GettingMiao);
                    PrintLogEvent.Publish(this, "开始查苗了");
                }
                var miaoSchedule = HttpServiceController.GetService<SearchController>();
                await miaoSchedule.SearchAsync();
            });
        }

        #endregion AutoRun

        #region Hospital Dept

        private void OnSelectedDepartmentChanged()
        {
            var selectedDept = SelectedDepartment as HysHospital;
            MainSession.PlatformSession.AddOrUpdate(Constants.HospitalId, selectedDept.HospitalId);
            MainSession.PlatformSession.AddOrUpdate(Constants.DeptId, selectedDept.DepartmentId);

            Log(selectedDept.ToLogString());

            //MainSession.BuildMiaoSession(MainSession.PlatformSesstion[Constant.DeptId].NotNullString());
        }

        protected override void ReSession()
        {
        }

        #endregion Hospital Dept


        #region Copy / Start new App
        private void StartApp()
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    var processInfo = new ProcessStartInfo();
                    processInfo.FileName = "hys020.exe";
                    processInfo.ArgumentList.Add(Cookie);
                    processInfo.ArgumentList.Add(Location);

                    for (int i = 0; i < ProcessCount; i++)
                    {
                        var p = Process.Start(processInfo);
                    }
                }
                catch (Exception ex)
                {
                    Log(ex);
                }
            });

        }

        #endregion Copy / Start new App
    }
}
