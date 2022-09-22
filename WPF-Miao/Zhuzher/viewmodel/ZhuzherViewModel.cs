using Base.Events;
using Base.viewModel;
using CommunityToolkit.Mvvm.Input;
using CoreControl.LogConsole;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Zhuzher.collectsun;
using Zhuzher.Exchange;
using Zhuzher.search;
using Zhuzher.session;

namespace Zhuzher.viewmodel
{
    internal class ZhuzherViewModel : ViewModelBase
    {
        #region Properties

        public ICommand CollectSunCommand { get; set; }
        public ICommand ExchangeCommand { get; set; }
        public ICommand SeckillCommand { get; set; }

        #endregion Properties

        #region Constructor

        public ZhuzherViewModel(LogPanel logPanel) : base(logPanel)
        {
            InitStaticData();
            InitCommands();
            ZhuzherSession.PrintLogEvent = PrintLogEvent;
        }

        private void InitStaticData()
        {
        }

        private void InitCommands()
        {
            CollectSunCommand = new RelayCommand(ExecuteCollectSunAsync, CanExecuteCollectSun);
            ExchangeCommand = new RelayCommand(ExecuteExchange);

            SessionEvents.Instance.Subscribe(LogSession);
        }

        #endregion Constructor

        #region Search

        private bool CanExecuteCollectSun()
        {
            return true;
        }

        private void ExecuteCollectSunAsync()
        {
            try
            {
                ZhuzherSession.Cookie = Cookie;
                var searchController = HttpServiceController.GetService<CollectSunController>();
                searchController.CollectSunAsync();
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

        #endregion Search

        #region AutoRun

        private void ExecuteExchange()
        {
            try
            {
                ZhuzherSession.Cookie = Cookie;
                var exchangeHandler = HttpServiceController.GetService<ExchangeController>();
                exchangeHandler.ExchangeAsync();
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

        private async Task AutoRunAsync()
        {
            var searchController = HttpServiceController.GetService<MiaoshaSearch>();
            //await searchController.AutoSearchAsync(SessionItem);
        }

        #endregion AutoRun

        #region Session

        private void LogSession(object? sender, SesstionEventArgs args)
        {

        }

        #endregion Session
    }
}
