using HttpProcessor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WPF_Miao.Platform.yunnan
{
    internal class AppointmentController : HttpClientBase
    {
        public AppointmentController(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task AppointmentAsync()
        {
            //var content = new AppointmentContent("h5-health.tengmed.com/api/gateway/VaccinationServer/immunizationAppointment");
            //var stringContent = content.GetJsonContent();
            //stringContent.Headers.Add("Content-Type", "application/json");
            //var response = await Client.PostAsync(requestMessage);


        }
    }
}
