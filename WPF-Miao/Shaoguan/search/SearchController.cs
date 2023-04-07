using Shaoguan.login;
using HttpProcessor.Container;
using System;
using System.Threading.Tasks;
using Shaoguan.session;
using Utils.timerUtil;
using Base.viewmodel.status;
using System.Threading;
using System.Linq;
using System.Collections.Generic;
using Base.model;
using Utils.number;

namespace Shaoguan.search
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

        public async Task SearchMiaoAsync()
        {
            MainSession.SetStatus(MiaoProgress.GettingMiao);

            var dateList = MainSession.PlatformSession["DateList"] as List<DspVal>;
            var defaultUser = MainSession.Users.FirstOrDefault();

            foreach (var date in dateList)
            {
                await Task.Factory.StartNew(() => GetMiao(date.Value, defaultUser));
            }
        }

        private void GetMiao(string date, ShaoguanLogin user)
        {
            var miaoController = HttpServiceController.GetService<MiaoController>();

            var isMiaoGet = false;
            while(!isMiaoGet)
            {
                isMiaoGet = miaoController.SearchMiao(date, user);
                Thread.Sleep(1000);
            }
        }
    }
}
