using Base.model;
using Base.session;
using Ych.appointment;
using System.Collections.Generic;
using System.Linq;

namespace Ych.session
{
    public class AppointSession : ControllerSession<YuyueController>
    {
        public AppointSession() : base()
        {
            var dateList = (MainSession.PlatformSession["DateList"] as List<DspVal>).Select(x => x.Value).ToList();
            foreach (var date in dateList)
            {
                AddController($"{date}|1"); // 上午
                AddController($"{date}|2"); // 下午
            }
        }
    }
}
