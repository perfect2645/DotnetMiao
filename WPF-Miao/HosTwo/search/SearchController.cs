using Base.viewmodel.status;
using HttpProcessor.Container;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HosTwo.login;
using HosTwo.session;
using Utils.datetime;
using Base.model;
using HosTwo.appointment;
using Utils;

namespace HosTwo.search
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
            var scheduleController = HttpServiceController.GetService<ScheduleController>();

            var isScheduleGet = false;
            var scheduleOrderList = new List<Order>();
            while(!isScheduleGet)
            {
                (isScheduleGet, scheduleOrderList) = scheduleController.SearchSchedule(date);
                Thread.Sleep(200);
            }

            if (!scheduleOrderList.HasItem())
            {
                GetMiao(date);
                return;
            }

            foreach(var schedule in scheduleOrderList)
            {
                var miaoController = HttpServiceController.GetService<MiaoController>();
                miaoController.SearchMiaoAsync(schedule);
            }
        }
    }
}
