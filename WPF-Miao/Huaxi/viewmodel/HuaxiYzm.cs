using Base.viewModel;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
using Huaxi.search;
using Huaxi.session;
using Huaxi.Yzm;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Utils;

namespace Huaxi.viewmodel
{
    internal partial class HuaxiViewModel
    {
        #region Properties
        private string _yzmSource;

        public string YzmSource
        {
            get { return _yzmSource; }

            set
            {
                _yzmSource = value;
                NotifyUI(() => YzmSource);
            }
        }

        private string _yzmText;
        public string YzmText
        {
            get { return _yzmText; }

            set
            {
                _yzmText = value;
                NotifyUI(() => YzmText);

                if (string.IsNullOrEmpty(value) || value.Length == 4)
                {
                    MainSession.PlatformSession.AddOrUpdate(Constants.Yzm, _yzmText);
                    MainSession.PrintLogEvent.Publish(YzmText, $"验证码已更新 {value}");
                }
            }
        }

        private string _bizSeq;
        public string BizSeq
        {
            get { return _bizSeq; }

            set
            {
                _bizSeq = value;
                NotifyUI(() => BizSeq);
            }
        }

        public ICommand YzmRefreshCommand { get; set; }

        #endregion Properties

        #region SendYzm

        private void ExecuteSendYzm()
        {
            Task.Factory.StartNew(async () => {
                try
                {
                    var yzmController = HttpServiceController.GetService<YzmController>();
                    var defaultUser = MainSession.Users.FirstOrDefault();
                    var yzm = await yzmController.GetYzmAsync(defaultUser);
                    if (yzm == null)
                    {
                        return;
                    }

                    YzmSource = yzm.ImageData;
                    BizSeq = yzm.BizSeq;

                    MainSession.PlatformSession.AddOrUpdate(Constants.BizSeq, BizSeq);

                }
                catch (HttpException ex)
                {
                    Log(ex);
                }
                catch (Exception ex)
                {
                    Log(ex);
                }
            });
        }

        #endregion SendYzm
    }
}
