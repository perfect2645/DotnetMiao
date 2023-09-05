using Base.viewmodel.status;
using HttpProcessor.Container;
using Kuerle.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utils.datetime;

namespace Kuerle.search
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
            var indexController = HttpServiceController.GetService<IndexController>();

            var isMiaoGet = false;
            while (!isMiaoGet)
            {
                (string pidKey, string vidKey) = indexController.GetIndex();
                if(string.IsNullOrEmpty(pidKey) || string.IsNullOrEmpty(vidKey))
                {
                    MainSession.PrintLogEvent.Publish(this, "pid & vid not get, continue");
                }

                isMiaoGet = miaoController.SearchMiao();
                Thread.Sleep(1000);
            }
        }
    }
}
