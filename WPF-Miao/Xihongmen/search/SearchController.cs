using Base.model;
using HttpProcessor.Container;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utils;
using Xihongmen.session;

namespace Xihongmen.search
{
    internal class SearchController
    {

        private readonly UserController userController;
        private readonly TimeController timeController;

        public SearchController()
        {
            userController = HttpServiceController.GetService<UserController>();
            timeController = HttpServiceController.GetService<TimeController>();
        }

        public void GetUser()
        {
            userController.GetUserAsync();
        }


        public void SearchAsync()
        {
            Task.Factory.StartNew(() => {
                if (!GetTime())
                {
                    return;
                }

                GetMiaoFromTime();
            });
        }

        private bool GetTime()
        {
            try
            {
                timeController.GetTime();

                var timeList = MainSession.PlatformSession["timeList"] as List<string>;
                return timeList.HasItem();
            }
            catch
            {
                return false;
            }
        }

        internal void GetMiaoFromDate()
        {
            var dateList = MainSession.PlatformSession["DateList"] as List<DspVal>;
            foreach(var date in dateList)
            {
                var timestamp = date.Value;
                var miaoController = MainSession.MiaoSession.GetController(timestamp);
                miaoController.GetMiaoAsync(timestamp);
            }
        }

        internal void GetMiaoFromTime()
        {
            var dateList = MainSession.PlatformSession["timeList"] as List<DspVal>;

        }
    }
}
