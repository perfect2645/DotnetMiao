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
using Sanya.appointment;
using Sanya.login;
using Sanya.search;
using Sanya.session;
using Utils;
using Utils.datetime;
using Utils.file;
using Utils.number;
using Utils.stringBuilder;
using System.Threading;

namespace Sanya.viewmodel
{
    internal class SanyaViewModel : OnTimeViewModel
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

        public SanyaViewModel(LogPanel logPanel) : base(logPanel)
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
            StartTime = DateTime.Today.AddHours(8).AddMinutes(59).AddSeconds(55);

            DateList = new List<DspVal>();

            Departments = new List<HospitalDept>
            {
                new SanyaHospital
                {
                    OrgCode = "A572000B004",
                    ZoneCode = "460200",
                    ZoneName = "5LiJ5Lqa5biC",
                    AppCode = "HC_SANYA_WX",
                    HospitalId = "A572000B004",
                    HospitalName = "三亚市吉阳区荔枝沟卫生院",
                    SubscribeType = "",
                    DepartmentId = "",
                    DepartmentName = "九价疫苗"
                },
                new SanyaHospital
                {
                    OrgCode = "A572000C001-03",
                    ZoneCode = "460200",
                    ZoneName = "5LiJ5Lqa5biC",
                    AppCode = "HC_SANYA_WX",
                    HospitalId = "A572000C001",
                    HospitalName = "三亚市吉阳区大东海社区卫生服务站",
                    SubscribeType = "2c90a2f271a585100171a5e96d030003",
                    DepartmentId = "2c90803b7478b52801747b2e7285014e",
                    DepartmentName = "13价肺炎疫苗（进口自费）"
                },
                new SanyaHospital
                {
                    OrgCode = "HLQFYBJY",
                    ZoneCode = "350200",
                    ZoneName = "5Y6m6Zeo5biC",
                    AppCode = "HC_PUB_WX",
                    HospitalId = "",
                    HospitalName = "湖里区妇幼保健院",
                    SubscribeType = "2c90804988c4aa5a0188dce414d0150b",
                    DepartmentId = "2c90811988d966530188dce5038d007b",
                    DepartmentName = "九价HPV疫苗(进口)（第一针）"
                },
                new SanyaHospital
                {
                    OrgCode = "460200",
                    ZoneCode = "460200",
                    ZoneName = "5LiJ5Lqa5biC",
                    AppCode = "HC_SANYA_WX",
                    HospitalId = "A572000A001",
                    HospitalName = "三亚市人民医院",
                    DepartmentId = "10222",
                    DepartmentName = "儿科 陈凯"
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
            MainSession.Users = FileReader.DeserializeFile<List<SanyaLogin>>("Login.json");
            foreach(var user in MainSession.Users)
            {
                Task.Factory.StartNew(async () =>
                {
                    var loginController = HttpServiceController.GetService<LoginController>();
                    await loginController.LoginAsync(user);

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

            var loginData = new SanyaLogin()
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
                        Age = user.Age.NotNullString(),
                        GoodsDetailId = template.GoodsDetailId,
                        GoodsId = template.GoodsId,
                        IcCardNo = user.IcCard,
                        IdCardNo = user.IdCard,
                        OrgCode = template.OrgCode,
                        ProvideAddress = template.ProvideAddress,
                        ServiceId = template.ServiceId,
                        SubscribeType = template.SubscribeType,
                        TimeStr = template.TimeStr,
                    };

                    orderList.Add(order);
                }

                Task.Factory.StartNew(() => StartOneOrder(user.UserName, orderList));
            }
        }

        #endregion Appoint

        #region Cancel
        #endregion Cancel

        #region Hospital Dept

        private void OnSelectedDepartmentChanged()
        {
            var selectedDept = SelectedDepartment as SanyaHospital;
            MainSession.PlatformSession.AddOrUpdate(Constants.DeptName, selectedDept.DepartmentName);
            MainSession.PlatformSession.AddOrUpdate(Constants.HospitalName, selectedDept.HospitalName);
            MainSession.PlatformSession.AddOrUpdate(Constants.DeptId, selectedDept.DepartmentId);
            MainSession.PlatformSession.AddOrUpdate(Constants.HospitalId, selectedDept.HospitalId);
            MainSession.PlatformSession.AddOrUpdate(Constants.OrgCode, selectedDept.OrgCode);
            MainSession.PlatformSession.AddOrUpdate(Constants.ZoneCode, selectedDept.ZoneCode);
            MainSession.PlatformSession.AddOrUpdate(Constants.ZoneName, selectedDept.ZoneName);
            MainSession.PlatformSession.AddOrUpdate(Constants.AppCode, selectedDept.AppCode);
            MainSession.PlatformSession.AddOrUpdate(Constants.SubscribeType, selectedDept.SubscribeType);

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
