using Base.model;
using HttpProcessor.Container;
using Zhi.session;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utils;

namespace Zhi.search
{
    internal class SearchController
    {
        private readonly Dictionary<string, DoctorController> _doctorControllerList;
        private Dictionary<string, SearchMiaoController> _miaoControllerList;

        public SearchController()
        {
            _doctorControllerList = new Dictionary<string, DoctorController>();
            _miaoControllerList = new Dictionary<string, SearchMiaoController>();

            var dateList = MainSession.PlatformSession["DateList"] as List<DspVal>;
            if (!dateList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, "DateList Is empty, 请设置预约日期");
                return;
            }

            foreach(var date in dateList)
            {
                var docController = MainSession.DoctorSession.GetController(date.Value);
                docController.BuildContent(date.Value);
                _doctorControllerList.Add(date.Value, docController);

                //var skey = $"{date.Value}|1";
                //var morningController = MainSession.SearchSession.GetController(skey);
                //morningController.BuildContent(date.Value, "1");
                //_miaoControllerList.Add(skey, morningController);

                //var xkey = $"{date.Value}|2";
                //var afternoonController = MainSession.SearchSession.GetController(xkey);
                //afternoonController.BuildContent(date.Value, "2");
                //_miaoControllerList.Add(xkey, afternoonController);
            }
        }

        public async Task GetUserInfo()
        {
            var userController = HttpServiceController.GetService<UserController>();
            await userController.GetUserAsync();
        }

        public void GetDoctorInfo()
        {
            foreach (var controller in _doctorControllerList)
            {
                controller.Value.SearchInterval.StartIntervalOntime();
            }
        }

        public void GetMiaoAsync()
        {
            foreach(var controller in _miaoControllerList)
            {
                controller.Value.SearchInterval.StartIntervalOntime();
            }
        }
    }
}
