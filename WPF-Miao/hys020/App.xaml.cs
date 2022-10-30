using HttpProcessor.Container;
using hys020.appointment;
using hys020.search;
using System.Threading.Tasks;
using System.Windows;

namespace hys020
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
            HttpServiceController.AddTransientService<MiaoController>();
            HttpServiceController.AddTransientService<MiaoDetailController>();
            HttpServiceController.AddTransientService<PreviewAppointController>();
            HttpServiceController.AddTransientService<AppointController>();

            HttpServiceController.BuidServiceProvider();
        }
    }
}
