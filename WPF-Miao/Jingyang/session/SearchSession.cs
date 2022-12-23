using Base.model;
using Base.session;
using Jingyang.search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jingyang.session
{
    internal class SearchSession : ControllerSession<GetMiaoController>
    {
        public SearchSession() : base()
        {
            var dateList = (MainSession.PlatformSession["DateList"] as List<DspVal>).Select(x => x.Value).ToList();
            foreach (var date in dateList)
            {
                var controller = AddController(date);
            }
        }
    }
}
