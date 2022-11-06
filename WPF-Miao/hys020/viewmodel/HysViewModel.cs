using Base.model;
using Base.viewmodel.status;
using Base.viewModel;
using Base.viewModel.hospital;
using CommunityToolkit.Mvvm.Input;
using CoreControl.LogConsole;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
using hys020.appointment;
using hys020.search;
using hys020.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Utils;
using Utils.stringBuilder;

namespace hys020.viewmodel
{
    internal class HysViewModel : OnTimeViewModel
    {
        #region Properties

        public ICommand SearchCommand { get; set; }
        public ICommand AppointCommand { get; set; }
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

        private string _location;
        public string Location
        {
            get { return _location; }
            set
            {
                _location = value;
                SaveLocation(value);
                NotifyUI(() => Location);
            }
        }

        private readonly object OrderLock = new object();

        private SearchController _searchController;

        #endregion Properties

        #region Constructor

        public HysViewModel(LogPanel logPanel) : base(logPanel)
        {
            InitCommands();
            InitStaticData();
            InitControllers();

            //TestData();
        }

        private void TestData()
        {
            Cookie = "JSESSIONID=3404102DBF977C90A4C324EC147C64EC";
            Location = "http://www.hys020.com/home/yyghDorMobile?userId=E8A53E4EFFDE46F7B8B15A44230C2524&wechatid=gh_868741944de3&openid=o1_Liw34_q5dnOFrOCRDK7jQn5E0&Timestamp=iS3Q9CBrbezAF/LZ2y4gP1JlxuCYfsu8";
        }

        private void InitControllers()
        {
            _searchController = HttpServiceController.GetService<SearchController>();
        }

        private void InitStaticData()
        {
            StartTime = new DateTime(2022, 10, 31, 10, 59, 55);

            //盆底修复 deptid 42CB58972CD44CD9945775814C00CA41
            Departments = new List<HospitalDept>
            {
                new HysHospital
                {
                    HospitalId = "doctorYyghMobileDate",
                    HospitalName = "肇庆市鼎湖区",
                    DepartmentId = "E8A53E4EFFDE46F7B8B15A44230C2524",
                    DepartmentName = "九价宫颈癌疫苗（进口）",
                },
                //<option value="24">儿童乙肝疫苗（免费）</option>
                new HysHospital
                {
                    HospitalId = "doctorYyghMobileDate",
                    HospitalName = "肇庆市鼎湖区",
                    DepartmentId = "42CB58972CD44CD9945775814C00CA41",
                    DepartmentName = "盆底修复",
                },
            };

            SelectedDepartment = Departments.FirstOrDefault();
        }

        private void InitCommands()
        {
            MainSession.PrintLogEvent = PrintLogEvent;

            SearchCommand = new RelayCommand(OnSearchClick);
            AppointCommand = new RelayCommand(ExecuteAppointAsync);
            SelectedDepartmentChanged = new Action(OnSelectedDepartmentChanged);

            MainSession.AppointEvent.Subscribe(OnAppointment);
        }

        private void SaveLocation(string location)
        {
            if (string.IsNullOrEmpty(location))
            {
                return;
            }
            var queryDic = location.UrlToDic();
            MainSession.PlatformSession.AddOrUpdate(queryDic);
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
            StopAutoRunTimer();
            var attIdList = data as List<string>;
            if (attIdList == null)
            {
                return;
            }

            var miaoDetailController = HttpServiceController.GetService<MiaoDetailController>();

            foreach (var attId in attIdList)
            {
                Task.Factory.StartNew(() =>
                {
                    miaoDetailController.SearchMiaoDetailAsync(attId);
                });
            }
        }

        #endregion Status Control

        #region Search

        private void OnSearchClick()
        {
            Task.Factory.StartNew(async () =>
            {
                if (MainSession.MiaoStatus.MiaoProgress != MiaoProgress.GettingMiao)
                {
                    MainSession.SetStatus(MiaoProgress.GettingMiao);
                }
                await SearchAsync();
            });
        }

        private async Task SearchAsync()
        {
            MainSession.Cookie = Cookie;

            try
            {
                MainSession.SetStatus(MiaoProgress.Searching);
                await _searchController.SearchAsync();
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
                appointController.Yuyue(orderList);
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
            StartAutoRunTimer();
        }

        protected override void AutoRun()
        {
            Task.Factory.StartNew(async () =>
            {
                PrintLogEvent.Publish(this, "开始了");
                if (MainSession.MiaoStatus.MiaoProgress < MiaoProgress.GettingMiao)
                {
                    MainSession.SetStatus(MiaoProgress.GettingMiao);
                }
                var miaoSchedule = HttpServiceController.GetService<SearchController>();
                await miaoSchedule.SearchAsync();
            });
        }

        #endregion AutoRun

        #region Hospital Dept

        private void OnSelectedDepartmentChanged()
        {
            var selectedDept = SelectedDepartment as HysHospital;
            MainSession.PlatformSession.AddOrUpdate(Constants.HospitalId, selectedDept.HospitalId);
            MainSession.PlatformSession.AddOrUpdate(Constants.DeptId, selectedDept.DepartmentId);

            Log(selectedDept.ToLogString());

            //MainSession.BuildMiaoSession(MainSession.PlatformSesstion[Constant.DeptId].NotNullString());
        }

        #endregion Hospital Dept
    }
}
