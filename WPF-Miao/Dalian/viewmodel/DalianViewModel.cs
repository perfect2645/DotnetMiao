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
using Dalian.appointment;
using Dalian.login;
using Dalian.search;
using Dalian.session;
using Utils;
using Utils.datetime;
using Utils.file;
using Utils.number;
using Utils.stringBuilder;
using System.Threading;

namespace Dalian.viewmodel
{
    internal class DalianViewModel : OnTimeViewModel
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

        public DalianViewModel(LogPanel logPanel) : base(logPanel)
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
            //StartTime = DateTime.Now.AddSeconds(10);
            MainSession.PrintLogEvent.Publish(this, GetIP());
        }

        private void InitStaticData()
        {
            StartTime = DateTime.Today.AddHours(18).AddMinutes(59).AddSeconds(50);

            DateList = new List<DspVal>();

            var targetDate = DateTimeUtil.GetTargetDate(7);

            //targetDate = "2023-12-18";
            DateList.Add(new DspVal(targetDate, DateTimeUtil.GetDateTime(targetDate, "yyyyMMdd")));

            MainSession.PlatformSession.AddOrUpdate("DateList", DateList);
            SelectedDate = DateList.FirstOrDefault();

            Departments = new List<HospitalDept>
            {
                //new DalianHospital
                //{
                //    AppId = "wxa794c2a4fcfeb7f4",
                //    HospitalId = "10018",
                //    HospitalName = "大连市妇女儿童医疗中心（集团）",
                //    DepartmentId = "166215",
                //    DepartmentName = "中医科门诊",
                //    HisDeptId = "",
                //    RegLevelId = "02",
                //    RegLevelName = "主任医师-矫承媛"
                //},
                new DalianHospital
                {
                    AppId = "wxa794c2a4fcfeb7f4",
                    HospitalId = "10018",
                    HospitalName = "大连市妇女儿童医疗中心（集团）",
                    DepartmentId = "139253",
                    DepartmentName = "长江路预防接种门诊（妇产院区）",
                    HisDeptId = "3978",
                    RegLevelId = "67",
                    RegLevelName = "进口九价（9-45岁）"
                },
            };

            SelectedDepartment = Departments.FirstOrDefault();
            _searchController = new SearchController();
        }

        private void InitCommands()
        {
            LoginCommand = new RelayCommand(ExecuteLogin);
            ManualCommand = new RelayCommand(ExecuteManual);

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
            MainSession.Users = FileReader.DeserializeFile<List<DalianLogin>>("Login.json");
            foreach(var user in MainSession.Users)
            {
                Thread.Sleep(1000);
                Task.Factory.StartNew(async () =>
                {
                    //var tokenController = HttpServiceController.GetService<TokenController>();
                    //await tokenController.GetTokenAsync(user);
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

            var loginData = new DalianLogin()
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
                var randomOrders = orders.DisorderItems();
                bool isSuccess = false;
                foreach (var order in randomOrders)
                {
                    if (isSuccess)
                    {
                        return;
                    }
                    var appointController = HttpServiceController.GetService<YuyueController>();
                    isSuccess = appointController.YuyueAsync(order);
                    if (isSuccess)
                    {
                        PrintLog($"{order.UserName}-预约成功");
                        PrintLog(order.ToLogString());
                        return;
                    }
                    Thread.Sleep(200);
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
                        PointId = template.PointId,
                        PointName = template.PointName,
                        PointDate = template.PointDate,
                        RegLevelId = template.RegLevelId,
                        RegLevelName = template.RegLevelName,
                        DeptId = template.DeptId,
                        VisitTime = template.VisitTime,
                        BeginTime = template.BeginTime,
                        EndTime = template.EndTime,
                        DiagnoseFee = template.DiagnoseFee,
                        NoonId = template.NoonId,
                        NoonName = template.NoonName,
                    };

                    orderList.Add(order);
                }

                Task.Factory.StartNew(() => StartOneOrder(user.UserName, orderList));
            }
        }

        #endregion Appoint

        #region Hospital Dept

        private void OnSelectedDepartmentChanged()
        {
            var selectedDept = SelectedDepartment as DalianHospital;
            MainSession.PlatformSession.AddOrUpdate(Constants.AppId, selectedDept.AppId);
            MainSession.PlatformSession.AddOrUpdate(Constants.DeptName, selectedDept.DepartmentName);
            MainSession.PlatformSession.AddOrUpdate(Constants.HospitalName, selectedDept.HospitalName);
            MainSession.PlatformSession.AddOrUpdate(Constants.DeptId, selectedDept.DepartmentId);
            MainSession.PlatformSession.AddOrUpdate(Constants.HospitalId, selectedDept.HospitalId);
            MainSession.PlatformSession.AddOrUpdate(Constants.RegLevelId, selectedDept.RegLevelId);
            MainSession.PlatformSession.AddOrUpdate(Constants.RegLevelName, selectedDept.RegLevelName);
            MainSession.PlatformSession.AddOrUpdate(Constants.HisDeptId, selectedDept.HisDeptId);

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
                    var userController = HttpServiceController.GetService<UserController>();
                    await userController.GetUserAsync(user);
                });
            }
        }

        #endregion Resession
    }
}
