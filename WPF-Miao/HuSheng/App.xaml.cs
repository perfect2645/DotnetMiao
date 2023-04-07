using HttpProcessor.Container;
using HuSheng.appointment;
using HuSheng.search;
using System.Threading.Tasks;
using System.Windows;

namespace HuSheng
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
            HttpServiceController.AddTransientService<VaccPlanController>();
            HttpServiceController.AddTransientService<AppointController>();
            //HttpServiceController.AddTransientService<YzmController>();

            HttpServiceController.BuidServiceProvider();
        }
    }
}
