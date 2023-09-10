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
            //var deptController = HttpServiceController.GetService<DepartmentController>();
            //var isDeptGet = false;
            //while (!isDeptGet)
            //{
            //    isDeptGet = deptController.SearchDepartment();
            //    if (!isDeptGet)
            //    {
            //        Thread.Sleep(1000);
            //    }
            //}

            var vaccineController = HttpServiceController.GetService<VaccineController>();
            var isVaccineGet = false;
            while (!isVaccineGet)
            {
                isVaccineGet = vaccineController.SearchVaccine();
                if (!isVaccineGet)
                {
                    Thread.Sleep(1000);
                }
            }

            var miaoController = HttpServiceController.GetService<MiaoController>();
            var isScheduleGet = false;
            while(!isScheduleGet)
            {
                isScheduleGet = miaoController.SearchMiao();
                Thread.Sleep(1000);
            }
        }
    }
}
