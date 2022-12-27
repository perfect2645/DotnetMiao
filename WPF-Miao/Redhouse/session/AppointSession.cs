using Base.model;
using Base.session;
using HttpProcessor.Container;
using Redhouse.appointment.Yuyue;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Redhouse.session
{
    public class AppointSession : ControllerSession<YuyueController>
    {
        public AppointSession() : base()
        {
            var dateList = (MainSession.PlatformSession["DateList"] as List<DspVal>).Select(x => x.Value).ToList();
            foreach (var date in dateList)
            {
                AddController($"{date}");
            }
        }
    }
}
