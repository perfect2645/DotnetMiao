using Baohe.constants;
using Baohe.session;
using Base.viewModel;
using HttpProcessor.Client;
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
    }
}
