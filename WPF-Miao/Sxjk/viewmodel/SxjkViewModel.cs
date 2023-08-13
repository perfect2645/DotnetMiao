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
using Sxjk.appointment;
using Sxjk.login;
using Sxjk.search;
using Sxjk.session;
using Utils;
using Utils.datetime;
using Utils.file;
using Utils.number;
using Utils.stringBuilder;
using System.Threading;
using Sxjk.tools;

namespace Sxjk.viewmodel
{
    internal class SxjkViewModel : OnTimeViewModel
    {
        #region Properties

        public ICommand ManualCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand CancelOneCommand { get; set; }
        
        public ICommand RefreshHistoryCommand { get; set; }
        public ICommand BindUserCommand { get; set; }

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

        private string _bindIdCard;
        public string BindIdCard
        {
            get { return _bindIdCard; }
            set
            {
                _bindIdCard = value;
                NotifyUI(() => BindIdCard);
            }
        }

        private readonly object OrderLock = new object();

        private SearchController _searchController;

        #endregion Properties

        #region Constructor

        public SxjkViewModel(LogPanel logPanel) : base(logPanel)
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
            StartTime = DateTime.Today.AddHours(7).AddMinutes(59).AddSeconds(55);

            DateList = new List<DspVal>();

            Departments = new List<HospitalDept>
            {
                new SxjkHospital
                {
                    CityCode = "140500000000",
                    CityName = "晋城市",
                    StationCode = "140502120100",
                    StationName = "钟家庄社区卫生服务中心",
                    DepartmentName = "九价疫苗预约",
                },
                new SxjkHospital
                {
                    CityCode = "140500000000",
                    CityName = "晋城市",
                    StationCode = "140502020200",
                    StationName = "北街社区卫生服务中心",
                    DepartmentName = "九价疫苗预约",
                },
                new SxjkHospital
                {
                    CityCode = "140200000000",
                    CityName = "大同市",
                    StationCode = "140213140400",
                    StationName = "大同市平城区大同市第一人民医院防保科",
                    DepartmentName = "九价疫苗预约",
                },
                new SxjkHospital
                {
                    CityCode = "140400000000",
                    CityName = "长治市",
                    StationCode = "10001",
                    StationName = "",
                    DepartmentName = "九价疫苗预约",
                    DepartmentId = "101",
                },
                new SxjkHospital
                {
                    CityCode = "140800000000",
                    CityName = "运城市",
                    StationCode = "140802014100",
                    StationName = "高家垣社区卫生服务站成人门诊",
                    DepartmentName = "九价疫苗预约",
                },
            };

            SelectedDepartment = Departments.FirstOrDefault();
            _searchController = new SearchController();
        }

        private void InitCommands()
        {
            LoginCommand = new RelayCommand(ExecuteLogin);
            ManualCommand = new RelayCommand(ExecuteManual);
            RefreshHistoryCommand = new RelayCommand(RefreshHistory);
            BindUserCommand = new RelayCommand(BindUser);

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
            MainSession.Users = FileReader.DeserializeFile<List<SxjkLogin>>("Login.json");
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
            var loginData = new SxjkLogin()
            {
                
            };

            MainSession.Users.Add(loginData);

            ClearLoginData();
        }

        private void ClearLoginData()
        {

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
                        Thread.Sleep(2000);
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
                        LoginUser_name = user.LoginUserName,
                        Child_code = user.UserId,
                        City_code = template.City_code,
                        Price_id = template.Price_id,
                        Reservation_begin_time = template.Reservation_begin_time,
                        Reservation_date = template.Reservation_date,
                        Reservation_end_time = template.Reservation_end_time,
                        Station_code = template.Station_code,
                        Vaccine_code = template.Vaccine_code
                    };

                    orderList.Add(order);
                }

                Task.Factory.StartNew(() => StartOneOrder(user.UserName, orderList));
            }
        }

        #endregion Appoint

        #region History

        private void RefreshHistory()
        {
            var historyController = HttpServiceController.GetService<OrderHistoryController>();

            foreach(var user in MainSession.Users)
            {
                historyController.GetOrderHistoryAsync(user);
            }
        }

        #endregion History

        #region Bind User

        private void BindUser()
        {
            var bindUserController = HttpServiceController.GetService<BindUserController>();
            var defaultUser = MainSession.Users.FirstOrDefault();
            defaultUser.Idcard = BindIdCard;
            bindUserController.BindUserAsync(defaultUser);

        }

        #endregion Bind User

        #region Cancel
        #endregion Cancel

        #region Hospital Dept

        private void OnSelectedDepartmentChanged()
        {
            var selectedDept = SelectedDepartment as SxjkHospital;
            MainSession.PlatformSession.AddOrUpdate(Constants.CityCode, selectedDept.CityCode);
            MainSession.PlatformSession.AddOrUpdate(Constants.StationCode, selectedDept.StationCode);
            MainSession.PlatformSession.AddOrUpdate(Constants.DeptName, selectedDept.DepartmentName);
            MainSession.PlatformSession.AddOrUpdate(Constants.HospitalName, selectedDept.HospitalName);
            MainSession.PlatformSession.AddOrUpdate(Constants.DeptId, selectedDept.DepartmentId);
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
                    var userController = HttpServiceController.GetService<UserController>();
                    await userController.GetUserAsync(user);
                });
            }
        }

        #endregion Resession
    }
}
