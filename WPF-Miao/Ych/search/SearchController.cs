using Base.model;
using HttpProcessor.Container;
using Ych.session;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utils;

namespace Ych.search
{
    internal class SearchController
    { 
        private Dictionary<string, SearchMiaoController> _controllerList;

        public SearchController()
        {
            _controllerList = new Dictionary<string, SearchMiaoController>();

            var dateList = MainSession.PlatformSession["DateList"] as List<DspVal>;
            if (!dateList.HasItem())
            {
                MainSession.PrintLogEvent.Publish(this, "DateList Is empty, 请设置预约日期");
                return;
            }

            foreach(var date in dateList)
            {
                var skey = $"{date.Value}{Constants.ShangWu}";
                var shangwuController = MainSession.SearchSession.GetController(skey);
                shangwuController.BuildContent(date.Value);
                _controllerList.Add(skey, shangwuController);

                //var xkey = $"{date.Value}{Constants.XiaWu}";
                //var afternoonController = MainSession.SearchSession.GetController(xkey);
                //afternoonController.BuildContent(date.Value, Constants.XiaWu);
                //_controllerList.Add(xkey, afternoonController);
            }
        }

        public async Task GetUserInfo()
        {
            var userController = HttpServiceController.GetService<UserController>();
            await userController.GetUserAsync();
        }

        public void GetMiaoAsync()
        {
            foreach(var controller in _controllerList)
            {
                controller.Value.SearchInterval.StartIntervalOntime();
            }
        }
    }
}
