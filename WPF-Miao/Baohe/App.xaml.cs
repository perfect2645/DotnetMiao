using Baohe.search;
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
            HttpServiceController.AddTransientService<UserInfoController>();
            HttpServiceController.AddTransientService<SearchController>();

            HttpServiceController.BuidServiceProvider();
        }
        private void InitViewContainer()
        {
            ContainerBase.ServiceCollection.AddTransient<ISessionItem, SessionItem>();
            ContainerBase.BuildServiceProvider();
        }

    }
}
