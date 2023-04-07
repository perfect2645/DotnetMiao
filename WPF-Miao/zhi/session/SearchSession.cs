using Base.model;
using Base.session;
using Zhi.search;
using System.Collections.Generic;
using System.Linq;

namespace Zhi.session
{
    public class SearchSession : ControllerSession<SearchMiaoController>
    {
        public SearchSession() : base()
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
