using HttpProcessor.Container;
using System;
using System.Threading.Tasks;
using Utils.timerUtil;

namespace gaoxin.search
{
    internal class SearchController
    {
        private SearchMiaoController _morningController;
        private SearchMiaoController _afternoonController;

        private IntervalOnTime MorningInterval;
        private IntervalOnTime AfternoonInterval;

        public SearchController()
        {
            _morningController = HttpServiceController.GetService<SearchMiaoController>();
            _morningController.BuildContent("上午");

            _afternoonController = HttpServiceController.GetService<SearchMiaoController>();
            _afternoonController.BuildContent("下午");

            MorningInterval = new IntervalOnTime(SearchMorningAsync, "上午", 200);
            AfternoonInterval = new IntervalOnTime(SearchAfternoon, "下午", 200);
        }

        public async Task GetUserInfoAsync()
        {
            var tokenController = HttpServiceController.GetService<AppletTokenController>();
            var tokenContent = new AppletTokenContent("https://ymglfw.care4u.cn/npApii/auth/getAppletToken");
            tokenController.BuildClientHeaders(tokenContent);
            await tokenController.ProcessAsync(tokenContent);

            var userController = HttpServiceController.GetService<UserController>();
            userController.GetUserAsync();
        }

        public void GetMiaoAsync()
        {
            //var isMiaoGet = await _morningController.SearchMiaoAsync();
            //if (isMiaoGet)
            //{

            //}
            MorningInterval.StartIntervalOntime();
            AfternoonInterval.StartIntervalOntime();
        }

        private async void SearchMorningAsync()
        {
            var isMiaoGet = await _morningController.SearchMiaoAsync();
            if (isMiaoGet)
            {
                MorningInterval.StopInterval();
            }
        }

        private async void SearchAfternoon()
        {
            var isMiaoGet = await _afternoonController.SearchMiaoAsync();
            if (isMiaoGet)
            {
                AfternoonInterval.StopInterval();
            }
        }
    }
}
