using Base.viewmodel.status;
using HttpProcessor.Container;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using B114.login;
using B114.session;
using Utils.datetime;
using Base.model;
using B114.appointment;
using Utils;

namespace B114.search
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
                Thread.Sleep(300);
                Task.Factory.StartNew(() =>
                {
                    GetMiao(dateItem.Value);
                });
            }
        }

        private void GetMiao(string date)
        {
            var miaoController = HttpServiceController.GetService<MiaoController>();
            var isScheduleGet = false;
            while(!isScheduleGet)
            {
                isScheduleGet = miaoController.SearchMiao(date);
                Thread.Sleep(1000);
            }
        }
    }
}
