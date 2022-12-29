using Xihongmen.appointment;
using Xihongmen.login;
using Xihongmen.search;
using HttpProcessor.Container;
using System.Threading.Tasks;
using System.Windows;

namespace Xihongmen
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
            HttpServiceController.AddTransientService<LoginController>();
            HttpServiceController.AddTransientService<FamilyController>();
            HttpServiceController.AddTransientService<DoctorInfoController>();
            HttpServiceController.AddTransientService<PreOrderController>();

            HttpServiceController.BuidServiceProvider();
        }
    }
}
