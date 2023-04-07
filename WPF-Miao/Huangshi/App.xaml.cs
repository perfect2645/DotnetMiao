using HttpProcessor.Container;
using Huangshi.appointment;
using Huangshi.cancel;
using Huangshi.login;
using Huangshi.search;
using System.Threading.Tasks;
using System.Windows;

namespace Huangshi
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
            HttpServiceController.AddTransientService<TimeController>();
            HttpServiceController.AddTransientService<MiaoController>();
            HttpServiceController.AddTransientService<YuyueController>();
            HttpServiceController.AddTransientService<SearchSuccessController>();
            HttpServiceController.AddTransientService<CancelController>();
            HttpServiceController.BuidServiceProvider();
        }
    }
}
