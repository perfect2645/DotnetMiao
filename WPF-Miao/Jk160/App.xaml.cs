using System;
using System.Threading.Tasks;
using System.Windows;

namespace Jk160
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            InitControllerAsync();
        }

        private void InitControllerAsync()
        {
            Task.Factory.StartNew(() =>
            {
                InitController();
            });
        }

        private void InitViewContainer()
        {
        }

        private void InitController()
        {
        }
    }
}
