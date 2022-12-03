using gaoxin.appointment;
using gaoxin.search;
using HttpProcessor.Container;
using System.Threading.Tasks;
using System.Windows;

namespace gaoxin
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
            HttpServiceController.AddTransientService<SearchMiaoController>();
            HttpServiceController.AddTransientService<YuyueController>();

            HttpServiceController.BuidServiceProvider();
        }
    }
}
