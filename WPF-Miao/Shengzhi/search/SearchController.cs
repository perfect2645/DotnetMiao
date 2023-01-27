using Base.viewmodel.status;
using HttpProcessor.Container;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Shengzhi.login;
using Shengzhi.session;

namespace Shengzhi.search
{
    internal class SearchController
    {
        private MiaoController miaoController;
        //private IntervalOnTime SearchInterval;

        public SearchController()
        {
            miaoController = HttpServiceController.GetService<MiaoController>();
        }

        public void SearchMiao()
        {
            var defaultUser = MainSession.Users.FirstOrDefault();
            miaoController.SearchMiaoAsync(defaultUser);
        }
    }
}
