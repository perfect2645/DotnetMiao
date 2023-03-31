using HttpProcessor.Container;
using Jikong.appointment;
using Jikong.cancel;
using Jikong.login;
using Jikong.search;
using System.Threading.Tasks;
using System.Windows;

namespace Jikong
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
            HttpServiceController.AddTransientService<OpenIdController>();
            HttpServiceController.AddTransientService<UserController>();
            HttpServiceController.AddTransientService<DateController>();
            HttpServiceController.AddTransientService<MiaoController>();
            HttpServiceController.AddTransientService<YuyueController>();
            HttpServiceController.AddTransientService<YuyueHpvController>();
            HttpServiceController.AddTransientService<SearchSuccessController>();
            HttpServiceController.AddTransientService<CancelController>();
            HttpServiceController.BuidServiceProvider();
        }
    }
}
