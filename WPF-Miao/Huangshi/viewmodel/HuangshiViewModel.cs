using Base.model;
using Base.viewModel;
using Base.viewModel.hospital;
using CommunityToolkit.Mvvm.Input;
using CoreControl.LogConsole;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Huangshi.appointment;
using Huangshi.login;
using Huangshi.search;
using Huangshi.session;
using Utils;
using Utils.datetime;
using Utils.file;
using Utils.number;
using Utils.stringBuilder;
using Huangshi.cancel;
using System.Threading;
using Huangshi.Encrypt;
using System.Timers;

namespace Huangshi.viewmodel
{
    internal class HuangshiViewModel : OnTimeViewModel
    {
        #region Properties

        public ICommand SearchCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand CancelOneCommand { get; set; }
        public ICommand RefreshHistoryCommand { get; set; }
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
                NotifyUI(() => Authorization);
            }
        }

        private List<DspVal> _historyList;
        public List<DspVal> HistoryList
        {
            get { return _historyList; }
            set
            {
                _historyList = value;
                NotifyUI(() => HistoryList);
            }
        }

        private DspVal _selectedHistory;
        public DspVal SelectedHistory
        {
            get { return _selectedHistory; }
            set
            {
                _selectedHistory = value;
                NotifyUI(() => SelectedHistory);
            }
        }

        private readonly object OrderLock = new object();

        private SearchController _searchController;

        #endregion Properties

        #region Constructor

        public HuangshiViewModel(LogPanel logPanel) : base(logPanel)
        {
            InitCommands();
            InitStaticData();
            MainSession.PrintLogEvent = PrintLogEvent;

            TestData();
            LoginFromConfigAsync();
        }

        private void TestData()
        {
            //var phoneEncode = JsReader.GetEncodeString("13940897525");
            Interval = 200;
            //StartTime = DateTime.Now.AddSeconds(5);
        }

        private void InitStaticData()
        {
            StartTime = DateTime.Today.AddHours(8).AddMinutes(29).AddSeconds(30);

            var dateRange = DateTimeUtil.GetDateRange("2023-04-03", "2023-04-04");
            DateList = new List<DspVal>();
            foreach(var date in dateRange)
            {
                var dateItem = new DspVal(date);
                DateList.Add(dateItem);
            }

            MainSession.PlatformSession.AddOrUpdate("DateList", DateList);

            TimeList = new List<DspVal>
            {
                //new DspVal("09:00-10:00"),
                new DspVal("08:00-17:00"),
                //new DspVal("11:00-12:00"),
                //new DspVal("14:00-15:00"),
                //new DspVal("15:00-16:00"),
                //new DspVal("16:00-17:00"),
            };

            MainSession.PlatformSession.AddOrUpdate("TimeList", TimeList);

            Departments = new List<HospitalDept>
            {   
                new HuangshiHospital
                {
                    HospitalId = "1",
                    HospitalName = "黄石妇幼",
                    DepartmentName = "九价",
                    DepartmentId = "216",
                    PackageId = "000109"
                },
                new HuangshiHospital
                {
                    HospitalId = "1",
                    HospitalName = "黄石妇幼",
                    DepartmentName = "四价",
                    DepartmentId = "215",
                },
                new HuangshiHospital
                {
                    HospitalId = "1",
                    HospitalName = "黄石妇幼",
                    DepartmentName = "国产2价",
                    DepartmentId = "213",
                    PackageId = "000092",
                },
            };

            SelectedDepartment = Departments.FirstOrDefault();
            _searchController = new SearchController();
        }

        private void InitCommands()
        {
            SearchCommand = new RelayCommand(ExecuteManual);
            RefreshHistoryCommand = new AsyncRelayCommand(ExecuteSearchHistory);
            CancelCommand = new AsyncRelayCommand(ExecuteCancel);
            CancelOneCommand = new AsyncRelayCommand(ExecuteCancelOne);
            SelectedDepartmentChanged = new Action(OnSelectedDepartmentChanged);
            MainSession.OrderEvent.Subscribe(OnOrder);
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

        private void LoginFromConfigAsync()
        {
            MainSession.Users = FileReader.DeserializeFile<List<HuangshiLogin>>("Login.json");
            MainSession.InitSession();
        }

        private void ClearLoginData()
        {
            Authorization = string.Empty;
        }

        #endregion Login

        #region AutoRun

        protected override void StartAutoRun()
        {
            Task.Factory.StartNew(async () => {
                try
                {
                    //BuildOrders();
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

        private void BuildOrders()
        {
            MainSession.Orders = new Dictionary<string, List<Order>>();
            var dateList = MainSession.PlatformSession["DateList"] as List<DspVal>;
            var timeList = MainSession.PlatformSession["TimeList"] as List<DspVal>;

            foreach (var user in MainSession.Users)
            {
                var orderList = new List<Order>();
                var userName = user.UserName;
                foreach (var date in dateList)
                {
                    foreach (var time in timeList)
                    {
                        Order orderWithTime = BuildOneOrder(user, date.Value, time.Value);
                        orderList.Add(orderWithTime);
                    }
                }
                orderList = orderList.DisorderItems();
                MainSession.Orders.AddOrUpdate(userName, orderList);
            }
        }

        private Order BuildOneOrder(HuangshiLogin user, string date, string timeId)
        {
            var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);
            return new Order
            {
                DeptId = deptId,
                UserName = user.UserName,
                User = user,
                Birthday = user.Birthday,
                ContactPhone = user.Phone,
                Date = date,
                SFZHM = user.IdCard,
                Time = timeId,
                UserPhone = user.Phone,
            };
        }

        protected override void AutoRun()
        {
            Task.Factory.StartNew(async () => {
                try
                {
                    //Appoint();     
                    await _searchController.SearchMiaoAsync();
                    //_searchController.SearchMiaoAsync();
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
                    BuildOrders();
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
                    foreach (var order in orders)
                    {
                        var appointController = MainSession.AppointSession.GetController($"{userName}");
                        isSuccess = appointController.YuyueAsync(order);
                        if (isSuccess)
                        {
                            PrintLog("预约成功");
                            PrintLog(order.ToLogString());
                            return;
                        }
                        Thread.Sleep(1000);
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

        private void OnOrder(object? sender, OrderEventArgs e)
        {
            var orderTemplateList = e.OrderList;

            var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);

            foreach (var user in MainSession.Users)
            {
                var orderList = new List<Order>();
                foreach (var template in orderTemplateList)
                {
                    var order = new Order
                    {
                        DeptId = deptId,
                        UserName = user.UserName,
                        User = user,
                        Birthday = user.Birthday,
                        ContactPhone = user.Phone,
                        Date = template.Date,
                        SFZHM = user.IdCard,
                        Time = template.Time,
                        UserPhone = user.Phone,
                    };

                    orderList.Add(order);
                }
                Task.Factory.StartNew(() => StartOneOrder(user.UserName, orderList));
            }
        }

        private void DirectlyOrder(string scheduleId)
        {
            var order = new Order();
        }

        #endregion Appoint

        #region Cancel

        private async Task ExecuteSearchHistory()
        {
            try
            {
                MainSession.HistoryList = new List<History>();
                foreach (var user in MainSession.Users)
                {
                    var historyController = HttpServiceController.GetService<SearchSuccessController>();
                    await historyController.SearchHistoryAsync(user);
                    Thread.Sleep(1000);
                }

                var historyList = MainSession.HistoryList;
                var historyGroup = historyList.GroupBy(x => x.Key);

                var orderHistories = new List<DspVal>();
                foreach (var history in historyGroup)
                {
                    var valList = history.Select(x => x.registrationId).ToArray();
                    if (!valList.HasItem())
                    {
                        continue;
                    }
                    var nameArr = history.Select(x => x.patientName).ToArray();
                    var val = string.Join(",", valList);
                    var names = string.Join(",", nameArr);
                    var dsp = $"{history.Key} 数量{valList.Count()} | {names} ";
                    orderHistories.Add(new DspVal(dsp, val));
                }

                HistoryList = orderHistories;
                PrintLog($"查询预约记录成功-数据量:{HistoryList.Count}");
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        private async Task ExecuteCancelOne()
        {
            try
            {
                if (SelectedHistory == null)
                {
                    PrintLog("请选择一个取消时间");
                    return;
                }
                await Task.Factory.StartNew(() =>
                {
                    var cancelController = HttpServiceController.GetService<CancelController>();
                    var defaultOrderId = SelectedHistory.Value.SplitToList(",").FirstOrDefault();
                    var defaultUser = MainSession.Users.FirstOrDefault();
                    cancelController.CancelAsync(defaultUser, defaultOrderId);
                });

                await ExecuteSearchHistory();
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        private async Task ExecuteCancel()
        {
            try
            {
                if (SelectedHistory == null)
                {
                    PrintLog("请选择一个取消时间");
                    return;
                }
                var cancelIdList = SelectedHistory.Value.SplitToList(",");
                var defaultUser = MainSession.Users.FirstOrDefault();

                var cancelTasks = new List<Task>();
                foreach (var cancelId in cancelIdList)
                {
                    var cancelTask = Task.Factory.StartNew(() =>
                    {
                        var cancelController = HttpServiceController.GetService<CancelController>();
                        cancelController.CancelAsync(defaultUser, cancelId);
                    });
                    cancelTasks.Add(cancelTask);
                }

                Task.WaitAll(cancelTasks.ToArray());

                await ExecuteSearchHistory();
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
            var selectedDept = SelectedDepartment as HuangshiHospital;
            MainSession.PlatformSession.AddOrUpdate(Constants.DeptName, selectedDept.DepartmentName);
            MainSession.PlatformSession.AddOrUpdate(Constants.HospitalName, selectedDept.HospitalName);
            MainSession.PlatformSession.AddOrUpdate(Constants.DeptId, selectedDept.DepartmentId);
            MainSession.PlatformSession.AddOrUpdate(Constants.HospitalId, selectedDept.HospitalId);
            MainSession.PlatformSession.AddOrUpdate(Constants.DocId, selectedDept.DoctorId);
            MainSession.PlatformSession.AddOrUpdate(Constants.DoctorName, selectedDept.DoctorName);
            MainSession.PlatformSession.AddOrUpdate(Constants.PackageId, selectedDept.PackageId);

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
