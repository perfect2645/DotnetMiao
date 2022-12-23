using Base.viewmodel.status;
using HttpProcessor.Client;
using HttpProcessor.Content;
using Jingyang.session;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Utils.stringBuilder;
using Utils.timerUtil;

namespace Jingyang.appointment
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
            //YuyueAsync(order);
            IntervalOnTime.StartIntervalOntime(() => YuyueAsync(order));
        }

        public void YuyueAsync(Order order)
        {
            if (MainSession.GetStatus() == MiaoProgress.AppointEnd)
            {
                IntervalOnTime.StopInterval();
                return;
            }
            MainSession.PrintLogEvent.Publish(null, $"开始预约：{order.ToLogString()}");
            var content = new YuyueContent(order);
            Yuyue(content);
        }

        internal void Yuyue(YuyueContent content)
        {
            try
            {
                if (MainSession.GetStatus() == MiaoProgress.AppointEnd)
                {
                    IntervalOnTime.StopInterval();
                    return;
                }
                
                content.BuildDefaultHeaders(Client);
                HttpDicResponse response = PostStringAsync(content, ContentType.Json).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"Appoint failed - {response?.Message},请检查参数");
                    return;
                }

                var success = response.JsonBody.RootElement.GetProperty("success").NotNullString().ToBool();
                var message = response.JsonBody.RootElement.GetProperty("message").NotNullString();
                if (!success)
                {
                    MainSession.PrintLogEvent.Publish(this, $"预约失败:{message}");
                    return;
                }
                MainSession.SetStatus(MiaoProgress.AppointEnd);
                MainSession.PrintLogEvent.Publish(this, $"预约结果:{message}");
                MainSession.PrintLogEvent.Publish(null, $"{content.Order.ToLogString()}");
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"预约异常{ex.Message}");
            }
        }
    }
}
