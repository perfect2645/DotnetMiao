using Base.Events;
using Base.model;
using Base.viewmodel.status;
using Base.viewModel;
using CommunityToolkit.Mvvm.Input;
using CoreControl.LogConsole;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
using HuSheng.appointment;
using HuSheng.search;
using HuSheng.session;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Utils;

namespace HuSheng.viewmodel
{
    internal class HushengViewModel : OnTimeViewModel
    {
        #region Properties

        public ICommand SearchCommand { get; set; }
        public ICommand AppointCommand { get; set; }

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
                NotifyUI(() => SelectedTime);
            }
        }

        private readonly object OrderLock = new object();

        private VaccPlanController _searchController;

        #endregion Properties

        #region Constructor

        public HushengViewModel(LogPanel logPanel) : base(logPanel)
        {
            InitCommands();
            InitStaticData();
            InitControllers();

            //TestData();
        }

        private void TestData()
        {
            Cookie = "JSESSIONID=3404102DBF977C90A4C324EC147C64EC";
        }

        private void InitStaticData()
        {
            //StartTime = new DateTime(2022, 10, 7, 8, 57, 0);
            //MainSession.PlatformSession.AddOrUpdate("StartTime", new DateTime(2022, 10, 7, 8, 57, 0));

            DateList = new List<DspVal>
            {
                new DspVal("2022-10-10"),
                new DspVal("2022-10-11"),
                new DspVal("2022-10-12"),
                new DspVal("2022-10-13"),
                new DspVal("2022-10-14"),
            };

            TimeList = new List<DspVal>
            {
                new DspVal("09:01~09:30"),
            };
        }

        private void InitCommands()
        {
            MainSession.PrintLogEvent = PrintLogEvent;

            SearchCommand = new RelayCommand(OnSearchClick);
            AppointCommand = new RelayCommand(ExecuteAppointAsync);
            SelectedDepartmentChanged = new Action(OnSelectedDepartmentChanged);

            MainSession.AppointEvent.Subscribe(OnAppointment);
        }

        private void InitControllers()
        {
            _searchController = HttpServiceController.GetService<VaccPlanController>();
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
            var searchController = HttpServiceController.GetService<VaccPlanController>();
            searchController.SearchAsync();
        }

        protected override void OnSearchendAsync()
        {

        }

        protected override void OnMiaoGetAsync(object data)
        {
            StopIntervalTimer();
            var attIdList = data as List<string>;
            if (attIdList == null)
            {
                return;
            }

            //var miaoDetailController = HttpServiceController.GetService<MiaoDetailController>();

            //foreach (var attId in attIdList)
            //{
            //    Task.Factory.StartNew(() =>
            //    {
            //        miaoDetailController.SearchMiaoDetailAsync(attId);
            //    });
            //}
        }

        #endregion Status Control

        #region Search

        private void OnSearchClick()
        {
            MainSession.SetStatus(MiaoProgress.Searching);
        }

        private async Task SearchAsync()
        {
            MainSession.Cookie = Cookie;

            try
            {
                MainSession.SetStatus(MiaoProgress.Searching);
                //await _searchController.SearchAsync();
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

        #endregion Search

        #region Appoint

        private void OnAppointment(object? sender, AppointEventArgs e)
        {
            lock (OrderLock)
            {
                if (MainSession.MiaoStatus.MiaoProgress == MiaoProgress.AppointStart)
                {
                    lock (OrderLock)
                    {
                        var orderList = e.OrderList;
                        ConsumeOrdersAsync(orderList);
                    }
                }
            }
        }

        private void ConsumeOrdersAsync(List<Order> orderList)
        {
            try
            {
                var appointController = HttpServiceController.GetService<AppointController>();
                //appointController.Yuyue(orderList);
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        private void ExecuteAppointAsync()
        {
            try
            {
                MainSession.Cookie = Cookie;
                var appointController = HttpServiceController.GetService<AppointController>();
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

        #endregion Appoint

        #region AutoRun

        protected override void StartAutoRun()
        {
            StartIntervalTimer();
        }

        protected override void AutoRun()
        {
            Task.Factory.StartNew(() => {
                if (MainSession.MiaoStatus.MiaoProgress < MiaoProgress.GettingMiao)
                {
                    MainSession.SetStatus(MiaoProgress.GettingMiao);
                    PrintLogEvent.Publish(this, "开始查苗了");
                }
                var miaoSchedule = HttpServiceController.GetService<VaccPlanController>();
                return Task.CompletedTask;
            });
        }

        #endregion AutoRun

        #region Hospital Dept

        private void OnSelectedDepartmentChanged()
        {
            var selectedDept = SelectedDepartment as HushengHospital;
            MainSession.PlatformSession.AddOrUpdate(Constants.HospitalId, selectedDept.HospitalId);
            MainSession.PlatformSession.AddOrUpdate(Constants.DeptId, selectedDept.DepartmentId);

            Log(selectedDept.ToLogString());

            //MainSession.BuildMiaoSession(MainSession.PlatformSesstion[Constant.DeptId].NotNullString());
        }

        protected override void ReSession()
        {
        }

        #endregion Hospital Dept

    }
}
