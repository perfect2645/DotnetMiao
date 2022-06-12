using HttpProcessor.Client;
using HttpProcessor.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Miao.Platform.shanxi.Clients.Appointment;

namespace WPF_Miao.Platform.shanxi
{
    internal static class Initializer
    {
        public static void Init()
        {
            HttpServiceController.AddTransientService<AppointmentController, AppointmentHandler>();
        }
    }
}
