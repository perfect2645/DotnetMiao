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
using Lzy.appointment;
using Lzy.cancel;
using Lzy.login;
using Lzy.search;
using Lzy.session;
using Utils;
using Utils.datetime;
using Utils.file;
using Utils.number;
using Utils.stringBuilder;
using System.Threading;
using Base.viewmodel.status;
using Utils.Rdom;

namespace Lzy.viewmodel
{
    internal class LzyViewModel : OnTimeViewModel
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

        public LzyViewModel(LogPanel logPanel) : base(logPanel)
        {
            InitCommands();
            InitStaticData();
            MainSession.PrintLogEvent = PrintLogEvent;

            TestData();
            LoginFromConfig();
        }

        private void TestData()
        {
            Interval = 800;
            StartTime = DateTime.Now.AddSeconds(5);
        }

        private void InitStaticData()
        {
            StartTime = DateTime.Today.AddHours(4).AddMinutes(59).AddSeconds(58);

            DateList = new List<DspVal>();
            DateList.Add(new DspVal("2023-06-07"));

            MainSession.DateList = DateList;
            SelectedDate = DateList.FirstOrDefault();

            TimeList = new List<DspVal>();
            TimeList.Add(new DspVal("08:00~09:00","543"));
            TimeList.Add(new DspVal("09:00~10:00", "541"));
            TimeList.Add(new DspVal("10:00~10:20", "542"));

            MainSession.TimeList = TimeList;
            SelectedTime = TimeList.FirstOrDefault();

            Departments = new List<HospitalDept>
            {
                new LzyHospital
                {
                    HospitalId = "30",
                    HospitalName = "龙跃四预防保健门诊",
                    DepartmentName = "HPV九价周三",
                    DepartmentId = "19",
                },
                new LzyHospital
                {
                    HospitalId = "30",
                    HospitalName = "国风美唐预防保健门诊",
                    DepartmentName = "HPV九价周六",
                    DepartmentId = "44",
                },
                new LzyHospital
                {
                    HospitalId = "30",
                    HospitalName = "龙跃四预防保健门诊",
                    DepartmentName = "HPV九价周六",
                    DepartmentId = "32",
                },
                new LzyHospital
                {
                    HospitalId = "30",
                    HospitalName = "龙跃四预防保健门诊",
                    DepartmentName = "HPV九价周三",
                    DepartmentId = "19",
                },
                new LzyHospital
                {
                    HospitalId = "30",
                    HospitalName = "国风美唐预防保健门诊",
                    DepartmentName = "HPV九价周三",
                    DepartmentId = "42",
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
            StopIntervalTimer();
        }

        #endregion Status Control

        #region Login

        private void LoginFromConfig()
        {
            MainSession.Users = FileReader.DeserializeFile<List<LzyLogin>>("Login.json");

            /*
            foreach(var user in MainSession.Users)
            {
                if (string.IsNullOrEmpty(user.UserName))
                {
                    user.UserName = StringRandom.GetRandomName();
                    user.Mobile = NumberUtil.GetRandomPhone().NotNullString();
                    MainSession.PrintLogEvent.Publish(user, user.ToLogString());
                }
            }
            */

            MainSession.InitSession();
        }

        private void ExecuteLogin()
        {
            if (StringUtil.AnyEmpty(Authorization))
            {
                Log("请检查参数");
                return;
            }

            var loginData = new LzyLogin()
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
                    MainSession.SetStatus(MiaoProgress.GettingMiao);
                    StartIntervalTimer();
                    //StartReSessionTimer();
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
                    if (MainSession.GetStatus() == MiaoProgress.MiaoGet)
                    {
                        return;
                    }
                    _searchController.SearchMiao(SelectedDate);
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

        private void BuildOrders()
        {
            var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);
            foreach(var user in MainSession.Users)
            {
                var userOrders = new List<Order>();
                foreach(var timeInfo in MainSession.TimeList)
                {
                    var userName = user.UserName;
                    var phone = user.Mobile;
                    if (string.IsNullOrEmpty(userName))
                    {
                        userName = StringRandom.GetRandomName();
                        phone = NumberUtil.GetRandomPhone().NotNullString();
                        MainSession.PrintLogEvent.Publish(user, $"Random user - name:{userName}, phone={phone}");
                    }

                    var order = new Order
                    {
                        Date = SelectedDate.Value,
                        DeptId = deptId,
                        Mobile = phone,
                        TimeId = timeInfo.Value,
                        UserName = userName,
                        User = user,
                    };
                    userOrders.Add(order);
                }
                MainSession.Orders.AddOrUpdate(user.UserName, userOrders);
            }
        }


        private void ExecuteManual()
        {
            Task.Factory.StartNew(() => {
                try
                {
                    //BuildOrders();
                    foreach(var userOrder in MainSession.Orders)
                    {
                        Task.Factory.StartNew(() =>
                        {
                            StartOneOrder(userOrder.Key, userOrder.Value);
                        });
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

        private void StartOneOrder(string userName, List<Order> orders)
        {
            try
            {
                bool isSuccess = false;
                var cnt = 0;
                while (!isSuccess)
                {
                    if (cnt > 5)
                    {
                        return;
                    }
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
                        Thread.Sleep(500);
                    }
                    cnt++;
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
                    var userName = user.UserName;
                    var phone = user.Mobile;
                    if (string.IsNullOrEmpty(userName))
                    {
                        userName = StringRandom.GetRandomName();
                        phone = NumberUtil.GetRandomPhone().NotNullString();
                        MainSession.PrintLogEvent.Publish(user, $"Random user - name:{userName}, phone={phone}");
                    }
                    var order = new Order
                    {
                        UserName = userName,
                        Mobile = phone,
                        User = user,
                        Date = template.Date,
                        DeptId = template.DeptId,
                        TimeId = template.TimeId,
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
            var selectedDept = SelectedDepartment as LzyHospital;
            MainSession.PlatformSession.AddOrUpdate(Constants.DeptName, selectedDept.DepartmentName);
            MainSession.PlatformSession.AddOrUpdate(Constants.HospitalName, selectedDept.HospitalName);
            MainSession.PlatformSession.AddOrUpdate(Constants.DeptId, selectedDept.DepartmentId);
            MainSession.PlatformSession.AddOrUpdate(Constants.HospitalId, selectedDept.HospitalId);
            MainSession.PlatformSession.AddOrUpdate(Constants.DoctorId, selectedDept.DoctorId);
            MainSession.PlatformSession.AddOrUpdate(Constants.DoctorName, selectedDept.DoctorName);
            MainSession.PlatformSession.AddOrUpdate(Constants.DoctorSign, selectedDept.DoctorSign);

            Log(selectedDept.ToLogString());
        }

        #endregion Hospital Dept

        #region ReSession

        protected override void ReSession()
        {
            Log("ression invoke");
            foreach (var user in MainSession.Users)
            {
                Task.Factory.StartNew(async () =>
                {
                    var userController = HttpServiceController.GetService<MiaoController>();
                    await userController.SearchMiaoAsync(SelectedDate.Value);
                });
            }
        }

        #endregion Resession
    }
}
