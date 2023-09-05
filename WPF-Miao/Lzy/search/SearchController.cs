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

            foreach(var dateItem in MainSession.DateList)
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

            var isMiaoGet =  miaoController.SearchMiao(date);

            if (!isMiaoGet)
            {
                MainSession.PrintLogEvent.Publish(this, $"未查到苗date={date}");
            }
        }
    }
}
