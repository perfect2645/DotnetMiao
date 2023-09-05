using Base.viewmodel.status;
using HttpProcessor.Container;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dalian.login;
using Dalian.session;
using Utils.datetime;
using Base.model;
using Dalian.appointment;
using Utils;

namespace Dalian.search
{
    internal class SearchController
    {
        public SearchController()
        {
        }

        public void SearchMiao()
        {
            MainSession.SetStatus(MiaoProgress.GettingMiao);

            Task.Factory.StartNew(() =>
            {
                GetMiao();
            });
        }

        private void GetMiao()
        {
            //var scheduleController = HttpServiceController.GetService<ScheduleController>();

            //var isScheduleGet = false;
            //var dateList = new List<string>();
            //while (!isScheduleGet)
            //{
            //    (isScheduleGet, dateList) = scheduleController.SearchSchedule();
            //    Thread.Sleep(1000);
            //}

            //if (!dateList.HasItem())
            //{
            //    GetMiao();
            //    return;
            //}

            var isMiaoGet = false;
            var miaoController = HttpServiceController.GetService<MiaoController>();

            var dateList1 = MainSession.PlatformSession["DateList"] as List<DspVal>;

            while (!isMiaoGet)
            {
                //isMiaoGet = miaoController.SearchMiao(dateList1.LastOrDefault().Value);
                miaoController.SearchMiao(dateList1.LastOrDefault().Value);
                Thread.Sleep(1000);
            }
        }
    }
}
