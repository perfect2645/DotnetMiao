using Base.viewmodel.status;
using HttpProcessor.Container;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Shengzhi.login;
using Shengzhi.session;
using Utils;

namespace Shengzhi.search
{
    internal class SearchController
    {
        private DoctorController dateController;
        //private IntervalOnTime SearchInterval;

        public SearchController()
        {
            dateController = HttpServiceController.GetService<DoctorController>();
        }

        public void SearchMiao()
        {
            if (!GetDates())
            {
                return;
            }

            var scheduleList = MainSession.PlatformSession["scheduleList"] as List<Dictionary<string, object>>;
            var defaultUser = MainSession.Users.FirstOrDefault();
            foreach (var schedule in scheduleList)
            {
                Task.Factory.StartNew(() =>
                {
                    var miaoController = HttpServiceController.GetService<MiaoController>();
                    miaoController.SearchMiao(defaultUser, schedule);
                });
            }
        }

        private bool GetDates()
        {
            if (MainSession.PlatformSession.ContainsKey("scheduleList"))
            {
                return true;
            }

            var defaultUser = MainSession.Users.FirstOrDefault();

            var isDateGet = dateController.GetDoctors(defaultUser);
            return isDateGet;
        }
    }
}
