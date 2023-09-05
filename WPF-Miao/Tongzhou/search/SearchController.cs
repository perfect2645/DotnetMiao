using Base.viewmodel.status;
using HttpProcessor.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tongzhou.login;
using Tongzhou.session;
using Utils.datetime;

namespace Tongzhou.search
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
            GetSchedule(defaultUser);
        }

        private void GetSchedule(TongzhouLogin? defaultUser)
        {
            var scheduleController = HttpServiceController.GetService<ScheduleController>();
            scheduleController.GetScheduleAsync(defaultUser);
        }

        private void GetMiao(string date)
        {
            var miaoController = HttpServiceController.GetService<MiaoController>();

            var isMiaoGet = false;
            while(!isMiaoGet)
            {
                isMiaoGet = miaoController.SearchMiao(date);
                Thread.Sleep(500);
            }
        }
    }
}
