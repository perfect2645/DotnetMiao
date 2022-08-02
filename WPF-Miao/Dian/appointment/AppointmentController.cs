using Base.viewModel;
using HttpProcessor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Dian.appointment
{
    internal class AppointmentController : HttpClientBase
    {
        public AppointmentController(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task AppointmentAsync(ISessionItem sessionItem)
        {

            //var response = await Client.SendAsync(content.HttpRequestMessage);

            //ParseAppointmentResult(response);
        }

        private void ParseAppointmentResult(HttpResponseMessage response)
        {
            var resString = response.Content.ReadAsStringAsync();
        }
    }
}
