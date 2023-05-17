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
            LoginFromConfig();
            MainSession.PrintLogEvent.Publish(this, StartTime.ToString());
            MainSession.PrintLogEvent.Publish(this, MainSession.YzmMode.ToString());
        }

        private void TestData()
        {
            SessionItem.Referer = "https://appoint.yihu.com/appoint/doctor/doctorArrange.html?deptId=7231670&doctorId=710791067&hospitalInternal=1&showMultiDept=0&platformType=9000981&exConsult=&consultHosId=1094367&utm_source=0.0.h.1026.bus010.0";

            //VerifyCode.ArrangeSn = "170654946";
            //VerifyCode.Phone = "18301135103";
            //StartTime = DateTime.Now.AddSeconds(20);
            Cookie = "jkzlAn_uuid=06063330-A203-4D12-8280-B2E000B2FD50; jkzlAn_p=-1; jkzlAn_c=-1; logintype=62; TOKEN_14FCC715CE30CDD8507EB8430632CEB4=A6D4F19DE8C543CAA5EAD8918B9A6EEB; _YyghSysTrackUUID=16192223774; YiHu_OpenId=eyJPcGVuSUQiOiJvSE4tdHdnNnNwVG84YWJtZU5mNVoyM3pfSHhZIiwiU2VjU3RyIjoiNzIyQzAxQ0ZERUIzRUFDNzlERTJDMUUyMDRBOEYwOTcifQ%3D%3D; loginid=oHN-twg6spTo8abmeNf5Z23z_HxY; OpenID=oHN-twg6spTo8abmeNf5Z23z_HxY; LoginChannel=9001016; TOKEN_542FCDD9CDDD3CB73109486E0F441BE6=0BB04EB2A3C7404DB2F986C845FD3BED; jkzlAn_sid=3BD6202F-5602-44BB-91A4-EAFE2987936B; jkzlAn_channelid=9001016; jkzlAn_utm_source=0.0.h.1026.bus010.0; loginprovinceid=0; logincityid=0; BaseDoctorUid=0; BaseUserType=0; platformJson=%7bplatformType%3a9001016%2csourceType%3a0%2csourceId%3a0%2cclientId%3a9001016%7d; jkzlAn_userid=157047433; YiHu_UserJosn=eyJBY2NvdW50U24iOiIxNTcwNDc0MzMiLCJDYXJkTnVtYmVyIjoiMjA4NzEzMTM1NiIsIkxvZ2luSWQiOiJvSE4tdHdnNnNwVG84YWJtZU5mNVoyM3pfSHhZIiwiVXNlck5hbWUiOiIiLCJTZWNTdHIiOiIyOUI2Qzc0MzJFQUUwNzAxRjlBOUNCRTE2MDQwNTdFRCJ9; jkzlAn_ct=1684323765475";

            UserName = "顾新涵";
        }

        private void InitStaticData()
        {
            MainSession.YzmMode = YzmMode.OnTimeSendVerify;

            //StartTime = DateTime.Today.AddHours(9).AddMinutes(59).AddSeconds(58);

            //StartTime = DateTime.Today.AddHours(5).AddMinutes(59).AddSeconds(58);
            //StartTime = DateTime.Today.AddHours(7).AddMinutes(59).AddSeconds(58);
            //StartTime = DateTime.Today.AddHours(9).AddMinutes(59).AddSeconds(57);
            //StartTime = DateTime.Today.AddHours(11).AddMinutes(59).AddSeconds(58);
            //StartTime = DateTime.Today.AddHours(12).AddMinutes(59).AddSeconds(57);
            //StartTime = DateTime.Today.AddHours(15).AddMinutes(59).AddSeconds(58);
            //StartTime = DateTime.Today.AddHours(19).AddMinutes(59).AddSeconds(58);
            StartTime = DateTime.Today.AddHours(20).AddMinutes(29).AddSeconds(58);
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
