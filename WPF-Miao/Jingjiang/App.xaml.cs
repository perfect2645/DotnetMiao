using HttpProcessor.Container;
using Jingjiang.appointment;
using Jingjiang.login;
using Jingjiang.search;
using System.Threading.Tasks;
using System.Windows;


namespace Jingjiang
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
            HttpServiceController.BuidServiceProvider();
        }
    }
}
