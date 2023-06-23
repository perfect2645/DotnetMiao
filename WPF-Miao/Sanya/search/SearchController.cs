using Base.viewmodel.status;
using HttpProcessor.Container;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Sanya.login;
using Sanya.session;
using Utils.datetime;
using Base.model;
using Sanya.appointment;
using Utils;

namespace Sanya.search
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
                Thread.Sleep(600);
            }
            var date = vaccineController.Date;

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
