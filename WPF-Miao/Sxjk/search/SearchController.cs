using Base.viewmodel.status;
using HttpProcessor.Container;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Sxjk.login;
using Sxjk.session;
using Utils.datetime;
using Base.model;
using Sxjk.appointment;
using Utils;

namespace Sxjk.search
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
                GetMiao();
            });
        }

        private void GetMiao()
        {
            var vaccineController = HttpServiceController.GetService<VaccineController>();
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
                isVaccineGet = vaccineController.SearchVaccine();

                if (!isVaccineGet)
                {
                    Thread.Sleep(500);
                }
            }

            var date = vaccineController.Date;

            var miaoController = HttpServiceController.GetService<MiaoController>();
            var isScheduleGet = false;
            while(!isScheduleGet)
            {
                isScheduleGet = miaoController.SearchMiao(date);
                Thread.Sleep(500);
            }
        }
    }
}
