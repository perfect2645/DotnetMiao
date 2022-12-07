using Base.model;
using Base.viewmodel.status;
using Base.viewModel;
using Base.viewModel.hospital;
using CommunityToolkit.Mvvm.Input;
using CoreControl.LogConsole;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
using gaoxin.appointment;
using gaoxin.search;
using gaoxin.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Utils;
using Utils.datetime;
using Utils.stringBuilder;
using Utils.file;

namespace gaoxin.viewmodel
{
    internal class GaoxinViewModel : OnTimeViewModel
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

        private string _token;
        public string Token
        {
            get { return _token; }
            set
            {
                _token = value;
                MainSession.Token = value;
                NotifyUI(() => Token);
            }
        }

        private string _disparkId;
        public string DisparkId
        {
            get { return _disparkId; }
            set
            {
                _disparkId = value;
                MainSession.DisparkId = value;
                NotifyUI(() => DisparkId);
            }
        }

        private string _orderToken;
        public string OrderToken
        {
            get { return _orderToken; }
            set
            {
                _orderToken = value;
                MainSession.OrderToken = value;
                NotifyUI(() => OrderToken);
            }
        }

        private readonly object OrderLock = new object();

        private SearchController _searchController;

        #endregion Properties

        #region Constructor

        public GaoxinViewModel(LogPanel logPanel) : base(logPanel)
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
            StartTime = DateTime.Now.AddSeconds(20);
        }

        private void InitStaticData()
        {
            StartTime = new DateTime(2022, 12, 7, 8, 59, 58);

            DisparkId = "b64b468d8131681e4c9dd271d573eb79";

            DateList = new List<DspVal>
            {
                new DspVal(DateTime.Today.ToString("yyyy-MM-dd")),
            };

            MainSession.PlatformSession.AddOrUpdate("DateList", DateList);

            Departments = new List<HospitalDept>
            {
                new GaoxinHospital
                {
                    HospitalId = "12510109450812372N",
                    HospitalName = "成都高新区中和社区卫生服务中心",
                    DepartmentId = DisparkId,
                    DepartmentName = "九价HPV疫苗",
                },
            };

            SelectedDepartment = Departments.FirstOrDefault();
            _searchController = new SearchController();
            MainSession.InitSession();
        }

        private void InitCommands()
        {
            LoginCommand = new RelayCommand(ExecuteLogin);
            SearchCommand = new RelayCommand(ExecuteManual);
            CancelCommand = new RelayCommand(ExecuteCancel);

            MainSession.ReSessionEvent.Subscribe(OnResession);
            MainSession.OrderEvent.Subscribe(OnOrder);

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

        private List<GaoxinLogin> _gaoxinLogins = new List<GaoxinLogin>();

        private void LoginFromConfig()
        {
            if (StringUtil.AnyEmpty(DisparkId))
            {
                Log("请检查DisparkId参数");
                return;
            }
            _gaoxinLogins = FileReader.DeserializeFile<List<GaoxinLogin>>("Login.json");

            foreach(var gaoxinLogin in _gaoxinLogins)
            {
                Task.Factory.StartNew(async () =>
                {
                    await _searchController.GetUserInfoAsync(gaoxinLogin);
                });
            }
        }

        private void ExecuteLogin()
        {
            if (StringUtil.AnyEmpty(Token, OrderToken, DisparkId))
            {
                Log("请检查参数");
                return;
            }

            var loginData = new GaoxinLogin()
            {
                OrderToken = OrderToken,
                Token = Token,
            };

            Task.Factory.StartNew(async () =>
            {
                await _searchController.GetUserInfoAsync(loginData);
            });

            ClearLoginData();
        }

        private void ClearLoginData()
        {
            Token = string.Empty;
            OrderToken = string.Empty;
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

        protected override void AutoRun()
        {
            Task.Factory.StartNew(() => {
                try
                {
                    //var order = MainSession.Order;

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

        private void OnOrder(object? sender, OrderEventArgs e)
        {
            lock (OrderLock)
            {
                var orderList = e.OrderList;
                Task.Factory.StartNew(() => OnAppointment(orderList));
            }
        }

        private void OnAppointment(List<Order> orderList)
        {
            try
            {
                foreach (var order in orderList)
                {
                    //var yuyueController = MainSession.AppointSession.GetController(order.AppointDate);
                    //yuyueController.StartInterval(order);
                }
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        private void ExecuteManual()
        {
            Task.Factory.StartNew(async () => {
                try
                {

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
            var selectedDept = SelectedDepartment as GaoxinHospital;
            MainSession.PlatformSession.AddOrUpdate(Constants.DeptId, selectedDept.DepartmentId);
            MainSession.PlatformSession.AddOrUpdate(Constants.DeptName, selectedDept.DepartmentName);
            MainSession.PlatformSession.AddOrUpdate(Constants.HospitalName, selectedDept.HospitalName);

            Log(selectedDept.ToLogString());
        }

        #endregion Hospital Dept

        #region ReSession

        protected override void ReSession()
        {
            Log("ression invoke");
            Task.Factory.StartNew(() => ExecuteLogin());
        }

        private void OnResession(object? sender, ResessionEventArgs e)
        {
            ReSession();
        }

        #endregion Resession
    }
}
