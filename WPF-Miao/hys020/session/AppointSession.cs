using hys020.appointment.Yuyue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hys020.session
{
    public static class AppointSession
    {
        public static Dictionary<string, YuyueController> YuyueControllerCache { get; set; }

        static AppointSession()
        {

        }

        public static void Init()
        {
            YuyueControllerCache = new Dictionary<string, YuyueController>();
        }
    }
}
