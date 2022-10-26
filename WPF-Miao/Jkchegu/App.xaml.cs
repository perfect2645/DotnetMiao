using HttpProcessor.Container;
using Jkchegu.appointment;
using Jkchegu.search;
using Jkchegu.search.yzm;
using System.Threading.Tasks;
using System.Windows;

namespace Jkchegu
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
            HttpServiceController.AddTransientService<SearchController>();
            HttpServiceController.AddTransientService<DateCountController>();
            HttpServiceController.AddTransientService<AppointController>(); 
            HttpServiceController.AddTransientService<YzmController>();

            HttpServiceController.BuidServiceProvider();
        }
    }
}
