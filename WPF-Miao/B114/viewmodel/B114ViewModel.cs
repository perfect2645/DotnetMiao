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
using B114.appointment;
using B114.cancel;
using B114.login;
using B114.search;
using B114.session;
using Utils;
using Utils.datetime;
using Utils.file;
using Utils.number;
using Utils.stringBuilder;
using System.Threading;

namespace B114.viewmodel
{
    internal class B114ViewModel : OnTimeViewModel
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

        public B114ViewModel(LogPanel logPanel) : base(logPanel)
        {
            InitCommands();
            InitStaticData();
            MainSession.PrintLogEvent = PrintLogEvent;

            TestData();
            LoginFromConfig();
        }

        private void TestData()
        {
            Interval = 200;
            StartTime = DateTime.Now.AddSeconds(10);
            MainSession.PrintLogEvent.Publish(this, GetIP());
        }

        private void InitStaticData()
        {
            StartTime = DateTime.Today.AddHours(8).AddMinutes(29).AddSeconds(55);

            DateList = new List<DspVal>();
            DateList.Add(new DspVal("2023-06-19"));

            MainSession.PlatformSession.AddOrUpdate("DateList", DateList);
            SelectedDate = DateList.FirstOrDefault();

            Departments = new List<HospitalDept>
            {
                new B114Hospital
                {
                    HospitalId = "H08110008",
                    HospitalName = "航天中心医院",
                    DepartmentName = "消化科门诊",
                    FirstDeptCode = "mn_fst_xhk_93dfee",
                    DepartmentId = "1243",
                },
                new B114Hospital
                {
                    HospitalId = "H08110008",
                    HospitalName = "航天中心医院",
                    DepartmentName = "九价HPV疫苗预约",
                    FirstDeptCode = "hyde_CRMYJZMZ_e15685d5_vir",
                    DepartmentId = "6125",
                }
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
            MainSession.Users = FileReader.DeserializeFile<List<B114Login>>("Login.json");
            foreach(var user in MainSession.Users)
            {
                Task.Factory.StartNew(async () =>
                {
                    var tokenController = HttpServiceController.GetService<TokenController>();
                    await tokenController.GetTokenAsync(user);
                    var userController = HttpServiceController.GetService<UserController>();
                    await userController.GetUserAsync(user);
                });
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

            var loginData = new B114Login()
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
                    MainSession.SetStatus(Base.viewmodel.status.MiaoProgress.ReadyForSearch);
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
                    MainSession.SetStatus(Base.viewmodel.status.MiaoProgress.ReadyForSearch);
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
                            PrintLog($"{order.UserName}-预约成功");
                            PrintLog(order.ToLogString());
                            return;
                        }
                        Thread.Sleep(500);
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
                        UserName = user.UserName,
                        User = user,

                    };

                    orderList.Add(order);
                }

                Task.Factory.StartNew(() => StartOneOrder(user.UserName, orderList));
            }
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
            var selectedDept = SelectedDepartment as B114Hospital;
            MainSession.PlatformSession.AddOrUpdate(Constants.DeptName, selectedDept.DepartmentName);
            MainSession.PlatformSession.AddOrUpdate(Constants.HospitalName, selectedDept.HospitalName);
            MainSession.PlatformSession.AddOrUpdate(Constants.DeptId, selectedDept.DepartmentId);
            MainSession.PlatformSession.AddOrUpdate(Constants.FirstDeptCode, selectedDept.FirstDeptCode);
            MainSession.PlatformSession.AddOrUpdate(Constants.HospitalId, selectedDept.HospitalId);

            Log(selectedDept.ToLogString());
        }

        #endregion Hospital Dept

        #region ReSession

        protected override void ReSession()
        {
            Log("ression invoke");
            foreach (var user in MainSession.Users)
            {
                Thread.Sleep(1000);
                Task.Factory.StartNew(async () =>
                {
                    //var loginController = HttpServiceController.GetService<LoginController>();
                    //await loginController.LoginAsync(user);
                    var userController = HttpServiceController.GetService<UserController>();
                    await userController.GetUserAsync(user);
                });
            }
        }

        #endregion Resession
    }
}
