using Base.model;
using Base.session;
using Zhi.appointment;
using System.Collections.Generic;
using System.Linq;

namespace Zhi.session
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
