using Base.model;
using Base.viewmodel.status;
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
using Utils;
using Utils.datetime;
using Utils.file;
using Utils.stringBuilder;
using Xihongmen.appointment;
using Xihongmen.login;
using Xihongmen.search;
using Xihongmen.session;

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

            LoginFromConfig();
        }

        private void TestData()
        {
            Interval = 800;
            MainSession.PlatformSession.AddOrUpdate("StartTime", StartTime);
            StartTime = DateTime.Now.AddSeconds(10);
        }

        private void InitStaticData()
        {
            StartTime = new DateTime(2023, 1, 25, 16, 59, 00);

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
            var targetDates = DateTimeUtil.GetFutureDays(DayOfWeek.Monday, DayOfWeek.Wednesday);
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

        private void LoginFromConfig()
        {
            MainSession.Users = FileReader.DeserializeFile<List<XhmLogin>>("Login.json");
            foreach (var user in MainSession.Users)
            {
                var userController = HttpServiceController.GetService<UserController>();
                userController.GetUserAsync(user);
            }

            MainSession.InitSession();
        }

        private async Task ExecuteLogin()
        {
            if (StringUtil.AnyEmpty(UserPhone, LoginYzm))
            {
                MainSession.PrintLogEvent.Publish(this, "请填写用户手机和验证码");
                return;
            }
            var loginController = HttpServiceController.GetService<LoginController>();
            var token = await loginController.LoginAsync(UserPhone, LoginYzm);

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
                    MainSession.SetStatus(MiaoProgress.ReadyForSearch);
                    MainSession.InitSession();
                    _searchController = new SearchController();

                    foreach(var user in MainSession.Users)
                    {
                        _searchController.TryGetCookie(user);
                        StartIntervalTimer();
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
            lock (OrderLock)
            {
                var orderTemplateList = e.OrderList;
                Task.Factory.StartNew(() => OnAppointment(orderTemplateList));
            }
        }

        private void OnAppointment(List<Order> orderTemplateList)
        {
            try
            {
                var preContent = new YuyueContent();

                foreach (var user in MainSession.Users)
                {
                    var orderList = new List<Order>();
                    var userNameEncode = UnicodeConverter.Encode(user.UserName, true);
                    foreach (var template in orderTemplateList)
                    {
                        var order = new Order
                        {
                            Date = template.Date,
                            TimeType = template.TimeType,
                            Token = user.Token,
                            TypeId = template.TypeId,
                            TypeTitle = template.TypeTitle,
                            User = user,
                            UserId = user.UserId,
                            UserName = user.UserName,
                            UserNameEncode = userNameEncode
                        };

                        orderList.Add(order);
                    }

                    Task.Factory.StartNew(() => StartOneOrder(user.UserName, orderList));
                }
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
                        var controller = MainSession.AppointSession.GetController($"{userName}|{order.Date}");
                        isSuccess = controller.YuyueAsync(order);
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

        private void ExecuteManual()
        {

        }

        private void DirectlyOrder(string scheduleId)
        {

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
