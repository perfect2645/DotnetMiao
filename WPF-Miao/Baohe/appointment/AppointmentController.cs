using Baohe.constants;
using Baohe.session;
using Base.viewModel;
using HttpProcessor.Client;
using HttpProcessor.Content;
using HttpProcessor.ExceptionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Utils.stringBuilder;

namespace Baohe.appointment
{
    internal class AppointmentController : HttpClientBase
    {
        public AppointmentController(HttpClient httpClient) : base(httpClient)
        {
            OrderEvent.Subscribe(Appointment);
        }

        public Task AppointmentAsync(ISessionItem sessionItem)
        {
            return Task.Factory.StartNew(() => Appointment(sessionItem));
        }

        public void Appointment(ISessionItem sessionItem)
        {
            
/*            var content = new AppointmentContent();
            content.AddHeader("Cookie", sessionItem.Cookie);
            content.AddHeader("Referer", sessionItem.Referer);

            content.BuildDefaultHeaders(Client);

            HttpDicResponse userInfo = PostStringAsync(content).Result;
            var userid = userInfo.Body.FirstOrDefault(x => x.Key == Constant.accountSn).Value?.ToString();
            if (userid == null || userid == "0")
            {
                throw new HttpException($"{Constant.ProjectName}:{content.RequestUrl} has issue", Constant.accountSn);
            }
            sessionItem.Key = userid;
            BaoheSession.AddMiaoSession(userInfo.Body);
            sessionItem.PrintLogEvent.Publish(this, userInfo.Body);*/
        }

        private void ParseAppointmentResult(HttpResponseMessage response)
        {
            var resString = response.Content.ReadAsStringAsync();
        }

        private void Appointment(object? sender, OrderArgs e)
        {
            var key = sender.NotNullString();
            BaoheSession.PrintLogEvent.Publish(this, $"开始预约：key : {key}");

            var content = e.Content;
            content.AddHeader("Cookie", BaoheSession.Cookie);
            content.AddHeader("Referer", content.BuildReferer());

            content.BuildDefaultHeaders(Client);

            HttpDicResponse response = PostStringAsync(content, ContentType.Encode).Result;
            var code = response.Body.FirstOrDefault(x => x.Key == Constant.StatusCode).Value?.ToString();
            if (code == null || code != "10000")
            {
                throw new HttpException($"{Constant.ProjectName}:Appointment-{content.RequestUrl} - {response.Body["Message"]}", Constant.GetNumbers);
            }

            var result = response.JsonBody.RootElement.GetProperty("Result");
            if (result.ValueKind == JsonValueKind.Null)
            {
                throw new HttpException($"{Constant.ProjectName}:Appointment-{content.RequestUrl} - Result is empty", "empty result");
            }
        }
    }
}
