using Base.model;
using Base.session;
using System.Collections.Generic;
using Xihongmen.appointment;

namespace Xihongmen.session
{
    internal class AppointSession : ControllerSession<YuyueController>
    {
        public AppointSession() : base()
        {
            var dateList = MainSession.PlatformSession["DateList"] as List<DspVal>;
            foreach(var date in dateList)
            {
                AddController($"{date.Value}");
            }
        }
    }
}
