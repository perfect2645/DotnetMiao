using HttpProcessor.Container;
using suiyang.appointment;
using suiyang.login;
using suiyang.search;
using System.Threading.Tasks;
using System.Windows;

namespace suiyang
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
            HttpServiceController.AddTransientService<GetMiaoController>();

            HttpServiceController.BuidServiceProvider();
        }
    }
}
