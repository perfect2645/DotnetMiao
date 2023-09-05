using Base.viewmodel.status;
using HttpProcessor.Container;
using Haikou.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utils.datetime;
using Utils;

namespace Haikou.search
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
            //var dateController = HttpServiceController.GetService<DateController>();

            //var dateList = new List<string>();
            //while (!dateList.HasItem())
            //{
            //    dateList = dateController.GetDateList();
            //    Thread.Sleep(500);
            //}

            var miaoController = HttpServiceController.GetService<MiaoController>();

            var isMiaoGet = false;
            while (!isMiaoGet)
            {
                var dateController = HttpServiceController.GetService<DateController>();
                var dateList = dateController.GetDateList();
                if (!dateList.HasItem())
                {
                    Thread.Sleep(800);
                    continue;
                }

                isMiaoGet = miaoController.SearchMiao(dateList);
                Thread.Sleep(500);
            }
        }
    }
}
