using Base.model;
using Base.viewmodel.status;
using Base.viewModel;
using Base.viewModel.hospital;
using CommunityToolkit.Mvvm.Input;
using CoreControl.LogConsole;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
using Ych.appointment;
using Ych.search;
using Ych.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Utils;
using Utils.datetime;
using Utils.stringBuilder;

namespace Ych.viewmodel
{
    internal class YchViewModel : OnTimeViewModel
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

        private string _auth;
        public string Auth
        {
            get { return _auth; }
            set
            {
                if (value != _auth)
                {
                    _auth = value;
                    MainSession.PlatformSession.AddOrUpdate(Constants.Authorization, value);
                    NotifyUI(() => Auth);
                }
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

        public YchViewModel(LogPanel logPanel) : base(logPanel)
        {
            InitCommands();
            InitStaticData();
            MainSession.PrintLogEvent = PrintLogEvent;

            TestData();
        }

        private void TestData()
        {
            Interval = 200;

            Auth = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJleHAiOjE2NzE2MTE5NzUsInVzZXJuYW1lIjoib1BDbHM1aDJNV1pXTHU4YXh0MFFfOXlMRk5IOCJ9.XvFuDxjsN9WpcWjo8CSvDVkqSyhb0mD4g7f5xiZ4j9s";

            StartTime = DateTime.Now.AddSeconds(20);
            MainSession.StartTime = StartTime;
        }

        private void InitStaticData()
        {
            StartTime = new DateTime(2022, 11, 25, 8, 50, 56);
            MainSession.StartTime = StartTime;

            DateList = new List<DspVal>
            {
                new DspVal(DateTimeUtil.GetTomorrow()),
                new DspVal(DateTimeUtil.GetTargetDate(2)),
            };

            MainSession.PlatformSession.AddOrUpdate("DateList", DateList);

            Departments = new List<HospitalDept>
            {
                new YchHospital
                {
                    HospitalId = "http://yy.test.shinegosoft.com.cn",
                    HospitalName = "金银湖街家庭医生服务平台",
                    DepartmentId = "18",
                    DepartmentName = "九价",
                },
                new YchHospital
                {
                    HospitalId = "http://yy.test.shinegosoft.com.cn",
                    HospitalName = "金银湖街家庭医生服务平台",
                    DepartmentId = "9",
                    DepartmentName = "成人疫苗",
                },
                new YchHospital
                {
                    HospitalId = "http://yy.test.shinegosoft.com.cn",
                    HospitalName = "金银湖街家庭医生服务平台",
                    DepartmentId = "9",
                    DepartmentName = "成人疫苗",
                },
            };

            SelectedDepartment = Departments.FirstOrDefault();
            MainSession.InitSession();
            _searchController = new SearchController();
        }

        private void InitCommands()
        {
            LoginCommand = new AsyncRelayCommand(ExecuteLogin);
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
            StopIntervalTimer();
        }

        #endregion Status Control

        #region Login

        private async Task ExecuteLogin()
        {
            await _searchController.GetUserInfo();
        }

        #endregion Login

        #region AutoRun

        protected override void StartAutoRun()
        {
            Task.Factory.StartNew(async () => {
                try
                {
                    _searchController = new SearchController();
                    await _searchController.GetUserInfo();
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
                    _searchController.GetMiaoAsync();
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
                for (int i = 0; i < 10; i ++)
                {
                    foreach (var order in orderList)
                    {
                        if (MainSession.GetStatus() == MiaoProgress.AppointEnd)
                        {
                            return;
                        }
                        var key = $"{order.ReservationDate}{order.ReservationTime}";
                        var yuyueController = MainSession.AppointSession.GetController(key);
                        yuyueController.YuyueAsync(order);
                    }
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
                    _searchController = new SearchController();
                    if (StringUtil.AnyEmpty(Auth))
                    {
                        MainSession.PrintLogEvent.Publish(this, "请填写Auth");
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

                    _searchController.GetMiaoAsync();
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
            //order.Hospitalid = hospitalId;
            //order.UserId = userId;
            //order.FamilyId = familyId;
            //order.UserName = userName;
            //order.UserPhone = phone;

            //var preOrderController = HttpServiceController.GetService<PreOrderController>();
            //var content = new PreOrderContent(order);
            //preOrderController.BuildHeaders(content);
            //Task.Factory.StartNew(() =>
            //{
            //    preOrderController.Exchange(content);
            //});
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
            var selectedDept = SelectedDepartment as YchHospital;
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
