using Base.model;
using Base.viewmodel.status;
using Base.viewModel;
using Base.viewModel.hospital;
using Xihongmen.appointment;
using Xihongmen.login;
using Xihongmen.search;
using Xihongmen.session;
using CommunityToolkit.Mvvm.Input;
using CoreControl.LogConsole;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Utils;
using Utils.stringBuilder;

namespace Xihongmen.viewmodel
{
    internal class XhmViewModel : OnTimeViewModel
    {
        #region Properties

        public ICommand SearchCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand YzmCommand { get; set; }

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

        private string _userPhone;
        public string UserPhone
        {
            get { return _userPhone; }
            set
            {
                _userPhone = value;
                MainSession.Phone = value;
                NotifyUI(() => UserPhone);
            }
        }

        private string _loginYzm;
        public string LoginYzm
        {
            get { return _loginYzm; }
            set
            {
                _loginYzm = value;
                MainSession.PlatformSession.AddOrUpdate("LoginYzm", value);
                NotifyUI(() => LoginYzm);
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

        private string _scheduleId;
        public string ScheduleId
        {
            get { return _scheduleId; }
            set
            {
                _scheduleId = value;
                NotifyUI(() => ScheduleId);
            }
        }

        private readonly object OrderLock = new object();

        private SearchController _searchController;

        #endregion Properties

        #region Constructor

        public XhmViewModel(LogPanel logPanel) : base(logPanel)
        {
            InitCommands();
            InitStaticData();
            MainSession.PrintLogEvent = PrintLogEvent;

            TestData();
        }

        private void TestData()
        {
            Interval = 800;
            MainSession.PlatformSession.AddOrUpdate("StartTime", StartTime);
            UserPhone = "13940897525";
            Token = "76db955e18f11a00f24e3807ec139085";

            StartTime = DateTime.Now.AddSeconds(10);
        }

        private void InitStaticData()
        {
            StartTime = new DateTime(2022, 12, 14, 8, 59, 40);

            SetDateList();

            Departments = new List<HospitalDept>
            {
                new XhmHospital
                {
                    HospitalId = "yiyuan.dabannet.cn",
                    HospitalName = "北京西红门医院",
                    DepartmentId = "10",
                    DepartmentName = "九价HPV疫苗",
                },
                new XhmHospital
                {
                    HospitalId = "yiyuan.dabannet.cn",
                    HospitalName = "北京西红门医院",
                    DepartmentId = "9",
                    DepartmentName = "四价HPV疫苗",
                },
                new XhmHospital
                {
                    HospitalId = "yiyuan.dabannet.cn",
                    HospitalName = "北京西红门医院",
                    DepartmentId = "7",
                    DepartmentName = "流感疫苗",
                },
            };

            SelectedDepartment = Departments.FirstOrDefault();
        }

        private void SetDateList()
        {
            DateList = new List<DspVal>();

        }

        private void InitCommands()
        {
            LoginCommand = new AsyncRelayCommand(ExecuteLogin);
            SearchCommand = new RelayCommand(ExecuteManual);
            CancelCommand = new RelayCommand(ExecuteCancel);
            YzmCommand = new RelayCommand(SendYzm);

            MainSession.ReSessionEvent.Subscribe(OnResession);
            MainSession.ScheduleEvent.Subscribe(OnSchedule);

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
            StopIntervalTimer();
        }

        #endregion Status Control

        #region Login

        private async Task ExecuteLogin()
        {
            if (StringUtil.AnyEmpty(UserPhone, LoginYzm))
            {
                MainSession.PrintLogEvent.Publish(this, "请填写用户手机和验证码");
                return;
            }
            var loginController = HttpServiceController.GetService<LoginController>();
            Token = await loginController.LoginAsync(UserPhone, LoginYzm);
        }

        #endregion Login

        #region Send Yzm

        private void SendYzm()
        {
            if (string.IsNullOrEmpty(UserPhone))
            {
                PrintLog("请输入手机号");
                return;
            }
            var yzmController = HttpServiceController.GetService<YzmController>();
            yzmController.SendYzmAsync(UserPhone);
        }

        #endregion Send Yzm

        #region AutoRun

        protected override void StartAutoRun()
        {
            Task.Factory.StartNew(async () => {
                try
                {
                    _searchController = new SearchController();
                    _searchController.GetUser();
                    MainSession.PlatformSession.AddOrUpdate("StartTime", StartTime);
                    StartIntervalTimer();
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
                    _searchController.SearchAsync();
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

        int cnt = 0;
        private void OnSchedule(object? sender, ScheduleEventArgs e)
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
                var preOrderController = HttpServiceController.GetService<YuyueController>();
                var preContent = new YuyueContent();
                preOrderController.BuildHeaders(preContent);

                for(int i = 0; i < 10; i ++)
                {
                    foreach (var order in orderList)
                    {
                        lock (OrderLock)
                        {
                            if (MainSession.GetStatus() == MiaoProgress.AppointEnd)
                            {
                                return;
                            }
                            lock (OrderLock)
                            {
                                var content = new YuyueContent(order);
                                preOrderController.PreOrder(content);
                            }
                        }
                    }
                }

                /* 防止一个人约到多个号，不能用异步
                foreach (var order in orderList)
                {
                    var content = new PreOrderContent(order);
                    preOrderController.PreOrderAsync(content);
                    order.IntervalOnTime.StartIntervalOntime(() =>
                    {
                        Task.Factory.StartNew(() => preOrderController.PreOrderAsync(content));
                    });
                }
                */
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
                    _searchController = new SearchController();
                    if (StringUtil.AnyEmpty(UserPhone, LoginYzm))
                    {
                        MainSession.PrintLogEvent.Publish(this, "请填写用户手机和密码");
                        return;
                    }
                    MainSession.PrintLogEvent.Publish(this, $"手动预约");
                    MainSession.PlatformSession.AddOrUpdate("StartTime", StartTime);

                    await ExecuteLogin();

                    if (StringUtil.NotEmpty(ScheduleId))
                    {
                        DirectlyOrder(ScheduleId);
                        return;
                    }
                    if (MainSession.GetStatus() != MiaoProgress.ReadyForSearch)
                    {
                        return;
                    }
                    _searchController.SearchAsync();
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
            var doctorId = MainSession.PlatformSession.GetString(Constants.DoctorId);
            var hospitalId = MainSession.PlatformSession.GetString(Constants.HospitalId);
            var userInfo = MainSession.UserSession.Users.FirstOrDefault(x => 
                (x.Value as Dictionary<string, object>)?.GetString("isDefault") == "1").Value as Dictionary<string, object>;

            var userId = userInfo.GetString(Constants.UserId);
            var userName = userInfo.GetString("familyName");
            var phone = userInfo.GetString("familyPhone");

            var order = new Order();
            order.UserId = userId;


            var preOrderController = HttpServiceController.GetService<YuyueController>();
            var content = new YuyueContent(order);
            preOrderController.BuildHeaders(content);
            Task.Factory.StartNew(() =>
            {
                preOrderController.Exchange(content);
            });
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
            var selectedDept = SelectedDepartment as XhmHospital;
            MainSession.PlatformSession.AddOrUpdate(Constants.DeptId, selectedDept.DepartmentId);

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
