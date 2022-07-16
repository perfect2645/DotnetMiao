using HttpProcessor.Container;
using System.Threading.Tasks;
using WPF_Miao.Platform.yunnan.getTimestamp;
using Microsoft.Extensions.DependencyInjection;
using WPF_Miao.Platform.yunnan.model;
using HttpProcessor.Client;
using WPF_Miao.Platform.yunnan.view;
using System;
using WPF_Miao.Platform.yunnan.viewModel;

namespace WPF_Miao.Platform.yunnan
{
    internal class Initializer
    {
        public static async Task InitAsync()
        {
            await Task.Factory.StartNew(() => InitHttpContainer());
            await Task.Factory.StartNew(() => InitViewContainer());
            var win = new YunnanConsole();
            win.ShowDialog();
        }
        private static void InitHttpContainer()
        {
            HttpServiceController.AddTransientService<AppointmentController, AppointmentHandler>();
            HttpServiceController.AddTransientService<GetTimestampController>();
            HttpServiceController.ServiceCollection.AddTransient<AppointmentContent>();
            HttpServiceController.ServiceCollection.AddTransient<SecureHeader>();
            HttpServiceController.ServiceCollection.BuildServiceProvider();
            HttpServiceController.BuidServiceProvider();
        }

        private static void InitViewContainer()
        {
            Container.ServiceCollection.AddTransient<ISessionItem, SessionItem>();
            Container.BuildServiceProvider();
        }
    }
}
