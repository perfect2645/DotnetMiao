using Baohe.appointment;
using Baohe.search;
using Baohe.search.ArrangeWater;
using Baohe.search.cookie;
using Baohe.search.doctor;
using Baohe.search.Liudiao;
using Baohe.search.numbers;
using Baohe.search.user;
using Baohe.verification;
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
            if (e.Args != null && e.Args.Length == 2)
            {
                Properties["UserName"] = e.Args[0];
                Properties["Cookie"] = e.Args[1];
            }
            else if (e.Args != null && e.Args.Length == 3)
            {
                Properties["UserName"] = e.Args[0];
                Properties["Cookie"] = e.Args[1];
                Properties["RetId"] = e.Args[2];
            }
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
            HttpServiceController.AddTransientService<CookieController>();
            HttpServiceController.AddTransientService<AuthController>();
            HttpServiceController.AddTransientService<UserInfoController>();
            HttpServiceController.AddTransientService<DoctorController>();
            HttpServiceController.AddTransientService<ArrangeWaterController>();
            HttpServiceController.AddTransientService<AppointNumbersController>();
            HttpServiceController.AddTransientService<SearchController>();

            HttpServiceController.AddTransientService<LiudiaoController>();
            
            HttpServiceController.AddTransientService<AppointmentController>();
            HttpServiceController.AddTransientService<YzmController>();

            HttpServiceController.BuidServiceProvider();
        }
        private void InitViewContainer()
        {
            ContainerBase.ServiceCollection.AddTransient<ISessionItem, SessionItem>();
            ContainerBase.BuildServiceProvider();
        }
    }
}
