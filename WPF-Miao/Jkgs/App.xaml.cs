using HttpProcessor.Container;
using Jkgs.appointment;
using Jkgs.search;
using System.Threading.Tasks;
using System.Windows;

namespace Jkgs
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
            HttpServiceController.AddTransientService<DateController>();
            HttpServiceController.AddTransientService<UserController>();
            HttpServiceController.AddTransientService<SearchMiaoController>();
            HttpServiceController.AddTransientService<YuyueController>();
            HttpServiceController.AddTransientService<SubmitOrderController>();

            HttpServiceController.BuidServiceProvider();
        }
    }
}
