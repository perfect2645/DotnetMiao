using Base.viewmodel.status;
using HttpProcessor.Client;
using HttpProcessor.Content;
using hys020.session;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Utils.timerUtil;

namespace hys020.appointment.Yuyue
{
    public class YuyueController : HttpClientBase
    {
        public IntervalOnTime IntervalOnTime { get; set; }
        internal YuyueContent Content { get; set; }

        public string Key { get; set; }

        public YuyueController(HttpClient httpClient) : base(httpClient)
        {
            IntervalOnTime = new IntervalOnTime(YuyueAsync, Key, 100);
        }

        public void StartInterval()
        {
            IntervalOnTime.StartIntervalOntime();
        }

        public void YuyueAsync()
        {
            if (MainSession.GetStatus() == MiaoProgress.AppointEnd)
            {
                IntervalOnTime.StopInterval();
                return;
            }
            Task.Factory.StartNew(() => Yuyue());
        }

        private void Yuyue()
        {
            try
            {
                Content.BuildDefaultHeaders(Client);

                HttpDicResponse response = PostStringAsync(Content, ContentType.String).Result;
                if (response?.Body == null)
                {
                    MainSession.PrintLogEvent.Publish(this, $"Appoint failed - {response?.Message},请检查参数");
                    return;
                }

                var result = response.JsonBody.RootElement.GetProperty("successMessage").ToString();
                if (string.IsNullOrEmpty(result))
                {
                    Log(result);
                    return;
                }
                MainSession.SetStatus(MiaoProgress.AppointEnd);
                MainSession.PrintLogEvent.Publish(this, $"预约申请提交成功 result:{result}");
                IntervalOnTime.StopInterval();
            }
            catch (Exception ex)
            {
                MainSession.PrintLogEvent.Publish(this, $"预约异常{ex.Message}");
            }
        }
    }
}
