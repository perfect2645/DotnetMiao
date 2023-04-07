using HttpProcessor.Client;
using HttpProcessor.Content;
using Jkchegu.session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Jkchegu.appointment
{
    internal class CancelController : HttpClientBase
    {
        public CancelController(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task CancelAsync(Order order)
        {
            await Task.Factory.StartNew(() => Cancel(order));
        }

        public void Cancel(Order order)
        {
            try
            {
                var url = "http://app.whkfqws.com/wx-mobile/Vaccination/cancelReservation.do";

                var content = new CancelContent(url, order);

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
                }

                if (string.IsNullOrEmpty(result) || result.Contains("存在已有"))
                {
                    JkSession.PrintLogEvent.Publish(this, $"result:{result}，预约成功请查询确认");
                }

                JkSession.PrintLogEvent.Publish(this, $"result:{result}");
            }
            catch (Exception ex)
            {
                JkSession.PrintLogEvent.Publish(this, $"预约异常{ex.Message}");
            }

        }
    }
}
