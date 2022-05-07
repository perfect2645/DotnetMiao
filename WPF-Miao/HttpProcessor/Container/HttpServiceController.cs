using HttpProcessor.Client;
using Microsoft.Extensions.DependencyInjection;

namespace HttpProcessor.Container
{
    public static class HttpServiceController
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

        public static void AddTransientService<TClient, THandler>() 
            where TClient : HttpClientBase
            where THandler : HttpHandler
        {
            _serviceController.AddTransient<THandler>()
                .AddHttpClient<TClient>()
                .AddHttpMessageHandler<THandler>();
        }

        public static void AddAddSingletonService<TClient, THandler>()
            where TClient : HttpClientBase
            where THandler : HttpHandler
        {
            _serviceController.AddSingleton<THandler>()
                .AddHttpClient<TClient>()
                .AddHttpMessageHandler<THandler>();
        }
    }
}
