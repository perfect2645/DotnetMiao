using Base.model;
using Base.viewmodel.status;
using Base.viewModel;
using Base.viewModel.hospital;
using CommunityToolkit.Mvvm.Input;
using CoreControl.LogConsole;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
using suiyang.appointment;
using suiyang.login;
using suiyang.search;
using suiyang.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Utils;
using Utils.stringBuilder;

namespace suiyang.viewmodel
{
    internal class SuiyangViewModel : OnTimeViewModel
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

        private string _auth;
        public string Auth
        {
            get { return _auth; }
            set
            {
                _auth = value;
                MainSession.Auth = value;
                NotifyUI(() => Auth);
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

        private SearchController _searchController = new SearchController();

        #endregion Properties

        #region Constructor

        public SuiyangViewModel(LogPanel logPanel) : base(logPanel)
        {
            InitCommands();
            InitStaticData();
            MainSession.PrintLogEvent = PrintLogEvent;

            TestData();
        }

        private void TestData()
        {
            Interval = 800;
            StartTime = DateTime.Now.AddSeconds(20);
            MainSession.PlatformSession.AddOrUpdate("StartTime", StartTime);
            Auth = "Bearer e5c40e93-c261-40e7-ae47-c61b4c1fe09b";
        }

        private void InitStaticData()
        {
            StartTime = new DateTime(2022, 11, 21, 7, 59, 56);
            MainSession.PlatformSession.AddOrUpdate("StartTime", StartTime);

            Departments = new List<HospitalDept>
            {
                new SuiyangHospital
                {
                    HospitalId = "514966",
                    HospitalName = "绥阳县妇幼保健院",
                    DepartmentId = "F",
                    DepartmentName = "九价宫颈癌疫苗",
                },
                new SuiyangHospital
                {
                    HospitalId = "514966",
                    HospitalName = "绥阳县妇幼保健院",
                    DepartmentId = "D",
                    DepartmentName = "预约分娩",
                },
            };

            SelectedDepartment = Departments.FirstOrDefault();
        }

        private void InitCommands()
        {
            SearchCommand = new RelayCommand(ExecuteManual);
            CancelCommand = new RelayCommand(ExecuteCancel);

            MainSession.AppointEvent.Subscribe(OnSchedule);

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

        #region AutoRun

        protected override void StartAutoRun()
        {
            Task.Factory.StartNew(() => {
                try
                {
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

        private void OnSchedule(object? sender, AppointEventArgs e)
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
                //var preOrderController = HttpServiceController.GetService<PreOrderController>();
                //var preContent = new PreOrderContent();
                //preOrderController.BuildHeaders(preContent);

                //foreach (var order in orderList)
                //{
                //    var content = new PreOrderContent(order);
                //    preOrderController.PreOrderAsync(content);
                //    order.IntervalOnTime.StartIntervalOntime(() =>
                //    {
                //        Task.Factory.StartNew(() => preOrderController.PreOrderAsync(content));
                //    });
                //}
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        private void ExecuteManual()
        {
            Task.Factory.StartNew(() => {
                try
                {
                    if (StringUtil.AnyEmpty(Auth))
                    {
                        MainSession.PrintLogEvent.Publish(this, "请填写用户Auth -Bearer");
                        return;
                    }
                    MainSession.PrintLogEvent.Publish(this, $"手动预约");
                    MainSession.PlatformSession.AddOrUpdate("StartTime", StartTime);

                    if (StringUtil.NotEmpty(ScheduleId))
                    {
                        DirectlyOrder(ScheduleId);
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
            //var doctorId = MainSession.PlatformSession.GetString(Constants.DoctorId);
            //var hospitalId = MainSession.PlatformSession.GetString(Constants.HospitalId);
            //var userInfo = MainSession.UserSession.Users.FirstOrDefault(x =>
            //    (x.Value as Dictionary<string, object>)?.GetString("isDefault") == "1").Value as Dictionary<string, object>;

            //var userId = userInfo.GetString(Constants.UserID);
            //var familyId = userInfo.GetString(Constants.FamilyID);
            //var userName = userInfo.GetString("familyName");
            //var phone = userInfo.GetString("familyPhone");

            //var order = new Order();
            //order.ScheduleId = scheduleId;
            //order.DoctorId = doctorId;
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
            var selectedDept = SelectedDepartment as SuiyangHospital;
            MainSession.PlatformSession.AddOrUpdate(Constants.HospitalId, selectedDept.HospitalId);
            //MainSession.PlatformSession.AddOrUpdate(Constants.DoctorId, selectedDept.DepartmentId);

            Log(selectedDept.ToLogString());
        }

        #endregion Hospital Dept

        #region ReSession

        protected override void ReSession()
        {
            Log("ression invoke");
        }

        #endregion Resession
    }
}
