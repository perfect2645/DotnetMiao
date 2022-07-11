using HttpProcessor.Container;
using System.Threading.Tasks;
using WPF_Miao.Platform.yunnan.getTimestamp;
using Microsoft.Extensions.DependencyInjection;
using WPF_Miao.Platform.yunnan.model;
using HttpProcessor.Client;
using WPF_Miao.Platform.yunnan.view;

namespace WPF_Miao.Platform.yunnan
{
    internal class Initializer
    {
        public static void Init()
        {
            var win = new YunnanConsole();
            win.ShowDialog();
            //Task.Factory.StartNew(() => InitFromNonDispatcher());
        }

        public static void InitFromNonDispatcher()
        {
            HttpServiceController.AddTransientService<AppointmentController, AppointmentHandler>();
            HttpServiceController.AddTransientService<GetTimestampController>();
            HttpServiceController.ServiceCollection.AddTransient<AppointmentContent>();
            HttpServiceController.ServiceCollection.AddTransient<SecureHeader>();
            HttpServiceController.ServiceCollection.BuildServiceProvider();

            HttpServiceController.BuidServiceProvider();

            var appContr = HttpServiceController.GetService<AppointmentController>();
            appContr.AppointmentAsync().Wait();
        }
    }
}
