using Base.model;
using HttpProcessor.Container;
using hys020.appointment;
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
        internal static Dictionary<YuyueKey, YuyueController> YuyueControllerCache { get; private set; }

        static AppointSession()
        {

        }

        public static void Init()
        {
            YuyueControllerCache = new Dictionary<YuyueKey, YuyueController>();
            var dateList = (MainSession.PlatformSession["DateList"] as List<DspVal>).Select(x => x.Value).ToList();
            var timeList = (MainSession.PlatformSession["TimeList"] as List<DspVal>).Select(x => x.Value).ToList();
            foreach (var date in dateList)
            {
                foreach(var time in timeList)
                {
                    var key = new YuyueKey(date, time);
                    var yuyueContr = HttpServiceController.GetService<YuyueController>();
                    YuyueControllerCache.Add(key, yuyueContr);
                }
            }
        }
    }
}
