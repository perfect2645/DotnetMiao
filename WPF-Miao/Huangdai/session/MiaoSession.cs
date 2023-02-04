using Base.model;
using Base.session;
using Huangdai.appointment;
using Huangdai.search;
using System.Collections.Generic;

namespace Huangdai.session
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
