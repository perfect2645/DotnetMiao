using HttpProcessor.Container;
using Redhouse.search;
using System.Threading.Tasks;
using System.Windows;

namespace Redhouse
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //if (e.Args != null && e.Args.Length == 2)
            //{
            //    Properties["Cookie"] = e.Args[0];
            //    Properties["Location"] = e.Args[1];
            //}

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
            HttpServiceController.AddTransientService<UserController>();
            HttpServiceController.AddTransientService<MiaoController>();

            HttpServiceController.BuidServiceProvider();
        }
    }
}
