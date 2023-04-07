using Base.model;
using Base.session;
using System.Collections.Generic;
using Xihongmen.appointment;
using Xihongmen.search;

namespace Xihongmen.session
{
    internal class MiaoSession : ControllerSession<MiaoController>
    {
        public MiaoSession() : base()
        {
            var dateList = MainSession.PlatformSession["DateList"] as List<DspVal>;
            foreach(var date in dateList)
            {
                AddController($"{date.Value}");
            }
        }
    }
}
