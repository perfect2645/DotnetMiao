using Base.viewmodel.status;
using HttpProcessor.Container;
using Jksx.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utils.datetime;

namespace Jksx.search
{
    internal class SearchController
    {

        public void SearchMiao()
        {
            MainSession.SetStatus(MiaoProgress.GettingMiao);
            var defaultUser = MainSession.Users.FirstOrDefault();
            GetMiao();
        }

        private void GetMiao()
        {
            var miaoController = HttpServiceController.GetService<MiaoController>();

            var isMiaoGet = false;
            while (!isMiaoGet)
            {
                isMiaoGet = miaoController.SearchMiao();
                Thread.Sleep(500);
            }
        }
    }
}
