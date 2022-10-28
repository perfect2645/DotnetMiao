using HttpProcessor.Client;
using HttpProcessor.Content;
using hys020.session;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace hys020.appointment
{
    internal class AppointController : HttpClientBase
    {
        public AppointController(HttpClient httpClient) : base(httpClient)
        {
        }

        internal void Yuyue(List<Order> orderList)
        {

        }

        public async Task AppointAsync(Order order)
        {
            await Task.Factory.StartNew(() => Appoint(order));
        }

        public void Appoint(Order order)
        {
            var url = order.OrderUrl;

            try
            {
                var content = new AppointContent(url, order);

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
