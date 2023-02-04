using HttpProcessor.Container;
using Huangdai.appointment;
using Huangdai.cancel;
using Huangdai.login;
using Huangdai.search;
using System.Threading.Tasks;
using System.Windows;

namespace Huangdai
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
            HttpServiceController.AddTransientService<SearchSuccessController>();
            HttpServiceController.AddTransientService<CancelController>();
            HttpServiceController.BuidServiceProvider();
        }
    }
}
