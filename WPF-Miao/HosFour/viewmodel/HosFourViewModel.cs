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
using HosFour.appointment;
using HosFour.cancel;
using HosFour.login;
using HosFour.search;
using HosFour.session;
using Utils;
using Utils.datetime;
using Utils.file;
using Utils.number;
using Utils.stringBuilder;

namespace HosFour.viewmodel
{
    internal class HosFourViewModel : OnTimeViewModel
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

        public HosFourViewModel(LogPanel logPanel) : base(logPanel)
        {
            InitCommands();
            InitStaticData();
            MainSession.PrintLogEvent = PrintLogEvent;

            TestData();
            LoginFromConfig();
        }

        private void TestData()
        {
            Interval = 1000;
            StartTime = DateTime.Now.AddSeconds(20);
        }

        private void InitStaticData()
        {
            StartTime = DateTime.Today.AddHours(20).AddMinutes(29).AddSeconds(59);

            var dateRange = DateTimeUtil.GetDateRange("2023-04-20", "2023-04-20");
            DateList = new List<DspVal>();
            foreach (var date in dateRange)
            {
                var dateItem = new DspVal(date);
                DateList.Add(dateItem);
            }

            MainSession.PlatformSession.AddOrUpdate("DateList", DateList);

            TimeList = new List<DspVal>
            {
                new DspVal("08:00:00-11:00:00", "1"),
                new DspVal("14:00:00-16:00:00", "2"),
            };

            MainSession.PlatformSession.AddOrUpdate("TimeList", TimeList);

            Departments = new List<HospitalDept>
            {
                new HosFourHospital
                {
                    HospitalId = "01",
                    HospitalName = "上海市第四人民医院",
                    DepartmentName = "儿科门诊",
                    DepartmentId = "deptCode=0400|儿科门诊|1|普通门诊",
                    DocCode = "0400||儿科门诊普通号|儿科门诊|普通门诊",
                    DocName = "儿科门诊普通号",
                    DocDuty = string.Empty
                },
                new HosFourHospital
                {
                    HospitalId = "01",
                    HospitalName = "上海市第四人民医院",
                    DepartmentName = "疫苗接种高级专家门诊",
                    DepartmentId = "9289|疫苗接种高级专家门诊|102|疫苗门诊",
                    DocCode = "9289||疫苗接种高级专家门诊|疫苗接种高级专家门诊|疫苗门诊",
                    DocName = "疫苗接种高级专家门诊",
                    DocDuty = string.Empty
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
            MainSession.Users = FileReader.DeserializeFile<List<HosFourLogin>>("Login.json");
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

            var loginData = new HosFourLogin()
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

        private Order BuildOneOrder(HosFourLogin user, string date, string timeId)
        {
            var hospitalId = MainSession.PlatformSession.GetString(Constants.HospitalId);
            var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);
            return new Order
            {

            };
        }

        protected override void AutoRun()
        {
            Task.Factory.StartNew(() => {
                try
                {
                    _searchController.SearchMiao();
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
                    var appointController = MainSession.AppointSession.GetController($"{userName}|{order.RegistDate}");
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
                        var appointController = MainSession.AppointSession.GetController($"{userName}|{order.RegistDate}");
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
            var selectedDept = SelectedDepartment as HosFourHospital;
            MainSession.PlatformSession.AddOrUpdate(Constants.DeptName, selectedDept.DepartmentName);
            MainSession.PlatformSession.AddOrUpdate(Constants.HospitalName, selectedDept.HospitalName);
            MainSession.PlatformSession.AddOrUpdate(Constants.DeptId, selectedDept.DepartmentId);
            MainSession.PlatformSession.AddOrUpdate(Constants.HospitalId, selectedDept.HospitalId);
            MainSession.PlatformSession.AddOrUpdate(Constants.DocCode, selectedDept.DocCode);
            MainSession.PlatformSession.AddOrUpdate(Constants.DocName, selectedDept.DocName);
            MainSession.PlatformSession.AddOrUpdate(Constants.DocDuty, selectedDept.DocDuty);

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
