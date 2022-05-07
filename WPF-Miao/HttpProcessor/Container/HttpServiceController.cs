using HttpProcessor.Client;
using Microsoft.Extensions.DependencyInjection;

namespace HttpProcessor.Container
{
    public class HttpServiceController
    {
        public static ServiceCollection Instance
        {
            get
            {
                return _serviceController;
            }
        }

        private static readonly ServiceCollection _serviceController;

        static HttpServiceController()
        {
            _serviceController = new ServiceCollection();
        }

        public void AddService<TClient, THandler>(TClient client, THandler handler) 
            where TClient : HttpClientBase
            where THandler : HttpHandler
        {
            _serviceController.AddTransient<THandler>()
                .AddHttpClient<TClient>()
                .AddHttpMessageHandler<THandler>();
        }
    }
}
