using Base.model;
using Base.viewmodel.status;
using Base.viewModel;
using Base.viewModel.hospital;
using CommunityToolkit.Mvvm.Input;
using CoreControl.LogConsole;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
using Redhouse.appointment;
using Redhouse.appointment.Yuyue;
using Redhouse.Encrypt;
using Redhouse.search;
using Redhouse.session;
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

namespace Redhouse.viewmodel
{
    internal class RedhouseViewModel : OnTimeViewModel
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

        private string _authorization;
        public string Authorization
        {
            get { return _authorization; }
            set
            {
                _authorization = value;
                MainSession.Authorization = value;
                NotifyUI(() => Authorization);
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

        public RedhouseViewModel(LogPanel logPanel) : base(logPanel)
        {
            InitCommands();
            InitStaticData();
            InitControllers();

            TestData();
        }

        private void TestData()
        {
            Authorization = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoib2JZUU90N3FwcS1wY2JXN0lKektRYXRVSVMxSSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkZhd2VpIiwiX1RpbWVTdGFtcF8iOiIxMi8yOS8yMDIyIDI6MjU6NDIgUE0iLCJuYmYiOjE2NzIyOTUxNDIsImV4cCI6MTY3MjQwMzE0MiwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo0NDMzMiIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NDQzMzIifQ.zV_SKgKV6YSCzhPVx4fLRGyDP78i6RlDJp4zr626riM";

            var key = "d8c68cece7e53d9e7f1f2a03f39b1e0adcc0b15175bd1679ec9ccd122e81cb01";
            var data = "86aedbb4d5f20e79732375d0f42196457385ba4b7adad5f12de3602972c32e18fb0b021e8a12386e52e2f266d375b37f6f47a469e4b74491b19a3d5479f57566";

            var result = HfzEncrypt.Decrypt(key, data);

            //StartTime = DateTime.Now.AddSeconds(20);
        }

        private void InitControllers()
        {
            _searchController = new SearchController();
        }

        private void InitStaticData()
        {
            StartTime = new DateTime(2022, 12, 19, 10, 59, 50);

            Departments = new List<HospitalDept>
            {
                new RedhouseHospital
                {
                    HospitalId = "10001",
                    HospitalName = "红房子医院",
                    DepartmentId = "981",
                    DepartmentName = "九价宫颈癌疫苗（进口）",
                },
                //<option value="24">儿童乙肝疫苗（免费）</option>
                new RedhouseHospital
                {
                    HospitalId = "10001",
                    HospitalName = "红房子医院",
                    DepartmentId = "10072",
                    DepartmentName = "孕前保健",
                },
            };

            DateList = new List<DspVal>
            {
                new DspVal(DateTimeUtil.GetDayOfWeek(DayOfWeek.Wednesday)),
                new DspVal(DateTimeUtil.GetDayOfWeek(DayOfWeek.Thursday)),
                new DspVal(DateTimeUtil.GetDayOfWeek(DayOfWeek.Friday)),
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
                await _searchController.GetUserInfo();
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
                lock (OrderLock)
                {
                    var orderList = e.OrderList;
                    OnAppointment(orderList);
                }
            }
        }

        private void OnAppointment(List<Order> orderList)
        {
            try
            {
                if (MainSession.GetStatus() == MiaoProgress.AppointEnd)
                {
                    return;
                }

                for (int i = 0; i < 5; i++)
                {
                    foreach (var order in orderList)
                    {
                        if (MainSession.GetStatus() == MiaoProgress.AppointEnd)
                        {
                            return;
                        }
                        var yuyueController = HttpServiceController.GetService<YuyueController>();
                        yuyueController.YuyueAsync(order);
                    }
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
                var appointController = HttpServiceController.GetService<YuyueController>();
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
            Task.Factory.StartNew(() =>
            {
                MainSession.SetStatus(MiaoProgress.GettingMiao);
                _searchController.GetMiao();
                PrintLogEvent.Publish(this, "开始查苗了");
            });
        }

        #endregion AutoRun

        #region Hospital Dept

        private void OnSelectedDepartmentChanged()
        {
            var selectedDept = SelectedDepartment as RedhouseHospital;
            MainSession.PlatformSession.AddOrUpdate(Constants.HosId, selectedDept.HospitalId);
            MainSession.PlatformSession.AddOrUpdate(Constants.DepartId, selectedDept.DepartmentId);

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
                    processInfo.FileName = "Redhouse.exe";
                    //processInfo.ArgumentList.Add(Cookie);

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
