using Base.model;
using Base.viewmodel.status;
using HttpProcessor.Container;
using Huangshi.login;
using Huangshi.session;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Huangshi.search
{
    internal class SearchController
    {
        private TimeController dateController;

        public SearchController()
        {
            dateController = HttpServiceController.GetService<TimeController>();
        }

        public void StartSearchInterval()
        {
            MainSession.SetStatus(MiaoProgress.GettingMiao);
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

        private void GetTime(string date, HuangshiLogin user)
        {
            var miaoController = HttpServiceController.GetService<TimeController>();

            var isMiaoGet = false;
            while(!isMiaoGet)
            {
                isMiaoGet = miaoController.GetDateAndTime(user, date);
                Thread.Sleep(1000);
            }
        }


        private void GetMiao(string date, HuangshiLogin user)
        {
            var miaoController = HttpServiceController.GetService<MiaoController>();

            var isMiaoGet = false;
            while (!isMiaoGet)
            {
                isMiaoGet = miaoController.SearchMiao(user, date);
                Thread.Sleep(1000);
            }
        }
    }
}
