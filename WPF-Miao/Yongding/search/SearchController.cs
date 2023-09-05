using Base.viewmodel.status;
using HttpProcessor.Container;
using Yongding.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utils.datetime;
using Base.model;

namespace Yongding.search
{
    internal class SearchController
    {

        public void SearchMiao(DspVal date)
        {
            MainSession.SetStatus(MiaoProgress.GettingMiao);
            var defaultUser = MainSession.Users.FirstOrDefault();
            GetMiao(date);
        }

        private void GetMiao(DspVal date)
        {
            var scheduleController = HttpServiceController.GetService<ScheduleController>();

            var isMiaoGet = false;
            while (!isMiaoGet)
            {
                isMiaoGet = scheduleController.SearchSchedule(date);
                Thread.Sleep(500);
            }
        }
    }
}
