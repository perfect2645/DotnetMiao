using Base.viewmodel.status;
using HttpProcessor.Container;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dxm.login;
using Dxm.session;
using Utils.datetime;
using Base.model;
using Dxm.appointment;
using Utils;

namespace Dxm.search
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
            var vaccineController = HttpServiceController.GetService<VaccineController>();
            var isVaccineGet = false;
            while (!isVaccineGet)
            {
                Thread.Sleep(800);
                if (MainSession.GetStatus() == MiaoProgress.MiaoGet)
                {
                    break;
                }
                if (MainSession.GetStatus() == MiaoProgress.AppointEnd)
                {
                    break;
                }
                isVaccineGet = vaccineController.SearchVaccine();
            }

            var dateController = HttpServiceController.GetService<DateController>();
            var isDateGet = false;
            while (!isDateGet)
            {
                Thread.Sleep(500);
                if (MainSession.GetStatus() == MiaoProgress.AppointEnd)
                {
                    break;
                }
                isVaccineGet = dateController.SearchDate(date);
            }

            var targetDate = dateController.Date;
            var miaoController = HttpServiceController.GetService<MiaoController>();
            var isScheduleGet = false;
            while(!isScheduleGet)
            {
                isScheduleGet = miaoController.SearchMiao(targetDate);
                Thread.Sleep(1000);
            }
        }
    }
}
