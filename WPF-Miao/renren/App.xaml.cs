using HttpProcessor.Container;
using renren.appointment;
using renren.search;
using renren.search.hospital;
using renren.search.miao;
using renren.search.patient;
using System.Threading.Tasks;
using System.Windows;

namespace renren
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
            HttpServiceController.AddTransientService<PatientController>();
            HttpServiceController.AddTransientService<HospitalTeamsController>();
            HttpServiceController.AddTransientService<ScheduleController>();
            HttpServiceController.AddTransientService<ScheduleDetailController>();
            HttpServiceController.AddTransientService<AppointController>();


            HttpServiceController.BuidServiceProvider();
        }
    }
}
