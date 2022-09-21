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
using Zhuzher.search;
using Zhuzher.session;

namespace Zhuzher.viewmodel
{
    internal class ZhuzherViewModel : ViewModelBase
    {
        #region Properties

        public ICommand AppointmentCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand AutoRunCommand { get; set; }

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
            SearchCommand = new AsyncRelayCommand(ExecuteSearchAsync, CanExecuteSearch);

            AutoRunCommand = new RelayCommand(ExecuteAutoRun);

            SessionEvents.Instance.Subscribe(LogSession);
        }

        #endregion Constructor

        #region Search

        private bool CanExecuteSearch()
        {
            return true;
        }

        private async Task ExecuteSearchAsync()
        {
            try
            {
                var searchController = HttpServiceController.GetService<MiaoshaSearch>();
                //await searchController.SearchAllAsync(SessionItem);
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

        private void ExecuteAutoRun()
        {
            Task.Factory.StartNew(async () =>
            {
                await AutoRunAsync();
            });
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
