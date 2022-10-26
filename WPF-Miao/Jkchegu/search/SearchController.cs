using Base.model;
using HttpProcessor.Client;
using HttpProcessor.Container;
using HttpProcessor.Content;
using Jkchegu.appointment;
using Jkchegu.search.yzm;
using Jkchegu.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Utils;
using Utils.json;
using Utils.stringBuilder;
using Utils.timerUtil;

namespace Jkchegu.search
{
    internal class SearchController : HttpClientBase
    {
        private List<SearchProcessor> SearchProcessorList = new List<SearchProcessor>();

        private bool isGetMiao = false;
        private bool isGetYzm = false;

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

                Thread.Sleep(50);
            }
            JkSession.PrintLogEvent.Publish(this, $"程序已启动 开始时间：{startTime}");
        }

        private async Task Yuyue()
        {
            if (!isGetYzm)
            {
                isGetYzm = true;
                await GetYzmAsync();
            }

            var appointController = HttpServiceController.GetService<AppointController>();
            appointController.AppointAsync();
        }

        private async Task GetYzmAsync()
        {
            var yzmController = HttpServiceController.GetService<YzmController>();
            await yzmController.GetYzmAsync();
        }

    }
}
