using Base.viewmodel.status;
using HttpProcessor.Container;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dongxihu.login;
using Dongxihu.session;
using Utils.datetime;

namespace Dongxihu.search
{
    internal class SearchController
    {
        public SearchController()
        {
        }

        public void SearchMiao()
        {
            MainSession.SetStatus(MiaoProgress.GettingMiao);
            var defaultDate = DateTimeUtil.GetTomorrow();
            var defaultUser = MainSession.Users.FirstOrDefault();
            GetMiao(defaultDate);
        }

        private void GetMiao(string date)
        {
            var miaoController = HttpServiceController.GetService<MiaoController>();

            var isMiaoGet = false;
            while(!isMiaoGet)
            {
                isMiaoGet = miaoController.SearchMiao(date);
                Thread.Sleep(500);
            }
        }
    }
}
