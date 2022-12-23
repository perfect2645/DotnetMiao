using Base.model;
using Base.viewmodel.status;
using Base.viewModel;
using Base.viewModel.hospital;
using CommunityToolkit.Mvvm.Input;
using CoreControl.LogConsole;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
using Jingyang.appointment;
using Jingyang.login;
using Jingyang.search;
using Jingyang.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Utils;
using Utils.datetime;
using Utils.number;
using Utils.stringBuilder;
using Utils.timerUtil;

namespace Jingyang.viewmodel
{
    internal class JingyangViewModel : OnTimeViewModel
    {
        #region Properties

        public ICommand SearchCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        private IntervalOnTime VaccineOrderInterval;
        private ActionOnTime StopVaccineOrderTimer;

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

        private readonly object OrderLock = new object();

        private SearchController _searchController;

        private List<GaoxinLogin> _gaoxinLogins = new List<GaoxinLogin>();

        #endregion Properties

        #region Constructor

        public JingyangViewModel(LogPanel logPanel) : base(logPanel)
        {
            Interval = 200;
            InitCommands();
            InitStaticData();
            MainSession.PrintLogEvent = PrintLogEvent;

            TestData();
            LoginFromConfig();
        }

        private void TestData()
        {
            StopVaccineOrderTimer = new ActionOnTime("Stop Vaccin Order")
            {
                TargetAction = StopVaccineOrder,
                ActionTime = StartTime.AddMinutes(5)
            };
        }

        private void InitStaticData()
        {
            StartTime = new DateTime(2022, 12, 7, 8, 59, 50);

            DisparkId = "b64b468d8131681e4c9dd271d573eb79";

            DateList = new List<DspVal>
            {
                new DspVal(DateTime.Today.ToString("yyyy-MM-dd")),
            };

            MainSession.PlatformSession.AddOrUpdate("DateList", DateList);

            Departments = new List<HospitalDept>
            {
                new GaoxinHospital
                {
                    HospitalId = "12510109450812372N",
                    HospitalName = "成都高新区中和社区卫生服务中心",
                    DepartmentId = DisparkId,
                    DepartmentName = "九价HPV疫苗",
                },
            };

            SelectedDepartment = Departments.FirstOrDefault();
            _searchController = new SearchController();
            MainSession.InitSession();
        }

        private void InitCommands()
        {
            LoginCommand = new RelayCommand(ExecuteLogin);
            SearchCommand = new RelayCommand(ExecuteManual);
            CancelCommand = new RelayCommand(ExecuteCancel);

            MainSession.ReSessionEvent.Subscribe(OnResession);

            SelectedDepartmentChanged = new Action(OnSelectedDepartmentChanged);

            VaccineOrderInterval = new IntervalOnTime("Vaccin Order", Interval);
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
            if (StringUtil.AnyEmpty(DisparkId))
            {
                Log("请检查DisparkId参数");
                return;
            }
            _gaoxinLogins = FileReader.DeserializeFile<List<GaoxinLogin>>("Login.json");

            foreach (var gaoxinLogin in _gaoxinLogins)
            {
                if (string.IsNullOrEmpty(gaoxinLogin.Token))
                {
                    gaoxinLogin.Token = gaoxinLogin.OrderToken;
                }
                Task.Factory.StartNew(async () =>
                {
                    await _searchController.GetUserInfoAsync(gaoxinLogin);
                });
            }

            StartAutoRun();
        }

        private void ExecuteLogin()
        {
            if (StringUtil.AnyEmpty(Token, OrderToken, DisparkId))
            {
                Log("请检查参数");
                return;
            }

            var loginData = new GaoxinLogin()
            {
                OrderToken = OrderToken,
                Token = Token,
            };

            Task.Factory.StartNew(async () =>
            {
                await _searchController.GetUserInfoAsync(loginData);
            });

            ClearLoginData();
        }

        private void ClearLoginData()
        {
            Token = string.Empty;
            OrderToken = string.Empty;
        }

        #endregion Login

        #region AutoRun

        protected override void StartAutoRun()
        {
            Task.Factory.StartNew(() => {
                try
                {
                    StartOnTimeTimer();
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
                    var orderList = MainSession.OrderDic.Values.ToList();
                    foreach (var order in orderList)
                    {
                        var yuyueController = HttpServiceController.GetService<YuyueController>();
                        var yuyueContent = new YuyueContent(order);
                        yuyueController.StartInterval(yuyueContent);
                    }

                    Task.Factory.StartNew(() =>
                    {
                        VaccineOrderInterval.StartIntervalOntime(BuildVaccineOrder);
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

        private void BuildVaccineOrder()
        {
            try
            {
                var userList = MainSession.UserDic.Values.ToList();
                foreach (var user in userList)
                {
                    Task.Factory.StartNew(() => BuildVaccineOrderForOneUser(user));
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

        private async void BuildVaccineOrderForOneUser(UserInfo user)
        {
            var vaccineController = HttpServiceController.GetService<VaccineController>();
            var vaccineContent = new VaccineContent(user);
            vaccineController.BuildClientHeaders(vaccineContent);
            await vaccineController.ProcessAsyncTask(vaccineContent);
            var order = vaccineController.OrderResult;

            if (order != null)
            {
                var yuyueController = HttpServiceController.GetService<YuyueController>();
                var yuyueContent = new YuyueContent(order);
                yuyueController.YuyueAsync(yuyueContent);
            }
        }

        private void StopVaccineOrder()
        {
            VaccineOrderInterval?.StopInterval();
        }

        #endregion AutoRun

        #region Appoint

        private void ExecuteManual()
        {
            Task.Factory.StartNew(async () => {
                try
                {

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
            var selectedDept = SelectedDepartment as GaoxinHospital;
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
