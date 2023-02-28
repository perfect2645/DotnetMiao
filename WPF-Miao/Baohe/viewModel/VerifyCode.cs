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

namespace Baohe.viewModel
{
    internal class VerifyCode : ViewModelBase
    {
        #region Properties

        public ICommand SendYzmCommand { get; set; }

        public ICommand VerifyYzmCommand { get; set; }

        public ActionOnTime SendYzmTimer { get; set; }
        public ActionOnTime VerifyYzmTimer { get; set; }

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

        public string UserName { get; set; }

        private YzmReceiver YzmReceiver;

        #endregion Properties

        #region Constructor

        public VerifyCode(LogPanel logPanel) : base(logPanel)
        {
            SendYzmCommand = new DelegateCommand(ExecuteSendYzmAsync);
            VerifyYzmCommand = new DelegateCommand(ExecuteVerifyYzmAsync);
            YzmReceiver = new YzmReceiver(ReceiveRemoteYzm);
        }

        #endregion Properties

        #region 验证码

        public void SetTimer()
        {
            MainSession.IsYzmChecked = false;
            var dept = MainSession.PlatformSesstion[Constant.Department] as Jiankangzhilu;
            if (!dept.HasYzm)
            {
                MainSession.IsYzmChecked = true;
                return;
            }
            var startTime = MainSession.GetStartTime();
            var sendTime = startTime.AddMinutes(-8);
            //var date = new DateTime(2022, 9, 15, 21, 59, 0);
            var verifyTime = startTime.AddMinutes(-1);

            SendYzmTimer = new ActionOnTime("发送手机验证码")
            {
                TargetAction = ExecuteSendYzmAsync,
                ActionTime = sendTime
            };

            VerifyYzmTimer = new ActionOnTime("验证手机验证码")
            {
                TargetAction = ExecuteVerifyYzmAsync,
                ActionTime = verifyTime
            };

        }

        public void StopTimer()
        {
            var dept = MainSession.PlatformSesstion[Constant.Department] as Jiankangzhilu;
            if (dept.HasYzm)
            {
                SendYzmTimer?.StopTimer();
            }
        }

        private async void ExecuteSendYzmAsync()
        {
            try
            {
                var yzmController = HttpServiceController.GetService<YzmController>();
                await yzmController.SendYzmAsync(UserName);
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
        }
        public async void ExecuteVerifyYzmAsync()
        {
            try
            {
                var yzmController = HttpServiceController.GetService<YzmController>();
                await yzmController.CheckYzmAsync(Yzm, UserName);
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
