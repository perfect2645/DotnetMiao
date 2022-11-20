using Base.model;
using HttpProcessor.Container;
using suiyang.appointment;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace suiyang.session
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
            foreach (var date in dateList)
            {
                AddController(date);
            }
        }

        private static YuyueController AddController(string date)
        {
            var yuyueContr = HttpServiceController.GetService<YuyueController>();
            YuyueControllerCache.AddOrUpdate(date, yuyueContr);

            return yuyueContr;
        }

        public static YuyueController GetController(string date)
        {
            try
            {
                if (!YuyueControllerCache.ContainsKey(date))
                {
                    return AddController(date);
                }
 
                return YuyueControllerCache[date];
            }
            catch(Exception ex)
            {
                Logging.GLog.Logger.Error(ex);
                return null;
            }
        }
    }
}
