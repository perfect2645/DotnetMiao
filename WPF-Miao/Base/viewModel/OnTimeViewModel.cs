using CoreControl.LogConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.viewModel
{
    public class OnTimeViewModel : ViewModelBase
    {
        #region Properties

        private DateTime _searhTime;
        public DateTime SearhTime
        {
            get { return _searhTime; }
            set
            {
                if (_searhTime != value)
                {
                    _searhTime = value;
                    NotifyUI(() => SearhTime);
                }
            }
        }

        private int _searchInterval = 200;
        public int SearchInterval
        {
            get { return _searchInterval; }
            set
            {
                if (_searchInterval != value)
                {
                    _searchInterval = value;
                    NotifyUI(() => SearchInterval);
                }
            }
        }

        #endregion Properties

        public OnTimeViewModel(LogPanel logPanel) : base(logPanel)
        {
        }
    }
}
