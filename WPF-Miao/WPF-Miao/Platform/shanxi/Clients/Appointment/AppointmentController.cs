using HttpProcessor.Client;
using System.Net.Http;
using System.Threading.Tasks;

namespace WPF_Miao.Platform.shanxi.Clients.Appointment
{
    internal class AppointmentController : HttpClientBase
    {
        public AppointmentController(HttpClient httpClient) : base(httpClient)
        {
        }


        public async Task AppointmentAsync()
        {
            var content = new AppointmentContent("h5-health.tengmed.com/api/gateway/VaccinationServer/immunizationAppointment");
            var stringContent = content.GetJsonContent();
            stringContent.Headers.Add("Content-Type", "application/json");
            var response = await Client.PostAsync(requestMessage);
        }
    }
}
