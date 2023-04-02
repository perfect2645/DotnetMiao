using Baohe.constants;
using Baohe.session;
using Baohe.verification;
using Baohe.viewModel.platform;
using Base.viewModel;
using CoreControl.LogConsole;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
using Prism.Commands;
using Receiver.SignalRClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Baohe.viewModel
{
    internal partial class BaoheViewModel : ViewModelBase
    {

        #region Properties

        public ICommand ExchangeCommand { get; set; }


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

        #endregion Properties

        private void ExecuteExchangeAsync()
        {
            try
            {
                MainSession.YzmMode = YzmMode.PreSendOnTimeVerify;

                if (string.IsNullOrEmpty(ArrangeSn))
                {
                    MainSession.PrintLogEvent.Publish(this, "转号请先输入ArrangeSn");
                    return;
                }

                MainSession.Cookie = Cookie;

                var dept = MainSession.PlatformSesstion[Constant.Department] as Jiankangzhilu;
                dept.HasYzm = false;
                Task.Factory.StartNew(async () =>
                {
                    await AutoRunAsync();
                    VerifyCode.ExecuteVerifyYzmAsync();
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
        }
    }
}
