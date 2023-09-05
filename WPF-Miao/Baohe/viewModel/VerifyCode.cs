using Baohe.constants;
using Baohe.session;
using Baohe.verification;
using Baohe.viewModel.platform;
using Base.viewModel;
using Receiver.SignalRClient;
using CoreControl.LogConsole;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
using Prism.Commands;
using System;
using System.Windows.Input;
using Utils.timerUtil;
using Utils;
using Utils.stringBuilder;

namespace Baohe.viewModel
{
    internal class VerifyCode : ViewModelBase
    {
        #region Properties

        public ICommand SendYzmCommand { get; set; }

        public ICommand VerifyYzmCommand { get; set; }

        public ActionOnTime SendYzmTimer { get; set; }
        public ActionOnTime VerifyYzmTimer { get; set; }

        private bool _isCheckingYzm = false;

        private DateTime _actionTime = DateTime.Now;
        public DateTime ActionTime
        {
            get { return _actionTime; }
            set
            {
                _actionTime = value;
                NotifyUI(() => ActionTime);
            }
        }

        private string yzm;
        public string Yzm
        {
            get { return yzm; }
            set
            {
                yzm = value;
                NotifyUI(() => Yzm);
                ProcessYzmUpdated();
            }
        }

        private string phone;
        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                NotifyUI(() => Phone);
            }
        }

        private string arrangeSn;
        public string ArrangeSn
        {
            get { return arrangeSn; }
            set
            {
                arrangeSn = value;
                NotifyUI(() => ArrangeSn);
            }
        }

        public string UserName { get; set; }

        private YzmReceiver YzmReceiver;

        #endregion Properties

        #region Constructor

        public VerifyCode(LogPanel logPanel) : base(logPanel)
        {
            SendYzmCommand = new DelegateCommand(ExecuteSendYzmAsync);
            VerifyYzmCommand = new DelegateCommand(ExecuteVerifyYzmAsync);
            //YzmReceiver = new YzmReceiver(ReceiveRemoteYzm);
        }

        #endregion Constructor

        #region 验证码

        public void SetTimer()
        {
            MainSession.IsYzmSent = false;
            MainSession.IsYzmChecked = false;
            var dept = MainSession.PlatformSesstion[Constant.Department] as Jiankangzhilu;
            if (!dept.HasYzm)
            {
                MainSession.IsYzmSent = true;
                MainSession.IsYzmChecked = true;
                return;
            }

            if (MainSession.YzmMode == YzmMode.PreSendVerify)
            {
                MainSession.IsYzmSent = true;
                MainSession.IsYzmChecked = true;
            }

            var startTime = MainSession.GetStartTime();
            var sendTime = startTime.AddMinutes(-7);
            //var date = new DateTime(2022, 9, 15, 21, 59, 0);

            if (MainSession.YzmMode != YzmMode.OnTimeSendVerify)
            {
                SendYzmTimer = new ActionOnTime("发送手机验证码")
                {
                    TargetAction = ExecuteSendYzmAsync,
                    ActionTime = sendTime
                };
            }

            if (MainSession.YzmMode == YzmMode.PreSendVerify)
            {
                var verifyTime = startTime.AddMinutes(-1);
                VerifyYzmTimer = new ActionOnTime("验证手机验证码")
                {
                    TargetAction = ExecuteVerifyYzmAsync,
                    ActionTime = verifyTime
                };
            }
        }

        public void StopTimer()
        {
            var dept = MainSession.PlatformSesstion[Constant.Department] as Jiankangzhilu;
            if (dept.HasYzm)
            {
                SendYzmTimer?.StopTimer();
            }
        }

        private void ProcessYzmUpdated()
        {
            if (MainSession.YzmMode == YzmMode.ExchangePreSendOnTimeVerify)
            {
                return;
            }

            if (!MainSession.IsYzmSent)
            {
                return;
            }

            if (MainSession.IsYzmChecked)
            {
                return;
            }

            if (_isCheckingYzm)
            {
                return;
            }

            ExecuteVerifyYzmAsync();
        }

        private async void ExecuteSendYzmAsync()
        {
            try
            {
                _isCheckingYzm = true;
                var yzmController = HttpServiceController.GetService<YzmController>();
                var isYzmSent = await yzmController.SendYzmAsync(UserName, Phone, ArrangeSn);
                if (MainSession.YzmMode == YzmMode.OnTimeSendVerify || MainSession.YzmMode == YzmMode.ExchangePreSendOnTimeVerify)
                {
                    MainSession.IsYzmSent= isYzmSent;
                }
            }
            catch (HttpException ex)
            {
                StopTimer();
                Log(ex);
            }
            catch (Exception ex)
            {
                StopTimer();
                Log(ex);
            }
            finally
            {
                _isCheckingYzm = false;
            }
        }
        public async void ExecuteVerifyYzmAsync()
        {
            try
            {
                if (!MainSession.DefaultWater.HasItem())
                {
                    return;
                }

                if (string.IsNullOrEmpty(ArrangeSn))
                {
                    ArrangeSn = MainSession.DefaultWater["ArrangeID"].NotNullString();
                }

                var yzmController = HttpServiceController.GetService<YzmController>();
                await yzmController.CheckYzmAsync(Yzm, UserName, Phone, ArrangeSn);
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

        #endregion 验证码

        #region 接收验证码

        private void ReceiveRemoteYzm(string phone, string yzm)
        {
            try
            {
                if(Phone == phone)
                {
                    Yzm = yzm;
                    MainSession.PrintLogEvent.Publish(this, $"接收到验证码 - 手机号:{phone}:验证码:{yzm}");
                    return;
                }
                Log($"Bypass其他手机验证码 - 手机号:{phone}:验证码:{yzm}");
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        #endregion 接收验证码
    }
}
