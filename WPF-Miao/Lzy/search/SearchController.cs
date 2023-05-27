using Base.model;
using HttpProcessor.Container;
using Lzy.session;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Lzy.search
{
    internal class SearchController
    {
        public SearchController()
        {
        }

        public void SearchMiao(DspVal selectedDate)
        {
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
                isMiaoGet =  miaoController.SearchMiao(date);
                Thread.Sleep(1000);
            }
        }
    }
}
