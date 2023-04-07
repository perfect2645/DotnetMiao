using Base.viewmodel.status;
using HttpProcessor.Container;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HosFour.login;
using HosFour.session;

namespace HosFour.search
{
    internal class SearchController
    {
        private DateController dateController;
        //private IntervalOnTime SearchInterval;

        public SearchController()
        {
            dateController = HttpServiceController.GetService<DateController>();
            //SearchInterval = new IntervalOnTime(SearchAsync, "search", 200);
        }

        public void StartSearchInterval()
        {
            MainSession.SetStatus(MiaoProgress.GettingMiao);
            //SearchInterval.StartIntervalOntime();
        }

        public void SearchMiao()
        {
            MainSession.SetStatus(MiaoProgress.GettingMiao);

            GetDates();
            GetTimes();
        }

        private void GetDates()
        {
            var defaultUser = MainSession.Users.FirstOrDefault();

            var isDateGet = false;
            while (!isDateGet)
            {
                isDateGet = dateController.GetDate(defaultUser);
                Thread.Sleep(500);
            }
        }

        private void GetTimes()
        {
            var dateList = MainSession.PlatformSession["orderDates"] as List<string>;
            var defaultUser = MainSession.Users.FirstOrDefault();

            foreach (var date in dateList)
            {
                Task.Factory.StartNew(() => GetMiao(date, defaultUser));
            }
        }

        private void GetMiao(string date, HosFourLogin user)
        {
            var miaoController = HttpServiceController.GetService<MiaoController>();

            var isMiaoGet = false;
            while(!isMiaoGet)
            {
                isMiaoGet = miaoController.SearchMiao(date, user);
                Thread.Sleep(500);
            }
        }
    }
}
