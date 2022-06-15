using HttpProcessor.Client;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WPF_Miao.Platform.yunnan
{
    internal class AppointmentHandler : HttpHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return base.SendAsync(request, cancellationToken);
        }
    }
}
