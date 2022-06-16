using HttpProcessor.Container;
using System.Threading.Tasks;
using WPF_Miao.Platform.yunnan.getTimestamp;

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

            var appContr = HttpServiceController.GetService<AppointmentController>();
            var timeContr = HttpServiceController.GetService<GetTimestampController>();
            appContr.TimeContra = timeContr;
            appContr.AppointmentAsync().Wait();
        }
    }
}
