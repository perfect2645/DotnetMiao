using Base.model;
using Base.viewModel;
using Base.viewModel.hospital;
using CommunityToolkit.Mvvm.Input;
using CoreControl.LogConsole;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
using Jingyang.appointment;
using Jingyang.search;
using Jingyang.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Utils;
using Utils.file;
using Utils.number;
using Utils.stringBuilder;

namespace Jingyang.viewmodel
{
    internal class JingyangViewModel : OnTimeViewModel
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

        private string _userId;
        public string UserId
        {
            get { return _userId; }
            set
            {
                _userId = value;
                NotifyUI(() => UserId);
            }
        }

        private string _fid;
        public string Fid
        {
            get { return _fid; }
            set
            {
                _fid = value;
                NotifyUI(() => Fid);
            }
        }

        private readonly object OrderLock = new object();

        private SearchController _searchController;

        private List<JingyangLogin> _jingyangLogins = new List<JingyangLogin>();

        #endregion Properties

        #region Constructor

        public JingyangViewModel(LogPanel logPanel) : base(logPanel)
        {
            Interval = 200;
            InitCommands();
            InitStaticData();
            MainSession.PrintLogEvent = PrintLogEvent;

            //TestData();
            LoginFromConfig();
        }

        private void TestData()
        {
            StartTime = DateTime.Now.AddSeconds(20);
        }

        private void InitStaticData()
        {
            StartTime = new DateTime(2022, 12, 26, 8, 59, 50);

            DateList = new List<DspVal>
            {
                new DspVal(DateTime.Today.ToString("yyyy-MM-dd")),
            };

            MainSession.PlatformSession.AddOrUpdate("DateList", DateList);

            Departments = new List<HospitalDept>
            {
                new JingyangHospital
                {
                    HospitalId = "cnfw.mailiku.com",
                    HospitalName = "德阳旌阳区城南社区",
                    DepartmentName = "九价HPV疫苗",
                    TimeIdList = new List<string> { "1987", "1992", "1997", "2002", "2007" }
                },
                new JingyangHospital
                {
                    HospitalId = "cnfw.mailiku.com",
                    HospitalName = "德阳旌阳区城南社区",
                    DepartmentName = "四价HPV疫苗",
                    TimeIdList = new List<string> { "2012", "2017", "2022", "2027", "2032" }
                },
            };

            SelectedDepartment = Departments.FirstOrDefault();
            _searchController = new SearchController();
        }

        private void InitCommands()
        {
            LoginCommand = new RelayCommand(ExecuteLogin);
            SearchCommand = new RelayCommand(ExecuteManual);
            CancelCommand = new RelayCommand(ExecuteCancel);

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

        private void LoginFromConfig()
        {
            _jingyangLogins = FileReader.DeserializeFile<List<JingyangLogin>>("Login.json");

            MainSession.Users = _jingyangLogins;

            MainSession.InitSession();
            StartAutoRun();
        }

        private void ExecuteLogin()
        {
            if (StringUtil.AnyEmpty(UserId, Fid))
            {
                Log("请检查参数");
                return;
            }

            var loginData = new JingyangLogin()
            {
                UserId = UserId,
                Fid = Fid,
                Yid = Fid,
                Cookie = Cookie,
            };

            _jingyangLogins.Add(loginData);

            ClearLoginData();
        }

        private void ClearLoginData()
        {
            Cookie = string.Empty;
            UserId = string.Empty;
            Fid = string.Empty;
        }

        #endregion Login

        #region AutoRun

        protected override void StartAutoRun()
        {
            Task.Factory.StartNew(() => {
                try
                {
                    BuildOrders();
                    StartOnTimeTimer();
                    StartReSessionTimer();
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

        private void BuildOrders()
        {
            MainSession.Orders = new Dictionary<string, List<Order>>();
            foreach(var user in _jingyangLogins)
            {
                var cookie = user.Cookie;
                var timeId = user.TimeId;
                if (!string.IsNullOrEmpty(timeId))
                {
                    Order order = BuildOneOrder(user, timeId);
                    MainSession.Orders.AddOrUpdate(cookie, new List<Order> { order });
                    continue;
                }

                var orderList = new List<Order>();
                foreach (var time in MainSession.TimeIdList)
                {
                    Order order = BuildOneOrder(user, time);
                    orderList.Add(order);
                }

                orderList = orderList.DisorderItems();
                MainSession.Orders.AddOrUpdate(cookie, orderList);
            }
        }

        private Order BuildOneOrder(JingyangLogin user, string timeId)
        {
            return new Order
            {
                Cookie = user.Cookie,
                UserId = user.UserId,
                Fid = user.Fid,
                Yid = user.Fid,
                TimeId = timeId,
                UserName = user.UserName ?? user.Fid
            };
        }

        protected override void AutoRun()
        {
            Task.Factory.StartNew(() => {
                try
                {
                    Appoint();
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

        private void ExecuteManual()
        {
            Task.Factory.StartNew(() => {
                try
                {
                    Appoint();
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

        private void Appoint()
        {
            foreach (var order in MainSession.Orders)
            {
                Task.Factory.StartNew(() => StartOneOrder(order.Key, order.Value));
            }
        }

        private void StartOneOrder(string cookie, List<Order> orders)
        {
            try
            {
                var appointController = MainSession.AppointSession.GetController(cookie);

                bool isSuccess = false;
                while (!isSuccess)
                {
                    foreach(var order in orders)
                    {
                        isSuccess = appointController.YuyueAsync(order);
                        if (isSuccess)
                        {
                            PrintLog("预约成功");
                            PrintLog(order.ToLogString());
                            break;
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
            var selectedDept = SelectedDepartment as JingyangHospital;
            MainSession.PlatformSession.AddOrUpdate(Constants.DeptName, selectedDept.DepartmentName);
            MainSession.PlatformSession.AddOrUpdate(Constants.HospitalName, selectedDept.HospitalName);
            MainSession.TimeIdList = selectedDept.TimeIdList;

            Log(selectedDept.ToLogString());
        }

        #endregion Hospital Dept

        #region ReSession

        protected override void ReSession()
        {
            foreach(var user in _jingyangLogins)
            {
                var tokenController = HttpServiceController.GetService<TokenController>();
                tokenController.BuildContent(user.Cookie);
                var token = tokenController.GetToken(user.Cookie);
            }

            Log("ression invoke");
        }

        #endregion Resession
    }
}
