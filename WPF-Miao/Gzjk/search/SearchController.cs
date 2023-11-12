using Base.viewmodel.status;
using HttpProcessor.Container;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Gzjk.login;
using Gzjk.session;
using Utils.datetime;
using Base.model;
using Gzjk.appointment;
using Utils;

namespace Gzjk.search
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

            Task.Factory.StartNew(() =>
            {
                GetDate();
            });
        }

        private void GetDate()
        {
            var vaccineController = HttpServiceController.GetService<DateController>();
            var isVaccineGet = false;
            while (!isVaccineGet)
            {
                if (MainSession.GetStatus() == MiaoProgress.MiaoGet)
                {
                    break;
                }
                if (MainSession.GetStatus() == MiaoProgress.AppointEnd)
                {
                    break;
                }
                isVaccineGet = vaccineController.SearchDate();
                if (isVaccineGet)
                {
                    break;
                }
                Thread.Sleep(600);
            }
            var date = vaccineController.Date;

            GetSchedule(date);
        }

        private void GetSchedule(string date)
        {
            var miaoController = HttpServiceController.GetService<MiaoController>();
            var isScheduleGet = false;
            isScheduleGet = miaoController.SearchMiao(date);
            if (!isScheduleGet)
            {
                SearchMiao();
            }
        }
    }
}
