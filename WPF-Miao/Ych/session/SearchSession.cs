using Base.model;
using Base.session;
using Ych.search;
using System.Collections.Generic;
using System.Linq;

namespace Ych.session
{
    public class SearchSession : ControllerSession<SearchMiaoController>
    {
        public SearchSession() : base()
        {
            var dateList = (MainSession.PlatformSession["DateList"] as List<DspVal>).Select(x => x.Value).ToList();
            foreach (var date in dateList)
            {
                AddController($"{date}{Constants.ShangWu}");
                AddController($"{date}{Constants.XiaWu}");
            }
        }
    }
}
