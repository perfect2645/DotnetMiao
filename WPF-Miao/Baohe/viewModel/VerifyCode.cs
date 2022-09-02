using Baohe.verification;
using Base.viewModel;
using CoreControl.LogConsole;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
using Prism.Commands;
using System;
using System.Timers;
using System.Windows.Input;
using Utils.timerUtil;

namespace Baohe.viewModel
{
    internal class VerifyCode : ViewModelBase
    {
        #region Properties

        public ICommand SendYzmCommand { get; set; }

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

        #endregion Properties

        #region Constructor

        public VerifyCode(LogPanel logPanel) : base(logPanel)
        {
            SendYzmCommand = new DelegateCommand(ExecuteSendYzmAsync);
            SendYzmTimer = new ActionOnTime("发送手机验证码")
            {
                TargetAction = ExecuteSendYzmAsync,
                ActionTime = ActionTime
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

        #endregion 验证码
    }
}
