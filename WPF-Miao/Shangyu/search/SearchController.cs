using Base.viewmodel.status;
using HttpProcessor.Container;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Shangyu.login;
using Shangyu.session;
using Utils.datetime;
using Base.model;
using Shangyu.appointment;
using Utils;
using System;

namespace Shangyu.search
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
            var defaultDate = DateTimeUtil.GetToday();

            Task.Factory.StartNew(() =>
            {
                GetMiao(defaultDate);
            });
        }

        private void GetMiao(string date)
        {
            var scheduleController = HttpServiceController.GetService<ScheduleController>();

            var isScheduleGet = false;
            var scheduleOrderList = new List<Order>();
            while(!isScheduleGet)
            {
                (isScheduleGet, scheduleOrderList) = scheduleController.SearchSchedule(date);
                Thread.Sleep(1000);
            }
        }
    }
}
