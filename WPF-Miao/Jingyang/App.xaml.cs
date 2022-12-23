using HttpProcessor.Container;
using Jingyang.appointment;
using Jingyang.login;
using Jingyang.search;
using System.Threading.Tasks;
using System.Windows;

namespace Jingyang
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
            HttpServiceController.AddTransientService<TokenController>();
            HttpServiceController.AddTransientService<UserController>();
            HttpServiceController.AddTransientService<YuyueController>();
            HttpServiceController.AddTransientService<GetMiaoController>();

            HttpServiceController.BuidServiceProvider();
        }
    }
}
