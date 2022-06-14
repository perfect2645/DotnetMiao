using HttpProcessor.Container;

namespace WPF_Miao.Platform.yunnan
{
    internal class Initializer
    {
        public static void Init()
        {
            HttpServiceController.AddTransientService<AppointmentController, AppointmentHandler>();


            var controller = HttpServiceController.GetService<AppointmentController>();
            controller.AppointmentAsync().Wait();
        }
    }
}
