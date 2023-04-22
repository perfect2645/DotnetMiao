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
using Tianhe.appointment;
using Tianhe.cancel;
using Tianhe.login;
using Tianhe.search;
using Tianhe.session;
using Utils;
using Utils.datetime;
using Utils.file;
using Utils.number;
using Utils.stringBuilder;

namespace Tianhe.viewmodel
{
    internal class TianheViewModel : OnTimeViewModel
    {
        #region Properties

        public ICommand ManualCommand { get; set; }
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

        private readonly object OrderLock = new object();

        private SearchController _searchController;

        #endregion Properties

        #region Constructor

        public TianheViewModel(LogPanel logPanel) : base(logPanel)
        {
            InitCommands();
            InitStaticData();
            MainSession.PrintLogEvent = PrintLogEvent;

            //TestData();
            LoginFromConfig();
        }

        private void TestData()
        {
            Interval = 200;
            StartTime = DateTime.Now.AddSeconds(10);
        }

        private void InitStaticData()
        {
            StartTime = DateTime.Today.AddHours(9).AddMinutes(59).AddSeconds(59);

            var dateRange = DateTimeUtil.GetDateRange("2023-04-27", "2023-04-27");
            DateList = new List<DspVal>();
            foreach (var date in dateRange)
            {
                var dateItem = new DspVal(date);
                DateList.Add(dateItem);
            }

            MainSession.PlatformSession.AddOrUpdate("DateList", DateList);

            TimeList = new List<DspVal>
            {
                //new DspVal("08:00:00-11:00:00", "1"),
                //new DspVal("14:00:00-16:00:00", "2"),
                //new DspVal("08:00:00-08:30:00", "3"),
                //new DspVal("08:30:00-09:00:00", "4"),
                //new DspVal("09:00:00-09:30:00", "5"),
                //new DspVal("14:00:00-14:30:00", "9"),
                //new DspVal("14:30:00-15:00:00", "10"),
                //new DspVal("14:00:00-15:00:00", "13"),
                //new DspVal("15:00:00-16:00:00", "14"),

                new DspVal("14:00:00-16:00:00", "10"),
            };

            MainSession.PlatformSession.AddOrUpdate("TimeList", TimeList);

            Departments = new List<HospitalDept>
            {
                new TianheHospital
                {
                    HospitalPrefix = "cxsq",
                    HospitalId = "1",
                    HospitalName = "天河区长兴街社区卫生服务中心",
                    DepartmentName = "九价",
                    DepartmentId = "20",
                },
                new TianheHospital
                {
                    HospitalPrefix = "ldsq",
                    HospitalId = "4",
                    HospitalName = "天河区龙洞街社区卫生服务中心",
                    DepartmentName = "九价",
                    DepartmentId = "1",
                },
                new TianheHospital
                {
                    HospitalPrefix = "ldsq",
                    HospitalId = "4",
                    HospitalName = "天河区龙洞街社区卫生服务中心",
                    DepartmentName = "四价",
                    DepartmentId = "2",
                },
                new TianheHospital
                {
                    HospitalPrefix = "ygsq",
                    HospitalId = "4",
                    HospitalName = "天河区元岗",
                    DepartmentName = "九价",
                    DepartmentId = "1",
                },
            };

            SelectedDepartment = Departments.FirstOrDefault();
            _searchController = new SearchController();

        }

        private void InitCommands()
        {
            LoginCommand = new RelayCommand(ExecuteLogin);
            ManualCommand = new RelayCommand(ExecuteManual);
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

        private void LoginFromConfig()
        {
            MainSession.Users = FileReader.DeserializeFile<List<TianheLogin>>("Login.json");
            foreach(var user in MainSession.Users)
            {
                var userController = HttpServiceController.GetService<UserController>();
                userController.GetUserAsync(user);
            }

            MainSession.InitSession();
        }

        private void ExecuteLogin()
        {
            if (StringUtil.AnyEmpty(Authorization))
            {
                Log("请检查参数");
                return;
            }

            var loginData = new TianheLogin()
            {
                
            };

            MainSession.Users.Add(loginData);

            ClearLoginData();
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
                    BuildOrders();
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

        private Order BuildOneOrder(TianheLogin user, string date, string timeId)
        {
            var hospitalId = MainSession.PlatformSession.GetString(Constants.HospitalId);
            var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);
            return new Order
            {
                Address = user.Address,
                DutyTimeId = timeId,
                HosipitalId = hospitalId,
                InoculateTimes = user.InoculateTimes,
                SeeDate = date,
                UserId = user.UserId,
                UserName = user.UserName,
                User = user,
                VaccineId = deptId
            };
        }

        protected override void AutoRun()
        {
            Task.Factory.StartNew(() => {
                try
                {
                    Task.Factory.StartNew(() => Appoint());
                    //_searchController.SearchMiao();
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
                    BuildManualOrder();
                    foreach (var order in MainSession.Orders)
                    {
                        Task.Factory.StartNew(() => StartOneManual(order.Key, order.Value));
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
            });
        }

        private void BuildManualOrder()
        {
            var defaultUser = MainSession.Users.FirstOrDefault();
            var dateList = MainSession.PlatformSession["DateList"] as List<DspVal>;
            var timeList = MainSession.PlatformSession["TimeList"] as List<DspVal>;

            var defaultDate = dateList.FirstOrDefault();
            var defaultTime = timeList.FirstOrDefault();
            var order = BuildOneOrder(defaultUser, defaultDate.Value, defaultTime.Value);

            var orderList = new List<Order>();
            orderList.Add(order);

            MainSession.Orders = new Dictionary<string, List<Order>>();
            MainSession.Orders.Add(defaultUser.UserName, orderList);
        }

        private void Appoint()
        {
            foreach (var order in MainSession.Orders)
            {
                Task.Factory.StartNew(() => StartOneOrder(order.Key, order.Value));
            }
        }

        private void StartOneManual(string userName, List<Order> orders)
        {
            try
            {
                bool isSuccess = false;
                foreach (var order in orders)
                {
                    var appointController = MainSession.AppointSession.GetController($"{userName}|{order.SeeDate}{order.DutyTimeId}");
                    isSuccess = appointController.YuyueAsync(order);
                    if (isSuccess)
                    {
                        PrintLog("预约成功");
                        PrintLog(order.ToLogString());
                        return;
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

        private void StartOneOrder(string userName, List<Order> orders)
        {
            try
            {
                bool isSuccess = false;
                while (!isSuccess)
                {
                    foreach (var order in orders)
                    {
                        var appointController = MainSession.AppointSession.GetController($"{userName}|{order.SeeDate}{order.DutyTimeId}");
                        isSuccess = appointController.YuyueAsync(order);
                        if (isSuccess)
                        {
                            PrintLog("预约成功");
                            PrintLog(order.ToLogString());
                            return;
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

        private void OnOrder(object? sender, OrderEventArgs e)
        {
            var orderTemplateList = e.OrderList;

            foreach(var user in MainSession.Users)
            {
                var orderList = new List<Order>();
                foreach (var template in orderTemplateList)
                {
                    var order = new Order
                    {
                        Address = user.Address,
                        DutyTimeId = template.DutyTimeId,
                        HosipitalId = template.HosipitalId,
                        InoculateTimes = user.InoculateTimes,
                        SeeDate = template.SeeDate,
                        User = user,
                        UserId = user.UserId,
                        UserName = user.UserName,
                        VaccineId = template.VaccineId,
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
                var defaultUser = MainSession.Users.FirstOrDefault();
                var historyController = HttpServiceController.GetService<SearchSuccessController>();
                await historyController.SearchHistoryAsync(defaultUser);

                var historyList = MainSession.PlatformSession["history"] as List<History>;
                var historyGroup = historyList.GroupBy(x => x.Key);

                var orderHistories = new List<DspVal>();
                foreach(var history in historyGroup)
                {
                    var valList = history.Select(x => x.id).ToArray();
                    if (!valList.HasItem())
                    {
                        continue;
                    }
                    var val = string.Join(",", valList);
                    var dsp = $"{history.Key} 数量{valList.Count()}";
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
                foreach(var cancelId in cancelIdList)
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
            var selectedDept = SelectedDepartment as TianheHospital;
            MainSession.PlatformSession.AddOrUpdate(Constants.DeptName, selectedDept.DepartmentName);
            MainSession.PlatformSession.AddOrUpdate(Constants.HospitalName, selectedDept.HospitalName);
            MainSession.PlatformSession.AddOrUpdate(Constants.DeptId, selectedDept.DepartmentId);
            MainSession.PlatformSession.AddOrUpdate(Constants.HospitalId, selectedDept.HospitalId);
            MainSession.PlatformSession.AddOrUpdate(Constants.HospitalPrefix, selectedDept.HospitalPrefix);

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
