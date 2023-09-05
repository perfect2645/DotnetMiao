using HttpProcessor.Container;
using System.Threading.Tasks;
using System.Windows;
using Dayim.appointment;
using Dayim.cancel;
using Dayim.login;
using Dayim.search;

namespace Dayim
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
            HttpServiceController.AddTransientService<VaccineController>();
            HttpServiceController.AddTransientService<MiaoController>();
            HttpServiceController.AddTransientService<YuyueController>();
            HttpServiceController.AddTransientService<CancelController>();
            HttpServiceController.AddTransientService<SearchSuccessController>();

            HttpServiceController.BuidServiceProvider();
        }
    }
}
