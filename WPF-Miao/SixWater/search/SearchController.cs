using Base.viewmodel.status;
using HttpProcessor.Container;
using SixWater.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utils.datetime;

namespace SixWater.search
{
    internal class SearchController
    {

        public void SearchMiao(string date)
        {
            if (!DateTimeUtil.IsEqualOrGreaterThanToday(date))
            {
                date = DateTimeUtil.GetToday();
            }

            MainSession.SetStatus(MiaoProgress.GettingMiao);
            GetMiao(date);
        }

        private void GetMiao(string date)
        {
            var scheduleController = HttpServiceController.GetService<ScheduleController>();

            var isScheduleGet = false;
            var scheduleId = string.Empty;
            while (!isScheduleGet)
            {
                (isScheduleGet, scheduleId) = scheduleController.SearchSchedule(date);
                Thread.Sleep(1000);
            }

            if (string.IsNullOrEmpty(scheduleId))
            {
                GetMiao(date);
                return;
            }
            var miaoController = HttpServiceController.GetService<MiaoController>();
            var isMiaoGet = false;
            while (!isMiaoGet)
            {
                isMiaoGet = miaoController.SearchMiao(date, scheduleId);
                Thread.Sleep(1000);
            }
        }
    }
}
