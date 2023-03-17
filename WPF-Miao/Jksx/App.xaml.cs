using HttpProcessor.Container;
using System.Threading.Tasks;
using System.Windows;
using Jksx.appointment;
using Jksx.cancel;
using Jksx.login;
using Jksx.search;

namespace Jksx
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
            HttpServiceController.AddTransientService<DateController>();
            HttpServiceController.AddTransientService<MiaoController>();
            HttpServiceController.AddTransientService<YuyueController>();
            HttpServiceController.AddTransientService<CancelController>();
            HttpServiceController.AddTransientService<SearchSuccessController>();

            HttpServiceController.BuidServiceProvider();
        }
    }
}
