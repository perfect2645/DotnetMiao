using Base.viewmodel.status;
using HttpProcessor.Container;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Shengzhi.login;
using Shengzhi.session;

namespace Shengzhi.search
{
    internal class SearchController
    {
        private DoctorController dateController;
        private MiaoController miaoController;
        //private IntervalOnTime SearchInterval;

        public SearchController()
        {
            miaoController = HttpServiceController.GetService<MiaoController>();
            dateController = HttpServiceController.GetService<DoctorController>();
        }

        public void SearchMiao()
        {
            if (!GetDates())
            {
                return;
            }
            var defaultUser = MainSession.Users.FirstOrDefault();
            miaoController.SearchMiaoAsync(defaultUser);
        }

        private bool GetDates()
        {
            if (MainSession.PlatformSession.ContainsKey("dates"))
            {
                return true;
            }

            var defaultUser = MainSession.Users.FirstOrDefault();

            var isDateGet = dateController.GetDoctors(defaultUser);
            return isDateGet;
        }
    }
}
