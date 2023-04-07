using Base.model;
using HttpProcessor.Client;
using HttpProcessor.Container;
using Jkchegu.session;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Jkchegu.search
{
    internal class SearchController : HttpClientBase
    {
        public SearchController(HttpClient httpClient) : base(httpClient)
        {
            BuildSearchList();
        }

        private void BuildSearchList()
        {
            var startTime = JkSession.MiaoSession["StartTime"] as DateTime?;
            var preSetDateList = JkSession.PlatformSession["PreDateList"] as List<DspVal>;
            foreach(var preSetDate in preSetDateList)
            {
                var date = preSetDate.Value;
                var dateCountController = HttpServiceController.GetService<DateCountController>();
                dateCountController.Init(date, startTime);

                JkSession.PrintLogEvent.Publish(this, $"初始化查询：{date}");

            }
            JkSession.PrintLogEvent.Publish(this, $"程序已就绪 开始时间：{startTime}");
        }
    }
}
