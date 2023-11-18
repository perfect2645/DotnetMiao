using Base.viewmodel.status;
using HttpProcessor.Container;
using Rika.session;
using System.Linq;
using System.Threading;

namespace Rika.search
{
    internal class SearchController
    {

        public void SearchMiao()
        {
            MainSession.SetStatus(MiaoProgress.GettingMiao);
            var defaultUser = MainSession.Users.FirstOrDefault();
            GetMiao();
        }

        private void GetMiao()
        {
            GetDateSchedule();

            var miaoController = HttpServiceController.GetService<MiaoController>();

            var isMiaoGet = false;
            while (!isMiaoGet)
            {
                isMiaoGet = miaoController.SearchMiao();
                Thread.Sleep(3000);
            }
        }

        private void GetDateSchedule()
        {
            var dateController = HttpServiceController.GetService<DateController>();
            var indexStart = 1;
            var maxIndex = 7;

            var isScheduleGet = false;
            while(!isScheduleGet)
            {
                for (var i = indexStart; i < maxIndex; i++)
                {
                    Thread.Sleep(500);
                    isScheduleGet = dateController.SearchByDate(i);
                    if (isScheduleGet)
                    {
                        return;
                    }
                }
            }
        }
    }
}
