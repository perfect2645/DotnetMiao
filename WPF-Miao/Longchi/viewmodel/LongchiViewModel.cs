using Base.model;
using Base.viewModel;
using Base.viewModel.hospital;
using CommunityToolkit.Mvvm.Input;
using CoreControl.LogConsole;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
using Longchi.appointment;
using Longchi.search;
using Longchi.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Utils;
using Utils.datetime;
using Utils.file;
using Utils.number;
using Utils.stringBuilder;

namespace Longchi.viewmodel
{
    internal class LongchiViewModel : OnTimeViewModel
    {
        #region Properties

        public ICommand SearchCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand CancelCommand { get; set; }

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

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                NotifyUI(() => UserName);
            }
        }

        private readonly object OrderLock = new object();

        private SearchController _searchController;

        private List<LongchiLogin> _LongchiLogins = new List<LongchiLogin>();

        #endregion Properties

        #region Constructor

        public LongchiViewModel(LogPanel logPanel) : base(logPanel)
        {
            Interval = 200;
            InitCommands();
            InitStaticData();
            MainSession.PrintLogEvent = PrintLogEvent;

            TestData();
            LoginFromConfig();
        }

        private void TestData()
        {
            StartTime = DateTime.Now.AddSeconds(10);
        }

        private void InitStaticData()
        {
            StartTime = new DateTime(2023, 1, 1, 16, 59, 50);

            DateList = new List<DspVal>
            {
                new DspVal(DateTimeUtil.GetDayOfWeek(DayOfWeek.Monday)),
                new DspVal(DateTimeUtil.GetDayOfWeek(DayOfWeek.Tuesday)),
                //new DspVal(DateTimeUtil.GetDayOfWeek(DayOfWeek.Thursday)),
                //new DspVal(DateTimeUtil.GetDayOfWeek(DayOfWeek.Friday)),
                //new DspVal(DateTimeUtil.GetDayOfWeek(DayOfWeek.Saturday)),
                //new DspVal(DateTimeUtil.GetDayOfNextWeek(DayOfWeek.Monday)),
                //new DspVal(DateTimeUtil.GetDayOfNextWeek(DayOfWeek.Tuesday)),
                //new DspVal(DateTimeUtil.GetDayOfNextWeek(DayOfWeek.Wednesday)),
                //new DspVal(DateTimeUtil.GetDayOfNextWeek(DayOfWeek.Thursday)),
                //new DspVal(DateTimeUtil.GetDayOfNextWeek(DayOfWeek.Friday)),
                //new DspVal(DateTimeUtil.GetDayOfNextWeek(DayOfWeek.Saturday)),
                //new DspVal(DateTimeUtil.GetDayOfNextWeek(DayOfWeek.Sunday)),
            };

            MainSession.PlatformSession.AddOrUpdate("DateList", DateList);

            TimeList = new List<DspVal>
            {
                 new DspVal("08:00"),
                 new DspVal("09:00"),
                 //new DspVal("10:00"),
                 //new DspVal("11:00"),
            };

            MainSession.PlatformSession.AddOrUpdate("TimeList", TimeList);

            Departments = new List<HospitalDept>
            {
                new LongchiHospital
                {
                    HospitalId = "hpv_ym.zzytrj.net:15003",
                    HospitalName = "龙池/角美 社区卫生服务中心",
                    DepartmentName = "【九价疫苗】",
                    DepartmentList = new List<string> { "400", "800" },
                    TimeIdList = new List<string> { "1987", "1992", "1997", "2002", "2007" }
                },
                new LongchiHospital
                {
                    HospitalId = "hpv_ym.zzytrj.net:15003",
                    HospitalName = "龙池/角美 社区卫生服务中心",
                    DepartmentName = "【四价疫苗】",
                    DepartmentList = new List<string> { "300", "700" },
                    TimeIdList = new List<string> { "1987", "1992", "1997", "2002", "2007" }
                },
            };

            SelectedDepartment = Departments.FirstOrDefault();
            _searchController = new SearchController();
        }

        private void InitCommands()
        {
            LoginCommand = new RelayCommand(ExecuteLogin);
            SearchCommand = new RelayCommand(ExecuteManual);
            CancelCommand = new RelayCommand(ExecuteCancel);

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

        private void LoginFromConfig()
        {
            _LongchiLogins = FileReader.DeserializeFile<List<LongchiLogin>>("Login.json");

            MainSession.Users = _LongchiLogins;

            MainSession.InitSession();
            StartAutoRun();
        }

        private void ExecuteLogin()
        {
            if (StringUtil.AnyEmpty(UserName, Cookie))
            {
                Log("请检查参数");
                return;
            }

            var loginData = new LongchiLogin()
            {
                Cookie = Cookie,
            };

            _LongchiLogins.Add(loginData);

            ClearLoginData();
        }

        private void ClearLoginData()
        {
            Cookie = string.Empty;
            UserName = string.Empty;
        }

        #endregion Login

        #region AutoRun

        protected override void StartAutoRun()
        {
            Task.Factory.StartNew(async () => {
                try
                {
                    await _searchController.GetUsersAsync();
                    BuildOrders();
                    StartOnTimeTimer();
                    StartReSessionTimer();
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

        private void BuildOrders()
        {
            MainSession.Orders = new Dictionary<string, List<Order>>();
            var deptList = MainSession.DeptList;
            var dateList = MainSession.PlatformSession["DateList"] as List<DspVal>;
            var timeList = MainSession.PlatformSession["TimeList"] as List<DspVal>;

            foreach (var user in _LongchiLogins)
            {
                var orderList = new List<Order>();
                var userName = user.UserName;
                foreach (var dept in deptList)
                {
                    foreach (var date in dateList)
                    {
                        Order order = BuildOneOrder(user, dept, date.Value);
                        orderList.Add(order);
                        foreach(var time in timeList)
                        {
                            Order orderWithTime = BuildOneOrder(user, dept, $"{date.Value} {time.Value}");
                            orderList.Add(orderWithTime);
                        }
                    }
                }
                orderList = orderList.DisorderItems();
                MainSession.Orders.AddOrUpdate(userName, orderList);
            }
        }

        private Order BuildOneOrder(LongchiLogin user, string dept, string date)
        {
            return new Order
            {
                Cookie = user.Cookie,
                Date = date,
                Dizhi = dept,
                UserId = user.UserId,
                UserName = user.UserName,
                YuyueUserAdd = user.YuyueUserAdd,
                YuyueName = user.UserName,
                YuyueUserSuoshu = user.YuyueUserSuoshu,
                UserCode = user.UserCode,
                FamilyId = user.FamilyId,
            };
        }

        protected override void AutoRun()
        {
            Task.Factory.StartNew(() => {
                try
                {
                    Appoint();
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

        private void ExecuteManual()
        {
            Task.Factory.StartNew(() => {
                try
                {
                    Appoint();
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

        private void Appoint()
        {
            foreach (var order in MainSession.Orders)
            {
                Task.Factory.StartNew(() => StartOneOrder(order.Key, order.Value));
            }
        }

        private void StartOneOrder(string userName, List<Order> orders)
        {
            try
            {
                bool isSuccess = false;
                while (!isSuccess)
                {
                    foreach(var order in orders)
                    {
                        var appointController = MainSession.AppointSession.GetController($"{userName}|{order.Date}");
                        isSuccess = appointController.YuyueAsync(order);
                        if (isSuccess)
                        {
                            PrintLog("预约成功");
                            PrintLog(order.ToLogString());
                            break;
                        }
                    }
                }
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

        private void DirectlyOrder(string scheduleId)
        {

            var order = new Order();
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
            var selectedDept = SelectedDepartment as LongchiHospital;
            MainSession.PlatformSession.AddOrUpdate(Constants.DeptName, selectedDept.DepartmentName);
            MainSession.PlatformSession.AddOrUpdate(Constants.HospitalName, selectedDept.HospitalName);
            MainSession.TimeIdList = selectedDept.TimeIdList;
            MainSession.DeptList = selectedDept.DepartmentList;

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
