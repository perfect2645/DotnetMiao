using Base.model;
using HttpProcessor.Container;
using HuSheng.session;
using System;
using System.Collections.Generic;

namespace HuSheng.search
{
    internal class SearchController
    {
        public SearchController()
        {
            BuildSearchList();
        }

        private void BuildSearchList()
        {
            var startTime = MainSession.PlatformSession["StartTime"] as DateTime?;
            var preSetDateList = MainSession.PlatformSession["PreDateList"] as List<DspVal>;
            foreach (var preSetDate in preSetDateList)
            {
                var date = preSetDate.Value;
                var dateController = HttpServiceController.GetService<SelectDateController>();
                //dateController.Init(date, startTime);

                MainSession.PrintLogEvent.Publish(this, $"初始化查询：{date}");

            }
            MainSession.PrintLogEvent.Publish(this, $"程序已就绪 开始时间：{startTime}");
        }
    }
}
