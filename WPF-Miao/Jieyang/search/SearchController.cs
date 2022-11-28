using HttpProcessor.Container;
using System.Threading.Tasks;
using Utils.timerUtil;

namespace jieyang.search
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

        public async Task SearchAsync()
        {
            var isMiaoGet = await _morningController.SearchMiaoAsync();
            if (isMiaoGet)
            {

            }
            //MorningInterval.StartIntervalOntime();
            //AfternoonInterval.StartIntervalOntime();
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
