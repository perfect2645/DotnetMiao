using HttpProcessor.Client;
using HttpProcessor.Content;
using renren.session;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace renren.appointment
{
    internal class AppointController : HttpClientBase
    {
        public bool IsSuccess { get; private set; }
        public AppointController(HttpClient httpClient) : base(httpClient)
        {
        }

        internal void Yuyue(List<Order> orderList)
        {
            var result  = YuyueAsync(orderList).Result;
        }

        private async Task<int> YuyueAsync(List<Order> orderList)
        {
            for (var i = 1; i < 2; i++)
            {
                if (IsSuccess)
                {
                    MainSession.PrintLogEvent.Publish(this, $"预约结束，退出循环");
                    return 1;
                }
                foreach (var order in orderList)
                {
                    if (IsSuccess)
                    {
                        MainSession.PrintLogEvent.Publish(this, $"预约结束，退出循环");
                        return 1;
                    }
                    MainSession.PrintLogEvent.Publish(this, order.ToLogString());
                    var content = new AppointContent(order);

                    Thread.Sleep(1000);
                    await AppointAsync(content);
                }
            }

            return 0;
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
                    MainSession.PrintLogEvent.Publish(this, $"Appoint response is null");
                }

                MainSession.PrintLogEvent.Publish(this, $"预约完成");
                var result = response.JsonBody.RootElement.GetProperty("res").ToString();
                if (string.IsNullOrEmpty(result))
                {
                    MainSession.PrintLogEvent.Publish(this, $"result:预约申请提交成功");
                    return;
                }
                MainSession.PrintLogEvent.Publish(this, $"result:{result}");
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"预约异常{ex.Message}");
            }
        }

    }
}
