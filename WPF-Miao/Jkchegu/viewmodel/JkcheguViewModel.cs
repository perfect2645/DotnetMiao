using Base.Events;
using Base.model;
using Base.viewModel;
using Base.viewModel.hospital;
using CommunityToolkit.Mvvm.Input;
using CoreControl.LogConsole;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
using Jkchegu.appointment;
using Jkchegu.search;
using Jkchegu.search.yzm;
using Jkchegu.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Utils;
using Utils.datetime;

namespace Jkchegu.viewmodel
{
    internal class JkcheguViewModel : OnTimeViewModel
    {
        #region Properties

        public ICommand SearchCommand { get; set; }

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
                JkSession.MiaoSession.AddOrUpdate("Date", value.Value);
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
                JkSession.MiaoSession.AddOrUpdate("Time", value.Value);
                NotifyUI(() => SelectedTime);
            }
        }

        private string _etid;
        public string Etid
        {
            get { return _etid; }
            set
            {
                _etid = value;
                JkSession.MiaoSession.AddOrUpdate("Etid", value);
                NotifyUI(() => Etid);
            }
        }

        private readonly object OrderLock = new object();
        private Queue<Order> PendingOrders = new Queue<Order>();

        #endregion Properties

        #region Constructor

        public JkcheguViewModel(LogPanel logPanel) : base(logPanel)
        {
            InitCommands();
            InitStaticData();
            JkSession.PrintLogEvent = PrintLogEvent;

            TestData();
        }

        private void TestData()
        {
            //Cookie = "JSESSIONID=20745158995B109C28B5B97AF021C43F";
            //Etid = "7bf4400434ea4e80a6dfb331f6f6a077";
            Cookie = "JSESSIONID=41E0D772413CF7A20DB68C4BD8442539";
            Etid = "030b18b61121492bb4b57b147230aa0d";

            JkSession.MiaoSession.AddOrUpdate("StartTime", DateTime.Now.AddSeconds(20));
        }

        private void InitStaticData()
        {
            //JkSession.MiaoSession.AddOrUpdate("StartTime", new DateTime(2022, 10, 25, 22, 3, 30));

            DateList = new List<DspVal>
            {
                new DspVal(DateTimeUtil.GetDayOfWeek(DayOfWeek.Saturday)),
                new DspVal(DateTimeUtil.GetDayOfNextWeek(DayOfWeek.Monday)),
                new DspVal(DateTimeUtil.GetDayOfNextWeek(DayOfWeek.Tuesday)),
                new DspVal(DateTimeUtil.GetDayOfNextWeek(DayOfWeek.Wednesday)),
                new DspVal(DateTimeUtil.GetDayOfNextWeek(DayOfWeek.Thursday)),
                new DspVal(DateTimeUtil.GetDayOfNextWeek(DayOfWeek.Friday)),
            };

            JkSession.PlatformSession.AddOrUpdate("PreDateList", DateList);

            TimeList = new List<DspVal>
            {
                new DspVal("8:30-9:00", "DATE1_COUNT"),
                new DspVal("9:00-9:30", "DATE2_COUNT"),
                new DspVal("9:30-10:00", "DATE3_COUNT"),
                new DspVal("10:00-10:30", "DATE4_COUNT"),
                new DspVal("10:30-11:00", "DATE5_COUNT"),
            };

            Departments = new List<HospitalDept>
            {
                new JkHospital
                {
                    HospitalId = "whsjjkfqzyjxmsqwsfwzx",
                    HospitalName = "新民社区服务站",
                    DepartmentId = "18",
                    DepartmentName = "九价宫颈癌疫苗（进口）",
                },
                //<option value="24">儿童乙肝疫苗（免费）</option>
                new JkHospital
                {
                    HospitalId = "whsjjkfqzyjxmsqwsfwzx",
                    HospitalName = "新民社区服务站",
                    DepartmentId = "24",
                    DepartmentName = "儿童乙肝疫苗（免费）",
                },
            };

            SelectedDepartment = Departments.FirstOrDefault();
        }

        private void InitCommands()
        {
            //SearchCommand = new RelayCommand(ExecuteSearchAsync);

            JkSession.AppointEvent.Subscribe(OnAppointment);

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


        #region AutoRun

        protected override void StartAutoRun()
        {
            try
            {
                JkSession.Cookie = Cookie;
                var searchController = HttpServiceController.GetService<SearchController>();
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

        protected override void AutoRun()
        {
        }

        #endregion AutoRun

        #region Appoint

        private void OnAppointment(object? sender, AppointEventArgs e)
        {
            lock (OrderLock)
            {
                var orderList = e.OrderList;
                ConsumeOrdersAsync(orderList);
            }
        }

        private void ConsumeOrdersAsync(List<Order> orderList)
        {
            try
            {
                var appointController = HttpServiceController.GetService<AppointController>();
                appointController.Yuyue(orderList);
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        private async void ExecuteAppointAsync()
        {
            try
            {
                JkSession.Cookie = Cookie;
                var searchController = HttpServiceController.GetService<SearchController>();
                //await searchController.SearchByDateAsync();
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

        #region Hospital Dept

        private void OnSelectedDepartmentChanged()
        {
            var selectedDept = SelectedDepartment as JkHospital;
            JkSession.PlatformSession.AddOrUpdate(Constants.HospitalId, selectedDept.HospitalId);
            JkSession.PlatformSession.AddOrUpdate(Constants.DeptId, selectedDept.DepartmentId);

            Log(selectedDept.ToLogString());

            //MainSession.BuildMiaoSession(MainSession.PlatformSesstion[Constant.DeptId].NotNullString());
        }

        #endregion Hospital Dept
    }
}
