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
        public AppointController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void Yuyue(List<Order> orderList)
        {
            var result = YuyueAsync(orderList).Result;
        }

        private async Task<int> YuyueAsync(List<Order> orderList)
        {
            foreach (var order in orderList)
            {
                order.Yzm = await GetYzmAsync();
                JkSession.PrintLogEvent.Publish(this, order.ToLogString());
                var content = new AppointContent(order);
                await AppointAsync(content);
            }

            return 0;
        }

        private async Task<string> GetYzmAsync()
        {
            var yzmController = HttpServiceController.GetService<YzmController>();
            return await yzmController.GetYzmAsync();
        }

        public async Task AppointAsync(AppointContent content)
        {
            await Task.Factory.StartNew(() => Appoint(content));
        }

        public void Appoint(AppointContent content)
        {
            try
            {
                content.BuildDefaultHeaders(Client);

                HttpDicResponse response = PostStringAsync(content, ContentType.String).Result;
                if (response == null)
                {
                    JkSession.PrintLogEvent.Publish(this, $"Appoint response is null");
                }

                JkSession.PrintLogEvent.Publish(this, $"预约完成");
                var result = response.JsonBody.RootElement.GetProperty("res").ToString();
                if (string.IsNullOrEmpty(result))
                {
                    JkSession.PrintLogEvent.Publish(this, $"result:预约申请提交成功");
                    return;
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
