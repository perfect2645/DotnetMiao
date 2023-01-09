using Jikong.login;
using HttpProcessor.Container;
using System;
using System.Threading.Tasks;
using Jikong.session;
using Utils.timerUtil;
using Base.viewmodel.status;
using System.Threading;
using System.Linq;
using System.Collections.Generic;

namespace Jikong.search
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
                Thread.Sleep(200);
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

        private void GetMiao(string date, JikongLogin user)
        {
            var miaoController = HttpServiceController.GetService<MiaoController>();

            var isMiaoGet = false;
            while(!isMiaoGet)
            {
                isMiaoGet = miaoController.SearchMiao(date, user);
                Thread.Sleep(100);
            }
        }
    }
}
