using Base.viewmodel.status;
using HttpProcessor.Container;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Lzy.login;
using Lzy.session;
using Utils.datetime;
using Base.model;
using Lzy.appointment;
using Utils;

namespace Lzy.search
{
    internal class SearchController
    {
        public SearchController()
        {
        }

        public void SearchMiao()
        {
            MainSession.SetStatus(MiaoProgress.GettingMiao);
            var defaultUser = MainSession.Users.FirstOrDefault();
            var dateList = MainSession.PlatformSession["DateList"] as List<DspVal>;

            foreach(var dateItem in dateList)
            {
                Task.Factory.StartNew(() =>
                {
                    GetMiao(dateItem.Value);
                });
            }
        }

        private void GetMiao(string date)
        {
            var miaoController = HttpServiceController.GetService<MiaoController>();

            var isMiaoGet = false;
            while(!isMiaoGet)
            {
                isMiaoGet =  miaoController.SearchMiao();
                Thread.Sleep(1000);
            }
        }
    }
}
