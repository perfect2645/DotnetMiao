using HttpProcessor.Container;
using System.Threading.Tasks;
using System.Windows;
using HosTwo.appointment;
using HosTwo.cancel;
using HosTwo.login;
using HosTwo.search;

namespace HosTwo
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
            HttpServiceController.AddTransientService<MiaoController>();
            HttpServiceController.AddTransientService<YuyueController>();
            HttpServiceController.AddTransientService<CancelController>();
            HttpServiceController.AddTransientService<SearchSuccessController>();

            HttpServiceController.BuidServiceProvider();
        }
    }
}
