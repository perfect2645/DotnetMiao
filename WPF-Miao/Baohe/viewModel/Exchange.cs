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

        #endregion Properties

        private void ExecuteExchangeAsync()
        {
            try
            {
                VerifyCode.ArrangeSn = "173902390";
                MainSession.ExchangeInfo = new ExchangeInfo(VerifyCode.ArrangeSn, "837285164", "34", "15:36-15:38");
                UserName = "张海丽";
                StartTime = DateTime.Now.AddSeconds(20);
                MainSession.YzmMode = YzmMode.ExchangePreSendOnTimeVerify;

                if (string.IsNullOrEmpty(VerifyCode.ArrangeSn))
                {
                    MainSession.PrintLogEvent.Publish(this, "转号请先输入ArrangeSn");
                    return;
                }

                MainSession.Cookie = Cookie;
                Task.Factory.StartNew(async () =>
                {
                    await AutoRunAsync();
                    //VerifyCode.ExecuteVerifyYzmAsync();
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
