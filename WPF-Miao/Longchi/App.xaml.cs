using HttpProcessor.Container;
using Longchi.appointment;
using Longchi.search;
using System.Threading.Tasks;
using System.Windows;

namespace Longchi
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
            HttpServiceController.AddTransientService<UserController>();
            HttpServiceController.AddTransientService<ConfirmLoginController>();
            HttpServiceController.AddTransientService<YuyueController>();
            HttpServiceController.AddTransientService<VerifyYuyueController>();
            

            HttpServiceController.BuidServiceProvider();
        }
    }
}
