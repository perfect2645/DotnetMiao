using Base.viewModel;
using CommunityToolkit.Mvvm.Input;
using CoreControl.LogConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JkzlSearcher.viewModel
{
    internal class JkzlViewModel : OnTimeViewModel
    {

        #region Properties

        public ICommand SearchCommand { get; set; }

        #endregion Properties

        public JkzlViewModel(LogPanel logPanel) : base(logPanel)
        {
            SearchCommand = new RelayCommand(ExecuteSearch);
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

        protected override void StartAutoRun()
        {
            throw new NotImplementedException();
        }

        protected override void AutoRun()
        {
            throw new NotImplementedException();
        }

        protected override void ReSession()
        {
        }

        #endregion Status Control

        private void ExecuteSearch()
        {
        }
    }
}
