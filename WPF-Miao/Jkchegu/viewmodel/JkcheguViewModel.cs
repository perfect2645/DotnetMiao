using Base.model;
using Base.viewModel;
using Base.viewModel.hospital;
using CommunityToolkit.Mvvm.Input;
using CoreControl.LogConsole;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
using Jkchegu.appointment;
using Jkchegu.search;
using Jkchegu.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Utils;
using Utils.datetime;
using Utils.stringBuilder;

namespace Jkchegu.viewmodel
{
    internal class JkcheguViewModel : OnTimeViewModel
    {
        #region Properties

        public ICommand SearchCommand { get; set; }
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
                JkSession.PlatformSession.AddOrUpdate("Etid", value);
                NotifyUI(() => Etid);
            }
        }

        private string _guid;
        public string GUID
        {
            get { return _guid; }
            set
            {
                _guid = value;
                JkSession.MiaoSession.AddOrUpdate("GUID", value);
                NotifyUI(() => GUID);
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
            Cookie = "JSESSIONID=39D78F5A25E3535594138199ABD21EF5";
            StartTime = DateTime.Now.AddSeconds(20);
            JkSession.MiaoSession.AddOrUpdate("StartTime", StartTime);
        }

        private void InitStaticData()
        {
            StartTime = new DateTime(2022, 11, 11, 7, 59, 56);
            JkSession.MiaoSession.AddOrUpdate("StartTime", StartTime);

            DateList = new List<DspVal>
            {
                new DspVal(DateTimeUtil.GetDayOfWeek(DayOfWeek.Monday)),
                new DspVal(DateTimeUtil.GetDayOfWeek(DayOfWeek.Tuesday)),
                new DspVal(DateTimeUtil.GetDayOfWeek(DayOfWeek.Wednesday)),
                new DspVal(DateTimeUtil.GetDayOfWeek(DayOfWeek.Thursday)),
                new DspVal(DateTimeUtil.GetDayOfWeek(DayOfWeek.Friday)),
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
                new JkHospital
                {
                    HospitalId = "whsjjkfqzyjxmsqwsfwzx",
                    HospitalName = "新民社区服务站",
                    DepartmentId = "17",
                    DepartmentName = "四价宫颈癌疫苗（进口）",
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
            SearchCommand = new RelayCommand(ExecuteManual);
            CancelCommand = new RelayCommand(ExecuteCancel);

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
            Task.Factory.StartNew(()=> {
                try
                {
                    JkSession.UpdateSession(Cookie);
                    JkSession.MiaoSession.AddOrUpdate("StartTime", StartTime);
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
            });
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
                var orderType = e.OrderType;
                if ("Exchange" == orderType)
                {
                    var user = new User(Etid, "转号");
                    user.Session = Cookie;
                    ExchangeOrdersAsync(user, orderList);
                    return;
                }
                ConsumeOrdersAsync(orderList);
            }
        }

        int count = 0;

        private void ConsumeOrdersAsync(List<Order> orderList)
        {
            try
            {
                count++;
                foreach (var user in JkSession.ActiveUsers())
                {
                    Task.Factory.StartNew(() =>
                    {
                        JkSession.PrintLogEvent.Publish(this, $"{count.ToString()} - {user.Name}");
                        user.Yuyue(orderList);
                    });
                }
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        private void ExchangeOrdersAsync(User user, List<Order> orderList)
        {
            try
            {
                var appointController = HttpServiceController.GetService<AppointController>();
                appointController.Exchange(user, orderList);
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
                    JkSession.PrintLogEvent.Publish(this, $"手动预约{SelectedDate.Display}");
                    JkSession.UpdateSession(Cookie);
                    JkSession.MiaoSession.AddOrUpdate("StartTime", StartTime);

                    if (StringUtil.NotEmpty(SelectedDate?.Value, SelectedTime?.Value))
                    {

                        DirectlyOrder(SelectedDate.Value, SelectedTime.Value);
                        return;
                    }

                    var dateCountController = HttpServiceController.GetService<DateCountController>();
                    Task.Factory.StartNew(() =>
                    {
                        var result = dateCountController.SearchBydate(SelectedDate.Value);
                        JkSession.PrintLogEvent.Publish(this, $"{result}");
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

        private void DirectlyOrder(string date, string time)
        {
            var dateCountController = HttpServiceController.GetService<DateCountController>();
            Task.Factory.StartNew(() =>
            {
                var result = dateCountController.SearchBydateTime(date, time);
                JkSession.PrintLogEvent.Publish(this, $"{result}");
            });
        }

        #endregion Appoint

        #region Cancel

        private async void ExecuteCancel()
        {
            try
            {
                var appointController = HttpServiceController.GetService<CancelController>();

                var order = new Order();
                await appointController.CancelAsync(order);
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
            var selectedDept = SelectedDepartment as JkHospital;
            JkSession.PlatformSession.AddOrUpdate(Constants.HospitalId, selectedDept.HospitalId);
            JkSession.PlatformSession.AddOrUpdate(Constants.DeptId, selectedDept.DepartmentId);

            Log(selectedDept.ToLogString());

            //MainSession.BuildMiaoSession(MainSession.PlatformSesstion[Constant.DeptId].NotNullString());
        }

        #endregion Hospital Dept

        #region Session

        protected override void ReSession()
        {
        }

        #endregion Session
    }
}
