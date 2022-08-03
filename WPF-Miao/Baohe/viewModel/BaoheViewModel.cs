using Base.viewModel;
using CoreControl.LogConsole;

namespace Baohe.viewModel
{
    internal class BaoheViewModel: ViewModelBase
    {
        public BaoheViewModel(LogPanel logPanel) : base(logPanel)
        {
            Log("BaoheViewModel start");
        }
    } 
}
