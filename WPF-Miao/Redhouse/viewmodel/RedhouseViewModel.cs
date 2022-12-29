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

            var key = "408b32e0155250f910fe58a174e051811bd16f2b508158d0bc51fa3009283b09";
            var data = "17f715a728635041cde098f40a7796f16a46acbb37e6030ca2571c52376131e43042f1a03ff8870db42b4c011ffbf2e784f5d3668f1fc98e27b2ff75a9b09fa270fe914647c1e6a0dd57ce0ff341c86b2ef90e0a1e2031065b4ebc03ccf2f8997481ad0f325e8ab76106322fed8be88b95a0d14dceabb29ef603560c2e30381885213b1c1471736dec3d90e00de0ef9ee6c86ee51986656b25c4d4a6996d7244151bb0cdd470c61b7c37d26f14994344b62c2636ea087e80d23492f338d8d2237d7d3835ed399886b9d73c65cf4c04b1147195458f183b78a2557e2243993e342f4f2b8fc0245078f4d48052055cee187b7050d8e4a12ecb2d8546c6bf9428e9555626c00f62e17c14e20d39e0b774942ebcecc83c0e922ae3c05f7a0d5d8c412abec6b557582b460d1ef2606404457bbeaabb58192ec921a5e657de81ba4f01553558ca4ae53fa29546f7ed697e71a20ff536c6215965524b8982c8209c88397e9b5b48da36b6478f5b11aeac2020446fae1fb670dc13b33f7c2cdb1f6d6ae8cdda66ccedaee7bbefeef07371aa5896f881f36d8f61efcb7b7f439a3504f538e1a26a238720adca19f6b16581a28fcadb749c3e62b257e8434bc114b6bb5a094872805ca5800dbce9812fb8ee91bbd07d53d7132be0e667430f02d2745e915c9a41def7b61333f5c94f4e421987e083632b1e2c064cb8d14b943208de501329e07f2be878e61b2ba3de091514dd1393d93086fd3764fe77e36cbadfdee09d02b25064af1437df31e16987f2f348d3d33ca96021220e461e1e8234d1be30dad1cb903fc4fe741acf8fc5b4999844aa2b405e196d073f0ed45453220cb6d0aadadb0f63ecb6e1ee96ab8587bc4621d51e10bc4ee7d1bda99994814365df8d698a";

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
                    DepartmentId = "10072",
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
                    //var yuyueController = AppointSession.GetController(order.Date, order.Time);
                    //yuyueController.StartInterval(order);
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
            Task.Factory.StartNew(async () =>
            {
                if (MainSession.MiaoStatus.MiaoProgress < MiaoProgress.GettingMiao)
                {
                    MainSession.SetStatus(MiaoProgress.GettingMiao);
                    PrintLogEvent.Publish(this, "开始查苗了");
                }
                //var miaoSchedule = HttpServiceController.GetService<SearchController>();
                //await miaoSchedule.SearchAsync();
            });
        }

        #endregion AutoRun

        #region Hospital Dept

        private void OnSelectedDepartmentChanged()
        {
            var selectedDept = SelectedDepartment as RedhouseHospital;
            MainSession.PlatformSession.AddOrUpdate(Constants.HosId, selectedDept.HospitalId);
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
