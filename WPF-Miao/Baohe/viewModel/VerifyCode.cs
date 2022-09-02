using Baohe.verification;
using Base.viewModel;
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

        #endregion Properties

        #region Constructor

        public VerifyCode(LogPanel logPanel) : base(logPanel)
        {
            //ActionTime = DateTime.Now.AddMinutes(1);
            var date = new DateTime(2022, 9, 2, 19, 59, 0);

            SendYzmCommand = new DelegateCommand(ExecuteSendYzmAsync);
            VerifyYzmCommand = new DelegateCommand(ExecuteVerifyYzmAsync);
            SendYzmTimer = new ActionOnTime("发送手机验证码")
            {
                TargetAction = ExecuteSendYzmAsync,
                ActionTime = date
            };
        }

        #endregion Properties

        #region 验证码

        private async void ExecuteSendYzmAsync()
        {
            try
            {
                var yzmController = HttpServiceController.GetService<YzmController>();
                await yzmController.SendYzmAsync();
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
        private async void ExecuteVerifyYzmAsync()
        {
            var yzmController = HttpServiceController.GetService<YzmController>();
            await yzmController.CheckYzmAsync(Yzm);
        }

        #endregion 验证码
    }
}
