using HttpProcessor.Container;
using Tjzs.appointment;
using System.Threading.Tasks;
using System.Windows;
using Tjzs.login;

namespace Tjzs
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
        private void InitController()
        {
            HttpServiceController.AddTransientService<YuyueController>();
            HttpServiceController.AddTransientService<TokenController>();
            HttpServiceController.BuidServiceProvider();
        }
    }
}
