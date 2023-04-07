using Base.model;
using Base.session;
using Zhi.appointment;
using System.Collections.Generic;
using System.Linq;
using Zhi.search;

namespace Zhi.session
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
