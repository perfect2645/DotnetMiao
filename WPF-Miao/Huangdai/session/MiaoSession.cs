using Base.model;
using Base.session;
using Jikong.appointment;
using Jikong.search;
using System.Collections.Generic;

namespace Jikong.session
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
