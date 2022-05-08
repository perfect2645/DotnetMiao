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

        private static readonly ServiceProvider _serviceProvider;

        static HttpServiceController()
        {
            _serviceController = new ServiceCollection();
        }

        public static void AddTransientService<TClient, THandler>() 
            where TClient : HttpClientBase
            where THandler : HttpHandler
        {
            _serviceController.AddTransient<TClient>()
                .AddHttpClient<TClient>()
                .AddHttpMessageHandler<THandler>();

            _serviceController.BuildServiceProvider();
        }

        public static void AddSingletonService<TClient, THandler>()
            where TClient : HttpClientBase
            where THandler : HttpHandler
        {
            _serviceController.AddSingleton<THandler>()
                .AddHttpClient<TClient>()
                .AddHttpMessageHandler<THandler>();

            _serviceController.BuildServiceProvider();
        }

        public static TClient GetService<TClient>() where TClient : HttpClientBase
        {
            var service = _serviceProvider.GetRequiredService<TClient>();
            return service;
        }
    }
}
