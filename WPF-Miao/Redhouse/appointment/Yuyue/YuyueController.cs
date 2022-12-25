using Base.viewmodel.status;
using HttpProcessor.Client;
using HttpProcessor.Container;
using HttpProcessor.Content;
using Redhouse.session;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Utils.timerUtil;

namespace Redhouse.appointment.Yuyue
{
    public class YuyueController : HttpClientBase
    {
        public IntervalOnTime IntervalOnTime { get; set; }
        internal YuyueContent Content { get; set; }

        public string Key { get; set; }
        public YuyueController(HttpClient httpClient) : base(httpClient)
        {
            IntervalOnTime = new IntervalOnTime(Key, 300);
        }

        public void StartInterval(Order order)
        {
            IntervalOnTime.StartIntervalOntime(() => YuyueAsync(order));
        }

        public void YuyueAsync(Order order)
        {
            if (MainSession.GetStatus() == MiaoProgress.AppointEnd)
            {
                IntervalOnTime.StopInterval();
                return;
            }
            var preview = HttpServiceController.GetService<PreviewAppointController>();
            preview.PreviewAppoint(order);
            Log($"开始预约：{order.ToLogString()}");
            var content = new YuyueContent(order);
            Yuyue(content);
        }

        private void Yuyue(YuyueContent content)
        {
            try
            {
                if (MainSession.GetStatus() == MiaoProgress.AppointEnd)
                {
                    IntervalOnTime.StopInterval();
                    return;
                }
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
                MainSession.PrintLogEvent.Publish(null, $"预约申请提交成功 {content.Order.ToLogString()}");
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"预约异常{ex.Message}");
            }
        }
    }
}
