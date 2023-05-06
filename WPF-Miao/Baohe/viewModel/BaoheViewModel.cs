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
        }

        private void TestData()
        {
            SessionItem.Referer = "https://appoint.yihu.com/appoint/doctor/doctorArrange.html?deptId=7231670&doctorId=710791067&hospitalInternal=1&showMultiDept=0&platformType=9000981&exConsult=&consultHosId=1094367&utm_source=0.0.h.1026.bus010.0";

            //VerifyCode.ArrangeSn = "170654946";
            //VerifyCode.Phone = "18301135103";
            StartTime = DateTime.Now.AddSeconds(20);
            Cookie = "jkzlAn_uuid=06063330-A203-4D12-8280-B2E000B2FD50; jkzlAn_c=-1; jkzlAn_p=-1; TOKEN_C7468BDAF6775C16167024C13B05DA1E=740790B7D895474383BADC2CC9667C3B; TOKEN_814953DC967D0DD9ABCD951393D78A8C=598A8D87AAD4432E8BCA1334961AF046; TOKEN_00B86EA55CDE196D347EB2B14817296A=A12D3468DD134ECC8B73EA630802C616; TOKEN_7900775EFB4E88DDBD7D810CA5EE1AA6=C7D97D5170D543D08DD929CE9E83C1A0; TOKEN_E5F030EBAE56BBD52C8D763AD0712D49=EB984D5BAFC745549833440E6283F095; logintype=62; loginprovinceid=0; logincityid=0; BaseDoctorUid=0; BaseUserType=0; TOKEN_C74D88F1C321E224961BC00375EDCD10=3CAA0DA289CA4B1E9E1D695F08DB5B7D; TOKEN_FB5A200594A8979BB0B27C1C70B28FBD=BFCEDF4A160441E8B2B46287CD859DA6; TOKEN_6317FBF5579509A10F191D4231E6B42C=72CB283F1D5C4270AD88DB012A79CAC9; TOKEN_26B85F817FF5A3EA2593ACA8ED99D4F8=F1A31AE406DB4CEC8D3131B9B7EA3E93; TOKEN_193D9A39836E4D78216F4D3E85816C5F=F35D781849A1484FA77B2CC19998BD40; jkzlAn_sid=35B1C80F-7A1E-4BC2-BF6E-1354B5B21282; YiHu_OpenId=eyJPcGVuSUQiOiJvZmY2dHN6TlVYenNsanhrZ2YzLUVXclpIYV9JIiwiU2VjU3RyIjoiRTdENzgxNTlGQkVFNTNDOUUxRjE5RDA3QjRDN0Y0NUUifQ%3D%3D; loginid=off6tszNUXzsljxkgf3-EWrZHa_I; OpenID=off6tszNUXzsljxkgf3-EWrZHa_I; LoginChannel=1000031; YiHu_UserJosn=eyJBY2NvdW50U24iOiIxNTY1NTI3ODAiLCJDYXJkTnVtYmVyIjoiMjA4NjYzNzMxMiIsIkxvZ2luSWQiOiJvZmY2dHN6TlVYenNsanhrZ2YzLUVXclpIYV9JIiwiVXNlck5hbWUiOiIiLCJTZWNTdHIiOiI1Qjc3NjU2Q0VDNzE0MUNDOTdFQURCRTlBQjZGQjY5OCJ9; TOKEN_5A4DB2C36B748DAB92696C9301F2A51B=8F2DE8824A5949E9A9D990A97235FDB2; _YyghSysTrackUUID=06174146128; jkzlAn_channelid=1000031; jkzlAn_userid=156552780; platformJson=%7bplatformType%3a1000031%2csourceType%3a0%2csourceId%3a0%2cclientId%3a1000031%7d; jkzlAn_ct=1683366159820";

            UserName = "陈钰鸿";
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
