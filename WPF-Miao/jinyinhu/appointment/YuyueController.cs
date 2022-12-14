using Base.viewmodel.status;
using HttpProcessor.Client;
using HttpProcessor.Content;
using jinyinhu.session;
using System;
using System.Net.Http;
using Utils.stringBuilder;
using Utils.timerUtil;

namespace jinyinhu.appointment
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
                if (response?.JsonBody?.RootElement == null)
                {
                    Log($"预约失败：{response?.Message}");
                    return;
                }

                var root = response.JsonBody.RootElement;
                var code = root.GetProperty("code").NotNullString();
                var message = root.GetProperty("message").NotNullString();
                if (!"200".Equals(code)|| !"预约成功".Equals(message))
                {
                    MainSession.PrintLogEvent.Publish(this, $"code={code}, message={message}");
                    return;
                }

                MainSession.SetStatus(MiaoProgress.AppointEnd);
                var data = root.GetProperty("data").NotNullString();
                MainSession.PrintLogEvent.Publish(this, $"预约结果:code={code}, message={message}, data ={data}");
                MainSession.PrintLogEvent.Publish(null, $"{content.Order.ToLogString()}");
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"预约异常{ex.Message}");
            }
        }
    }
}
