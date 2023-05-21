using Baohe.appointment;
using Baohe.constants;
using Baohe.login;
using Baohe.search;
using Baohe.session;
using Baohe.viewModel.platform;
using Base.Events;
using Base.viewModel;
using Base.viewModel.hospital;
using CoreControl.LogConsole;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Utils;
using Utils.datetime;
using Utils.file;
using Utils.stringBuilder;

namespace Baohe.viewModel
{
    internal partial class BaoheViewModel: ViewModelBase
    {
        #region Properties

        public ICommand SearchCommand { get; set; }

        public ICommand AutoRunCommand { get; set; }

        public VerifyCode VerifyCode { get; set; }

        private string _retId;
        public string RetId
        {
            get { return _retId; }
            set
            {
                _retId = value;
                MainSession.PlatformSesstion.AddOrUpdate(Constant.RetId, value);
                NotifyUI(() => RetId);
            }
        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                if (VerifyCode != null)
                {
                    VerifyCode.UserName = value;
                }
                _userName = value;
                NotifyUI(() => UserName);
            }
        }

        private SearchController SearchController;

        #endregion Properties

        #region Constructor

        public BaoheViewModel(LogPanel logPanel) : base(logPanel)
        {
            InitCommands();
            InitStaticData();
            VerifyCode = new VerifyCode(logPanel);
            MainSession.PrintLogEvent = PrintLogEvent;
            MainSession.UpdateUiEvent = UpdateUiEvent;

            TestData();
            //LoginFromConfig();
            MainSession.PrintLogEvent.Publish(this, StartTime.ToString());
            MainSession.PrintLogEvent.Publish(this, MainSession.YzmMode.ToString());

            UserName = "黄星晨";
            Cookie = "jkzlAn_uuid=DF05E263-830B-41C9-8E0B-08CCA0983619; logintype=62; loginprovinceid=0; logincityid=0; BaseDoctorUid=0; BaseUserType=0; jkzlAn_p=-1; jkzlAn_c=-1; TOKEN_E6558D443C92A50A3EEA83AEEE57599F=FAE04C4EF9964990854AB517B3DF1CBF; TOKEN_EFA54E3C699F73D79CAD8AE045BE1D69=4434A1706E1B4CBFB63D4B8E0ECD7E31; TOKEN_56EE97A8CEDC2C347F0F6B4123E4D16B=B5E6718B511B40F7A2927FA03794CBC8; TOKEN_DB0DF065DCA71EC030402DBF2245D0ED=7981F07485614613B490B110AC6B5518; LoginChannel=9001478; platformJson=%7bplatformType%3a9001478%2csourceType%3a0%2csourceId%3a0%2cclientId%3a9001478%7d; jkzlAn_channelid=9001478; YiHu_OpenId=eyJPcGVuSUQiOiJvWTVUcDUydW1KZjBpdUpKZ2tKdHpNX1p6a3RzIiwiU2VjU3RyIjoiMzEzMjlDMzU0OTNDODdFRDUyNjg0RjU0RUJGRTFGMkYifQ%3D%3D; loginid=oY5Tp52umJf0iuJJgkJtzM_Zzkts; OpenID=oY5Tp52umJf0iuJJgkJtzM_Zzkts; YiHu_UserJosn=eyJBY2NvdW50U24iOiIxNTc0NDk3OTciLCJDYXJkTnVtYmVyIjoiMjA4NzUzMjYyMCIsIkxvZ2luSWQiOiJvWTVUcDUydW1KZjBpdUpKZ2tKdHpNX1p6a3RzIiwiVXNlck5hbWUiOiIiLCJTZWNTdHIiOiI5OTQyRjg5QjVGRUM0MDE0NUI4NzdEQTg3QzU5RjhEQiJ9; TOKEN_278A444FF64C86F60CD123B1D689F903=ED53855DA48C42C68383B0979966C907; jkzlAn_refercode=; jkzlAn_utm_source=; jkzlAn_sid=D2C964E0-3478-41F6-8A5B-CC776B127C25; jkzlAn_userid=157449797; jkzlAn_ct=1684597314487";
        }

        private void TestData()
        {
            SessionItem.Referer = "https://appoint.yihu.com/appoint/doctor/doctorArrange.html?deptId=7231670&doctorId=710791067&hospitalInternal=1&showMultiDept=0&platformType=9000981&exConsult=&consultHosId=1094367&utm_source=0.0.h.1026.bus010.0";

            //VerifyCode.ArrangeSn = "170654946";
            //VerifyCode.Phone = "18301135103";
            StartTime = DateTime.Now.AddSeconds(20);
        }

        private void InitStaticData()
        {
            MainSession.YzmMode = YzmMode.PreSendVerify;

            //StartTime = DateTime.Today.AddHours(9).AddMinutes(59).AddSeconds(58);

            //StartTime = DateTime.Today.AddHours(5).AddMinutes(59).AddSeconds(58);
            //StartTime = DateTime.Today.AddHours(7).AddMinutes(59).AddSeconds(58);
            //StartTime = DateTime.Today.AddHours(9).AddMinutes(59).AddSeconds(57);
            //StartTime = DateTime.Today.AddHours(12).AddMinutes(29).AddSeconds(58);
            //StartTime = DateTime.Today.AddHours(13).AddMinutes(13).AddSeconds(57);
            //StartTime = DateTime.Today.AddHours(15).AddMinutes(59).AddSeconds(58);
            //StartTime = DateTime.Today.AddHours(19).AddMinutes(59).AddSeconds(58);
            //StartTime = DateTime.Today.AddHours(20).AddMinutes(29).AddSeconds(58);
            //StartTime = DateTime.Today.AddHours(21).AddMinutes(59).AddSeconds(58);

            if (Application.Current.Properties.Contains("UserName"))
            {
                MainSession.User.UserName = Application.Current.Properties["UserName"].ToString();
                UserName = Application.Current.Properties["UserName"].ToString();
            }
            if (Application.Current.Properties.Contains("Cookie"))
            {
                MainSession.User.Cookie = Application.Current.Properties["Cookie"].ToString();
                Cookie = Application.Current.Properties["Cookie"].ToString();
            }

            if (Application.Current.Properties.Contains("RetId"))
            {
                MainSession.User.RetId = Application.Current.Properties["RetId"].ToString();
                RetId = Application.Current.Properties["RetId"].ToString();
            }

            BuildDepartments();

            InitPlatformSession();
        }

        private void InitPlatformSession()
        {
            try
            {
                var tsStr = DateTimeUtil.GetTimeStamp();
                var sessionTime = tsStr.Substring(0, 10);
                MainSession.PlatformSesstion.Add(Constant.SessionTime, sessionTime);
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

        private void InitCommands()
        {
            SearchCommand = new DelegateCommand(ExecuteSearchAsync, CanExecuteSearch);

            AutoRunCommand = new DelegateCommand(ExecuteAutoRun);

            ExchangeCommand = new DelegateCommand(ExecuteExchangeAsync);

            SessionEvents.Instance.Subscribe(LogSession);

            SelectedDepartmentChanged = new Action(OnSelectedDepartmentChanged);
            StartTimeChanged = new Action<DateTime?>(OnStartTimeChanged);
        }

        #endregion Constructor

        #region Login

        private void LoginFromConfig()
        {
            if (!string.IsNullOrWhiteSpace(MainSession.User.Cookie))
            {
                return;
            }
            var users = FileReader.DeserializeFile<List<JkzlLogin>>("Login.json");
            foreach (var user in users)
            {
                StartApp(user);
            }
        }

        private void StartApp(JkzlLogin user)
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    var processInfo = new ProcessStartInfo();
                    processInfo.FileName = "baohe.exe";
                    processInfo.ArgumentList.Add(user.UserName);
                    processInfo.ArgumentList.Add(user.Cookie);
                    processInfo.ArgumentList.Add(user.RetId ?? string.Empty);

                    var p = Process.Start(processInfo);
                }
                catch (Exception ex)
                {
                    Log(ex);
                }
            });

        }

        #endregion Login

        #region Appointment

        private bool CanExecuteAppointment()
        {
            return true;
        }

        #endregion Appointment

        #region Search

        private void SetSearchTimers()
        {
            SearchController.SetTimer();
            VerifyCode.SetTimer();
        }

        private void StopSearchTimers()
        {
            SearchController.StopTimer();
            VerifyCode.StopTimer();
        }

        private bool CanExecuteSearch()
        {
            return true;
        }

        private async void ExecuteSearchAsync()
        {
            try
            {
                MainSession.Cookie = Cookie;
                SearchController = HttpServiceController.GetService<SearchController>();
                SearchController.UserName= UserName;
                SearchController.UserPhone = VerifyCode.Phone;
                SetSearchTimers();
                await SearchController.SearchAllAsync(UserName);
            }
            catch (HttpException ex)
            {
                StopSearchTimers();
                Log(ex);
            }
            catch (Exception ex)
            {
                StopSearchTimers();
                Log(ex);
            }
        }

        #endregion Search

        #region AutoRun

        private void ExecuteAutoRun()
        {
            MainSession.Cookie = Cookie;
            Task.Factory.StartNew(async () =>
            {
                await AutoRunAsync();
            });
        }

        private async Task AutoRunAsync()
        {
            SearchController = HttpServiceController.GetService<SearchController>();
            SearchController.UserName = UserName;
            SearchController.UserPhone = VerifyCode.Phone;
            SearchController.CheckYzmAction = VerifyCode.ExecuteVerifyYzmAsync;
            SetSearchTimers();
            await SearchController.AutoSearchAsync(UserName);
        }

        #endregion AutoRun

        #region Session

        private void LogSession(object? sender, SesstionEventArgs args)
        {

        }

        #endregion Session

        #region Hospital Dept

        private void OnSelectedDepartmentChanged()
        {
            var selectedDept = SelectedDepartment as Jiankangzhilu;
            MainSession.PlatformSesstion.AddOrUpdate(Constant.PlatformType, selectedDept.PlatformId);
            MainSession.PlatformSesstion.AddOrUpdate(Constant.HospitalId, selectedDept.HospitalId);
            MainSession.PlatformSesstion.AddOrUpdate(Constant.DeptId, selectedDept.DepartmentId);
            MainSession.PlatformSesstion.AddOrUpdate(Constant.Department, selectedDept);
            MainSession.PlatformSesstion.AddOrUpdate(Constant.DoctorSn, selectedDept.DoctorSn);

            Log(selectedDept.ToLogString());

            MainSession.BuildMiaoSession(MainSession.PlatformSesstion[Constant.DeptId].NotNullString());
        }

        #endregion Hospital Dept

        #region Start Time

        private void OnStartTimeChanged(DateTime? selectedTime)
        {
            MainSession.PlatformSesstion.AddOrUpdate(Constant.StartTime, selectedTime!);
        }

        #endregion Start Time

        #region Update UI

        protected override void UpdateUI(UiEventArgs e)
        {
            var field = e.Field;
            if ("yzm".Equals(field, StringComparison.OrdinalIgnoreCase))
            {
                VerifyCode.Yzm = e.Value.NotNullString();
                VerifyCode.ExecuteVerifyYzmAsync();
                return;
            }

            if ("phone".Equals(field, StringComparison.OrdinalIgnoreCase))
            {
                VerifyCode.Phone = e.Value.NotNullString();
                return;
            }
        }

        #endregion Update UI

    }
}
