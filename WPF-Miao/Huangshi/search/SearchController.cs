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
        private DateController dateController;

        public SearchController()
        {
            dateController = HttpServiceController.GetService<DateController>();
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

        private void GetDateTime(string date, HuangshiLogin user)
        {
            var miaoController = HttpServiceController.GetService<DateController>();

            var isMiaoGet = false;
            while(!isMiaoGet)
            {
                isMiaoGet = miaoController.GetDateAndTime(user, date);
                Thread.Sleep(1000);
            }
        }
    }
}
