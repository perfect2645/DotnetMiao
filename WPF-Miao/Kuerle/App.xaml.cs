using HttpProcessor.Container;
using Kuerle.appointment;
using Kuerle.login;
using Kuerle.search;
using System.Threading.Tasks;
using System.Windows;

namespace Kuerle
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
            HttpServiceController.AddTransientService<UserController>();
            HttpServiceController.AddTransientService<MiaoController>();
            HttpServiceController.AddTransientService<VidController>();
            HttpServiceController.AddTransientService<IndexController>();
            HttpServiceController.BuidServiceProvider();
        }
    }
}
