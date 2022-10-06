using Base.Events;
using Base.viewModel;
using CommunityToolkit.Mvvm.Input;
using CoreControl.LogConsole;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
using Jkchegu.appointment;
using Jkchegu.search;
using Jkchegu.session;
using System;
using System.Windows.Input;

namespace Jkchegu.viewmodel
{
    internal class JkcheguViewModel : ViewModelBase
    {
        #region Properties

        public ICommand SearchCommand { get; set; }
        public ICommand AppointCommand { get; set; }

        #endregion Properties

        #region Constructor

        public JkcheguViewModel(LogPanel logPanel) : base(logPanel)
        {
            InitStaticData();
            InitCommands();
            JkSession.PrintLogEvent = PrintLogEvent;
        }

        private void InitStaticData()
        {
        }

        private void InitCommands()
        {
            SearchCommand = new RelayCommand(ExecuteSearch);
            AppointCommand = new RelayCommand(ExecuteAppoint);

            SessionEvents.Instance.Subscribe(LogSession);
        }

        #endregion Constructor

        #region Search

        private bool CanExecuteCollectSun()
        {
            return true;
        }

        private void ExecuteSearch()
        {
            try
            {
                JkSession.Cookie = Cookie;
                var searchController = HttpServiceController.GetService<SearchController>();
                searchController.SearchAsync();
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

        #region Appoint

        private void ExecuteAppoint()
        {
            try
            {
                JkSession.Cookie = Cookie;
                var appointController = HttpServiceController.GetService<AppointController>();
                appointController.Appoint();
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

        #endregion Appoint

        #region Session

        private void LogSession(object? sender, SesstionEventArgs args)
        {

        }

        #endregion Session
    }
}
