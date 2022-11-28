using Base.viewModel;
using CommunityToolkit.Mvvm.Input;
using CoreControl.LogConsole;
using HttpProcessor.ExceptionManager;
using JkzlSearcher.search;
using JkzlSearcher.session;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JkzlSearcher.viewModel
{
    internal class JkzlViewModel : OnTimeViewModel
    {

        #region Properties

        public ICommand SearchCommand { get; set; }

        private SearchController _searchController;

        #endregion Properties

        public JkzlViewModel(LogPanel logPanel) : base(logPanel)
        {
            SearchCommand = new RelayCommand(ExecuteSearch);
            _searchController = new SearchController();
            MainSession.PrintLogEvent = PrintLogEvent;
            StartTime = DateTime.Now.AddSeconds(10);
        }

        #region Status Control

        protected override void OnInitAsync()
        {

        }

        protected override void OnReadyForSearchAsync()
        {
        }

        protected override void OnSearchingAsync()
        {

        }

        protected override void OnSearchendAsync()
        {

        }

        protected override void OnMiaoGetAsync(object data)
        {
            StopIntervalTimer();
        }

        protected override void ReSession()
        {
        }

        #endregion Status Control

        #region AutoRun

        protected override void StartAutoRun()
        {
            Task.Factory.StartNew(() => {
                try
                {
                    StartIntervalTimer();
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

        protected override void AutoRun()
        {
            Task.Factory.StartNew(() => {
                try
                {
                    _searchController.SearchByHospitalId();
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

        #endregion AutoRun

        private void ExecuteSearch()
        {
            _searchController.SearchByHospitalId();
        }
    }
}
