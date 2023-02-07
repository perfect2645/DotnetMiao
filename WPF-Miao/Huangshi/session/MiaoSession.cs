using Base.model;
using Base.session;
using Huangshi.appointment;
using Huangshi.search;
using System.Collections.Generic;

namespace Huangshi.session
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
