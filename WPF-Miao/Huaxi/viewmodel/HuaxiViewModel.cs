using Base.viewModel;
using Base.viewModel.hospital;
using CommunityToolkit.Mvvm.Input;
using CoreControl.LogConsole;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
using Huaxi.appointment;
using Huaxi.login;
using Huaxi.search;
using Huaxi.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Utils;
using Utils.file;

namespace Huaxi.viewmodel
{
    internal class HuaxiViewModel : OnTimeViewModel
    {
        #region Properties

        public ICommand SearchCommand { get; set; }
        public ICommand LoginCommand { get; set; }

        private readonly object OrderLock = new object();

        private SearchController _searchController;

        private string _yzmSource;

        public string YzmSource
        {
            get { return _yzmSource; }

            set 
            { 
                _yzmSource = value;
                NotifyUI(() => YzmSource);
            }
        }

        #endregion Properties

        #region Constructor

        public HuaxiViewModel(LogPanel logPanel) : base(logPanel)
        {
            InitCommands();
            InitStaticData();
            MainSession.PrintLogEvent = PrintLogEvent;

            TestData();
            LoginFromConfigAsync();
        }

        private void TestData()
        {
            Interval = 200;

            StartTime = DateTime.Now.AddSeconds(5);

            YzmSource = "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";
        }

        private void InitStaticData()
        {
            StartTime = DateTime.Today.AddHours(7).AddMinutes(29).AddSeconds(50);

            Departments = new List<HospitalDept>
            {
                new HuaxiHospital
                {
                    HospitalId = "HXD2",
                    HospitalName = "四川大学华西第二医院 (锦江院区)",
                    DoctorId = "6809141336038441002",
                    DoctorName = "胡雅毅"
                },
                new HuaxiHospital
                {
                    HospitalId = "HXD2",
                    HospitalName = "四川大学华西第二医院 (锦江院区)",
                    DoctorId = "7782840383733497862",
                    DoctorName = "李宏宇"
                }
            };

            SelectedDepartment = Departments.FirstOrDefault();
            _searchController = new SearchController();
        }

        private void InitCommands()
        {
            SearchCommand = new RelayCommand(ExecuteManual);
            SelectedDepartmentChanged = new Action(OnSelectedDepartmentChanged);
            MainSession.OrderEvent.Subscribe(OnOrder);
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

        private void LoginFromConfigAsync()
        {
            MainSession.Users = FileReader.DeserializeFile<List<HuaxiLogin>>("Login.json");
            foreach (var user in MainSession.Users)
            {
                var userController = HttpServiceController.GetService<UserController>();
                userController.GetUserAsync(user);
            }

            MainSession.InitSession();
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
                    _searchController.SearchMiao();
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

        private void StartOneOrder(string userName, Order order)
        {
            try
            {
                bool isSuccess = false;
                while (!isSuccess)
                {
                    var appointController = MainSession.AppointSession.GetController($"{userName}");
                    isSuccess = appointController.YuyueAsync(order);
                    if (isSuccess)
                    {
                        PrintLog("预约成功");
                        PrintLog(order.ToLogString());
                        return;
                    }
                    Thread.Sleep(2000);
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


        private void StartOneOrder(string userName, List<Order> orders)
        {
            try
            {
                foreach (var order in orders)
                {
                    var appointController = MainSession.AppointSession.GetController($"{userName}");
                    var isSuccess = appointController.YuyueAsync(order);
                    if (isSuccess)
                    {
                        PrintLog("预约成功");
                        PrintLog(order.ToLogString());
                        return;
                    }
                    Thread.Sleep(2000);
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

        private void OnOrder(object? sender, OrderEventArgs e)
        {
            var orderTemplateList = e.OrderList;
            foreach (var user in MainSession.Users)
            {
                var orderList = new List<Order>();
                foreach (var template in orderTemplateList)
                {
                    var order = new Order
                    {
                        UserName = user.UserName,
                        User = user,
                        CardId = user.CardId,
                        OrganCode = template.OrganCode,
                        ScheduleId = template.ScheduleId,
                    };

                    orderList.Add(order);
                }

                Task.Factory.StartNew(() => StartOneOrder(user.UserName, orderList));
            }
        }

        private void DirectlyOrder(string scheduleId)
        {
            var order = new Order();
        }

        #endregion Appoint

        #region Hospital Dept

        private void OnSelectedDepartmentChanged()
        {
            var selectedDept = SelectedDepartment as HuaxiHospital;
            MainSession.PlatformSession.AddOrUpdate(Constants.HospitalId, selectedDept.HospitalId);
            MainSession.PlatformSession.AddOrUpdate(Constants.HospitalName, selectedDept.HospitalName);
            MainSession.PlatformSession.AddOrUpdate(Constants.DeptName, selectedDept.DepartmentName);
            MainSession.PlatformSession.AddOrUpdate(Constants.DeptId, selectedDept.DepartmentId);
            MainSession.PlatformSession.AddOrUpdate(Constants.DoctorId, selectedDept.DoctorId);
            MainSession.PlatformSession.AddOrUpdate(Constants.DoctorName, selectedDept.DoctorName);

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
