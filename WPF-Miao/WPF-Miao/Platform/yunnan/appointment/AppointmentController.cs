using HttpProcessor.Client;
using HttpProcessor.Container;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Threading.Tasks;
using WPF_Miao.Platform.yunnan.getTimestamp;
using WPF_Miao.Platform.yunnan.model;

namespace WPF_Miao.Platform.yunnan
{
    internal class AppointmentController : HttpClientBase
    {

        public AppointmentController(HttpClient httpClient) : base(httpClient)
        {
        }



        public async Task AppointmentAsync()
        {
            var secureHeader = HttpServiceController.ServiceProvider.GetService<SecureHeader>();
            var content = HttpServiceController.ServiceProvider.GetService<AppointmentContent>();
            content.HttpRequestMessage.Method = HttpMethod.Post;

            await secureHeader.BuildHeader();
            content.AddHeaders(secureHeader.SecurityHeaderDic);

            var response = await Client.SendAsync(content.HttpRequestMessage);
        }
    }
}
