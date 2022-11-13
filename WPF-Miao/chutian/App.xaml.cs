using chutian.appointment.Yuyue;
using chutian.login;
using chutian.search;
using HttpProcessor.Container;
using System.Threading.Tasks;
using System.Windows;

namespace chutian
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
            HttpServiceController.AddTransientService<LoginController>();
            HttpServiceController.AddTransientService<DoctorInfoController>();

            HttpServiceController.BuidServiceProvider();
        }
    }
}
