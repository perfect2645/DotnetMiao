using HttpProcessor.Client;
using HttpProcessor.Container;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WPF_Miao.Platform.yunnan.model;
using WPF_Miao.Platform.yunnan.viewModel;

namespace WPF_Miao.Platform.yunnan
{
    internal class AppointmentController : HttpClientBase
    {
        public AppointmentController(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task AppointmentAsync(ISessionItem sessionItem)
        {
            var secureHeader = HttpServiceController.ServiceProvider.GetService<SecureHeader>();
            var content = HttpServiceController.ServiceProvider.GetService<AppointmentContent>();

            await secureHeader.BuildHeader();
            content.BuildRequest(secureHeader, sessionItem);

            var response = await Client.SendAsync(content.HttpRequestMessage);

            ParseAppointmentResult(response);
        }

        private void ParseAppointmentResult(HttpResponseMessage response)
        {
            var resString = response.Content.ReadAsStringAsync();
        }
    }
}
