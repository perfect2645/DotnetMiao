using Base.model;
using Base.session;
using Ych.appointment;
using System.Collections.Generic;
using System.Linq;
using Ych.search;

namespace Ych.session
{
    internal class DoctorSession : ControllerSession<DoctorController>
    {
        public DoctorSession() : base()
        {
            var dateList = (MainSession.PlatformSession["DateList"] as List<DspVal>).Select(x => x.Value).ToList();
            foreach (var date in dateList)
            {
                AddController($"{date}"); // 上午
            }
        }
    }
}
