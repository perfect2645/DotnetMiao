using Base.viewmodel.status;
using HttpProcessor.Client;
using HttpProcessor.Container;
using HttpProcessor.Content;
using hys020.session;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
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
            var result = YuyueAsync(orderList).Result;
        }

        private async Task<int> YuyueAsync(List<Order> orderList)
        {
            for (var i = 1; i < 10; i++)
            {
                if (MainSession.MiaoStatus.MiaoProgress == MiaoProgress.AppointEnd)
                {
                    MainSession.PrintLogEvent.Publish(this, $"预约结束，退出循环");
                    return 1;
                }
                foreach (var order in orderList)
                {
                    if (MainSession.MiaoStatus.MiaoProgress == MiaoProgress.AppointEnd)
                    {
                        MainSession.PrintLogEvent.Publish(this, $"预约结束，退出循环");
                        return 1;
                    }
                    var preview = HttpServiceController.GetService<PreviewAppointController>();
                    preview.PreviewAppoint(order);
                    MainSession.PrintLogEvent.Publish(this, order.ToLogString());
                    var content = new AppointContent(order);
                    await AppointAsync(content);
                    Thread.Sleep(100);
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
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"Appoint failed - {response?.Message},请检查参数");
                }

                var result = response.JsonBody.RootElement.GetProperty("successMessage").ToString();
                if (string.IsNullOrEmpty(result))
                {
                    return;
                }
                MainSession.SetStatus(MiaoProgress.AppointEnd);
                MainSession.PrintLogEvent.Publish(this, $"预约申请提交成功 result:{result}");
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"预约异常{ex.Message}");
            }
        }
    }
}
