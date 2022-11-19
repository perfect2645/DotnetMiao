using Base.model;
using HttpProcessor.Container;
using hys020.appointment.Yuyue;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace hys020.session
{
    public static class AppointSession
    {
        internal static ConcurrentDictionary<string, YuyueController> YuyueControllerCache { get; private set; }

        static AppointSession()
        {

        }

        public static void Init()
        {
            YuyueControllerCache = new ConcurrentDictionary<string, YuyueController>();
            var dateList = (MainSession.PlatformSession["DateList"] as List<DspVal>).Select(x => x.Value).ToList();
            var timeList = (MainSession.PlatformSession["TimeList"] as List<DspVal>).Select(x => x.Value).ToList();
            foreach (var date in dateList)
            {
                foreach(var time in timeList)
                {
                    AddController(date, time);
                }
            }
        }

        private static YuyueController AddController(string? date, string? time)
        {
            var key = new YuyueKey(date, time).Key;
            var yuyueContr = HttpServiceController.GetService<YuyueController>();
            YuyueControllerCache.AddOrUpdate(key, yuyueContr);

            return yuyueContr;
        }

        public static YuyueController GetController(string date, string time)
        {
            try
            {
                var key = new YuyueKey(date, time).Key;
                if (!YuyueControllerCache.ContainsKey(key))
                {
                    return AddController(date, time);
                }
 
                return YuyueControllerCache[key];
            }
            catch(Exception ex)
            {
                Logging.GLog.Logger.Error(ex);
                return null;
            }
        }
    }
}
