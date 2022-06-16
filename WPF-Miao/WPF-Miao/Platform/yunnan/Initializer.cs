using HttpProcessor.Container;
using System.Threading.Tasks;
using WPF_Miao.Platform.yunnan.getTimestamp;
using Microsoft.Extensions.DependencyInjection;
using WPF_Miao.Platform.yunnan.model;
using HttpProcessor.Client;

namespace WPF_Miao.Platform.yunnan
{
    internal class Initializer
    {
        public static void Init()
        {
            Task.Factory.StartNew(() => InitFromNonDispatcher());
        }

        public static void InitFromNonDispatcher()
        {
            HttpServiceController.AddTransientService<AppointmentController, AppointmentHandler>();
            HttpServiceController.AddTransientService<GetTimestampController>();
            HttpServiceController.ServiceCollection.AddTransient<AppointmentContent>();
            HttpServiceController.ServiceCollection.AddTransient<SecureHeader>();
            HttpServiceController.ServiceCollection.BuildServiceProvider();

            var appContr = HttpServiceController.GetService<AppointmentController>();
            var timeContr = HttpServiceController.GetService<GetTimestampController>();
            appContr.AppointmentAsync().Wait();
        }
    }
}
