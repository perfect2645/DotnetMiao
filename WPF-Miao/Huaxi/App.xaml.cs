using HttpProcessor.Container;
using Huaxi.appointment;
using Huaxi.login;
using Huaxi.search;
using Huaxi.Yzm;
using System.Threading.Tasks;
using System.Windows;

namespace Huaxi
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
            HttpServiceController.AddTransientService<YzmController>();
            HttpServiceController.BuidServiceProvider();
        }
    }
}
