using HttpProcessor.Client;
using HttpProcessor.Content;
using renren.session;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Utils.datetime;

namespace renren.appointment
{
    internal class AppointController : HttpClientBase
    {
        public AppointController(HttpClient httpClient) : base(httpClient)
        {
        }

        internal void Yuyue(List<Order> orderList)
        {

        }

        public async Task AppointAsync()
        {
            await Task.Factory.StartNew(() => Appoint());
        }

        public void Appoint()
        {
            var url = "https://www.medic.ren/PM-server/mobserviceOrder/addServiceOrder";

            try
            {
                var content = new AppointContent(url);

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
