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
    }
}
