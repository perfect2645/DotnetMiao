using Base.model;
using HttpProcessor.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utils;
using Xihongmen.appointment;
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

        public async Task GetUserAsync()
        {
            await userController.GetUserAsync();
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

        internal void TryGetCookie()
        {
            var cookieController = HttpServiceController.GetService<YuyueController>();

            var deptId = MainSession.PlatformSession.GetString(Constants.DeptId);
            var deptName = MainSession.PlatformSession.GetString(Constants.DeptName);
            var deptNameEncode = UnicodeConverter.Encode(deptName, true);
            var hospitalId = MainSession.PlatformSession.GetString(Constants.HospitalId);

            var userInfo = MainSession.PlatformSession["user"] as Dictionary<string, object>;
            var userId = MainSession.PlatformSession.GetString(Constants.UserId);
            var userName = userInfo.GetString("child_name");
            var userNameEncode = UnicodeConverter.Encode(userName, true);

            var dateList = MainSession.PlatformSession["DateList"] as List<DspVal>;
            var defaultDate = dateList.FirstOrDefault();

            var dummyOrder = new Order
            {
                TypeId = deptId,
                Date = defaultDate.Value,
                TypeTitle = deptNameEncode,
                UserId = userId,
                UserName = userNameEncode,
                TimeType = "22",
            };
            var content = new YuyueContent(dummyOrder);
            cookieController.BuildClientHeaders(content);
            cookieController.AppointAsync(content);
        }
    }
}
