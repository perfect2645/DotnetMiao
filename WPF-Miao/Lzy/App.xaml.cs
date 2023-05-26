using HttpProcessor.Container;
using System.Threading.Tasks;
using System.Windows;
using Lzy.appointment;
using Lzy.cancel;
using Lzy.login;
using Lzy.search;

namespace Lzy
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
            HttpServiceController.AddTransientService<LoginController>();
            HttpServiceController.AddTransientService<UserController>();
            HttpServiceController.AddTransientService<ScheduleController>();
            HttpServiceController.AddTransientService<MiaoController>();
            HttpServiceController.AddTransientService<YuyueController>();
            HttpServiceController.AddTransientService<CancelController>();
            HttpServiceController.AddTransientService<SearchSuccessController>();

            HttpServiceController.BuidServiceProvider();
        }
    }
}
