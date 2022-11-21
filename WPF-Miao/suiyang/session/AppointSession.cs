using Base.model;
using Base.session;
using HttpProcessor.Client;
using HttpProcessor.Container;
using suiyang.appointment;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace suiyang.session
{
    public class AppointSession : ControllerSession<YuyueController>
    {
        public AppointSession() : base()
        {
            var dateList = (MainSession.PlatformSession["DateList"] as List<DspVal>).Select(x => x.Value).ToList();
            foreach (var date in dateList)
            {
                AddController(date);
            }
        }
    }
}
