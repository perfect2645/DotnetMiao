using HttpProcessor.Container;
using Guangde.login;
using Guangde.search;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Guangde
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
            HttpServiceController.AddTransientService<UserController>();
            HttpServiceController.AddTransientService<MiaoController>();
            HttpServiceController.BuidServiceProvider();
        }
    }
}
