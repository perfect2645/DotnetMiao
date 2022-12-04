using HttpProcessor.Container;
using JkzlSearcher.auth;
using JkzlSearcher.search;
using JkzlSearcher.search.user;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace JkzlSearcher
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
            HttpServiceController.AddTransientService<UserInfoController>();
            HttpServiceController.AddTransientService<DoctorAuthController>();
            HttpServiceController.AddTransientService<HosDeptController>();
            HttpServiceController.AddTransientService<HospitalController>();

            HttpServiceController.BuidServiceProvider();
        }
    }
}
