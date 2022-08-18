using Baohe.appointment;
using Baohe.search;
using Baohe.search.ArrangeWater;
using Baohe.search.auth;
using Baohe.search.doctor;
using Baohe.search.Liudiao;
using Baohe.search.numbers;
using Baohe.search.user;
using Base.container;
using Base.viewModel;
using HttpProcessor.Container;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Windows;

namespace Baohe
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
                InitViewContainer();
            });
        }
        private void InitController()
        {
            HttpServiceController.AddTransientService<AuthController>();
            HttpServiceController.AddTransientService<UserInfoController>();
            HttpServiceController.AddTransientService<DoctorController>();
            HttpServiceController.AddTransientService<ArrangeWaterController>();
            HttpServiceController.AddTransientService<AppointNumbersController>();
            HttpServiceController.AddTransientService<SearchController>();

            HttpServiceController.AddTransientService<LiudiaoController>();
            HttpServiceController.AddTransientService<AppointmentController>();


            HttpServiceController.BuidServiceProvider();
        }
        private void InitViewContainer()
        {
            ContainerBase.ServiceCollection.AddTransient<ISessionItem, SessionItem>();
            ContainerBase.BuildServiceProvider();
        }
    }
}
