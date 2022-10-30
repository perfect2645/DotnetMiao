using HttpProcessor.Client;
using HttpProcessor.Container;
using HttpProcessor.Content;
using Jkchegu.search.yzm;
using Jkchegu.session;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Jkchegu.appointment
{
    internal class AppointController : HttpClientBase
    {

        public bool IsSuccess { get; private set; }
        public YzmController YzmController { get; private set; }

        public AppointController(HttpClient httpClient) : base(httpClient)
        {
            YzmController = HttpServiceController.GetService<YzmController>();
        }

        public void Yuyue(List<Order> orderList)
        {
            var result = YuyueAsync(orderList).Result;
        }

        private async Task<int> YuyueAsync(List<Order> orderList)
        {
            for(var i = 1; i < 10; i++)
            {
                if (IsSuccess)
                {
                    JkSession.PrintLogEvent.Publish(this, $"预约结束，退出循环");
                    return 1;
                }
                foreach (var order in orderList)
                {
                    if (IsSuccess)
                    {
                        JkSession.PrintLogEvent.Publish(this, $"预约结束，退出循环");
                        return 1;
                    }
                    order.Yzm = await GetYzmAsync();
                    JkSession.PrintLogEvent.Publish(this, order.ToLogString());
                    var content = new AppointContent(order);
                    await AppointAsync(content);
                }
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
                    return;
                }

                content.BuildDefaultHeaders(Client);

                HttpDicResponse response = PostStringAsync(content, ContentType.String).Result;
                if (response == null)
                {
                    JkSession.PrintLogEvent.Publish(this, $"Appoint response is null");
                }

                JkSession.PrintLogEvent.Publish(this, $"预约完成");
                var result = response.JsonBody.RootElement.GetProperty("res").ToString();
                if (string.IsNullOrEmpty(result) || result.Contains("成功"))
                {
                    JkSession.PrintLogEvent.Publish(this, $"result:{result}，预约成功请查询确认");
                    IsSuccess = true;
                }

                JkSession.PrintLogEvent.Publish(this, $"result:{result}");
            }
            catch(Exception ex)
            {
                JkSession.PrintLogEvent.Publish(this, $"预约异常{ex.Message}");
            }
            
        }
    }
}
