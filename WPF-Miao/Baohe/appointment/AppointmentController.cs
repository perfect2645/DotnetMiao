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
using System.Threading.Tasks;

namespace Baohe.appointment
{
    internal class AppointmentController : HttpClientBase
    {
        public AppointmentController(HttpClient httpClient) : base(httpClient)
        {
        }


        public Task AppointmentAsync(ISessionItem sessionItem)
        {
            return Task.Factory.StartNew(() => Appointment(sessionItem));
        }

        public void Appointment(ISessionItem sessionItem)
        {
            var url = "https://appoint.yihu.com/appoint/do/registerInfo/register";
            var content = new AppointmentContent(url, sessionItem);
            content.AddHeader("Cookie", sessionItem.Cookie);
            content.AddHeader("Referer", content.BuildReferer());

            content.BuildDefaultHeaders(Client);

            HttpDicResponse response = PostStringAsync(content, ContentType.RichEncode).Result;
            var code = response.Body.FirstOrDefault(x => x.Key == Constant.StatusCode).Value?.ToString();
            if (code == null || code != "10000")
            {
                throw new HttpException($"{Constant.ProjectName}:GetDoctorList-{url} - {response.Body["Message"]}", "bad response");
            }
        }

        private void ParseAppointmentResult(HttpResponseMessage response)
        {
            var resString = response.Content.ReadAsStringAsync();
        }
    }
}
