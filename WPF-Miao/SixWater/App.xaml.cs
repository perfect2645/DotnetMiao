using HttpProcessor.Container;
using SixWater.appointment;
using SixWater.login;
using SixWater.search;
using System.Threading.Tasks;
using System.Windows;

namespace SixWater
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
            HttpServiceController.AddTransientService<ScheduleController>();
            HttpServiceController.AddTransientService<MiaoController>();
            HttpServiceController.BuidServiceProvider();
        }
    }
}
