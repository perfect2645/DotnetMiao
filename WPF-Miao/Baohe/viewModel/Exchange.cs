using Baohe.verification;
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

        private async void ExecuteExchangeAsync()
        {
            try
            {
                var yzmController = HttpServiceController.GetService<YzmController>();
                await yzmController.SendYzmAsync(UserName, VerifyCode.Phone, ArrangeSn);
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
