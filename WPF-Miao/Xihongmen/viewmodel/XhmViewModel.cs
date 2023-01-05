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
using Utils.datetime;

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
                    DepartmentName = "九价hpv",
                },
                new XhmHospital
                {
                    HospitalId = "yiyuan.dabannet.cn",
                    HospitalName = "北京西红门医院",
                    DepartmentId = "9",
                    DepartmentName = "四价hpv",
                },
                new XhmHospital
                {
                    HospitalId = "yiyuan.dabannet.cn",
                    HospitalName = "北京西红门医院",
                    DepartmentId = "7",
                    DepartmentName = "流感疫苗（自费-3周岁以上）",
                },
            };

            SelectedDepartment = Departments.FirstOrDefault();
        }

        private void SetDateList()
        {
            DateList = new List<DspVal>();
            var targetDates = DateTimeUtil.GetFutureDays(DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday);
            foreach(var dateStr in targetDates)
            {
                var timestamp = DateTimeUtil.GetTimeStamp($"{dateStr} 08:00").Substring(0, 10);
                DateList.Add(new DspVal(dateStr, timestamp));
            }

            MainSession.PlatformSession.AddOrUpdate("DateList", DateList);
        }

        private void InitCommands()
        {
            LoginCommand = new AsyncRelayCommand(ExecuteLogin);
            SearchCommand = new RelayCommand(ExecuteManual);
            CancelCommand = new RelayCommand(ExecuteCancel);
            YzmCommand = new RelayCommand(SendYzm);

            MainSession.ReSessionEvent.Subscribe(OnResession);
            MainSession.ScheduleEvent.Subscribe(OnOrder);

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

            //MainSession.InitSession();
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
                    MainSession.InitSession();
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
                    //Task.Factory.StartNew(() =>
                    //{
                    //    _searchController.SearchAsync();
                    //});

                    Task.Factory.StartNew(() =>
                    {
                        _searchController.GetMiaoFromDate();
                    });
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

        private void OnOrder(object? sender, ScheduleEventArgs e)
        {
            if (MainSession.GetStatus() == MiaoProgress.AppointEnd)
            {
                return;
            }
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
                var preContent = new YuyueContent();

                foreach (var order in orderList)
                {
                    if (MainSession.GetStatus() == MiaoProgress.AppointEnd)
                    {
                        return;
                    }
                    var controller = MainSession.AppointSession.GetController(order.Date);
                    var content = new YuyueContent(order);
                    controller.BuildClientHeaders(content);
                    controller.AppointAsync(content);
                    return;
                    order.IntervalOnTime.StartIntervalOntime(() =>
                    {
                        Task.Factory.StartNew(() => controller.AppointAsync(content));
                    });
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
                    MainSession.InitSession();
                    _searchController = new SearchController();
                    if (StringUtil.AnyEmpty(Token))
                    {
                        MainSession.PrintLogEvent.Publish(this, "请先登录");
                        return;
                    }
                    MainSession.PrintLogEvent.Publish(this, $"手动预约");
                    MainSession.PlatformSession.AddOrUpdate("StartTime", StartTime);

                    if (StringUtil.NotEmpty(ScheduleId))
                    {
                        DirectlyOrder(ScheduleId);
                        return;
                    }
 
                    _searchController.GetMiaoFromDate();
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
            MainSession.PlatformSession.AddOrUpdate(Constants.DeptName, selectedDept.DepartmentName);

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
