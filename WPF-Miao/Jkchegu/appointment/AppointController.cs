using HttpProcessor.Client;
using HttpProcessor.Container;
using HttpProcessor.Content;
using Jkchegu.search.yzm;
using Jkchegu.session;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Utils.timerUtil;

namespace Jkchegu.appointment
{
    internal class AppointController : HttpClientBase
    {
        private IntervalOnTime _intervalOnTime;
        public bool IsSuccess { get; private set; }
        public YzmController YzmController { get; private set; }

        private int loopCount = 0;

        public AppointController(HttpClient httpClient) : base(httpClient)
        {
            YzmController = HttpServiceController.GetService<YzmController>();
            _intervalOnTime = new IntervalOnTime("车谷预约", 2000);
        }

        public void Yuyue(List<Order> orderList)
        {
            var result = YuyueAsync(orderList).Result;
            _intervalOnTime.StartIntervalOntime(() =>
            {
                var result = YuyueAsync(orderList).Result;
            });
        }

        private async Task<int> YuyueAsync(List<Order> orderList)
        {
            loopCount++;
            JkSession.PrintLogEvent.Publish(this, $"第{loopCount}次预约循环，orderCount{orderList.Count}");
            if (IsSuccess)
            {
                _intervalOnTime.StopInterval();
                JkSession.PrintLogEvent.Publish(this, $"预约结束，退出循环");
                return 1;
            }
            foreach (var order in orderList)
            {
                if (IsSuccess)
                {
                    _intervalOnTime.StopInterval();
                    JkSession.PrintLogEvent.Publish(this, $"预约结束，退出循环");
                    return 1;
                }
                order.Yzm = await GetYzmAsync();
                Log(order.ToLogString());
                var content = new AppointContent(order);
                await AppointAsync(content);
            }

            return 0;
        }

        private async Task<string> GetYzmAsync()
        {
            return await YzmController.GetYzmAsync();
        }

        public async Task AppointAsync(AppointContent content)
        {
            await Task.Factory.StartNew(() => Appoint(content));
        }

        public void Appoint(AppointContent content)
        {
            try
            {
                if (IsSuccess)
                {
                    _intervalOnTime.StopInterval();
                    return;
                }

                content.BuildDefaultHeaders(Client);

                HttpDicResponse response = PostStringAsync(content, ContentType.String).Result;
                if (response == null)
                {
                    JkSession.PrintLogEvent.Publish(this, $"Appoint response is null");
                }

                var result = response.JsonBody.RootElement.GetProperty("res").ToString();
                if (string.IsNullOrEmpty(result) || result.Contains("成功"))
                {
                    JkSession.PrintLogEvent.Publish(this, $"result:{result}，预约成功请查询确认");
                    IsSuccess = true;
                }

                if (string.IsNullOrEmpty(result) || result.Contains("存在已有"))
                {
                    JkSession.PrintLogEvent.Publish(this, $"result:{result}，预约成功请查询确认");
                    IsSuccess = true;
                }

                //JkSession.PrintLogEvent.Publish(this, $"result:{result}");
            }
            catch(Exception ex)
            {
                JkSession.PrintLogEvent.Publish(this, $"预约异常{ex.Message}");
            }
            
        }
    }
}
