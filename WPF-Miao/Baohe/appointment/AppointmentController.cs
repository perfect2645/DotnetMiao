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
using Utils.datetime;
using Utils.stringBuilder;

namespace Baohe.appointment
{
    internal class AppointmentController : HttpClientBase
    {
        public AppointmentController(HttpClient httpClient) : base(httpClient)
        {
            OrderEvent.Subscribe(AppointmentAsync);
        }

        public void AppointmentAsync(object? sender, OrderArgs e)
        {
            var key = sender.NotNullString();
            
            Task.Factory.StartNew(() => 
            {
                try
                {
                    Appointment(sender, e);
                }
                catch (HttpException ex)
                {
                    MainSession.PrintLogEvent.Publish(this, ex.Message);
                }
                catch (Exception ex)
                {
                    MainSession.PrintLogEvent.Publish(this, ex.StackTrace ?? ex.Message);
                }
            });
        }

        private void ParseAppointmentResult(HttpResponseMessage response)
        {
            var resString = response.Content.ReadAsStringAsync();
        }

        private void Appointment(object? sender, OrderArgs e)
        {
            var key = sender.NotNullString();
            MainSession.PrintLogEvent.Publish(this, $"开始预约：key : {key}");

            var content = e.Content;
            content.AddHeader("Cookie", MainSession.Cookie);
            content.AddHeader("Referer", content.BuildReferer());

            content.BuildDefaultHeaders(Client);

            HttpDicResponse response = PostStringAsync(content, ContentType.Encode).Result;
            var code = response?.Body?.FirstOrDefault(x => x.Key == Constant.StatusCode).Value?.ToString();
            if (code == null || code != "10000")
            {
                throw new HttpException($"{Constant.ProjectName}:Appointment-{content.RequestUrl} - {response.Body["Message"]}", Constant.Appointment);
            }

            var orderId = response.JsonBody.RootElement.GetProperty("Orderid").NotNullString();
            if (!string.IsNullOrEmpty(orderId))
            {
                MainSession.PrintLogEvent.Publish(this, $"预约成功：orderid : {orderId}");
            }
        }
    }
}
