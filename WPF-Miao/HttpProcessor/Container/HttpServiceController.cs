using HttpProcessor.Client;
using Microsoft.Extensions.DependencyInjection;

namespace HttpProcessor.Container
{
    public static class HttpServiceController
    {
        public static ServiceCollection ServiceCollection
        {
            get
            {
                return _serviceCollection;
            }
        }

        private static readonly ServiceCollection _serviceCollection;

        public static ServiceProvider ServiceProvider
        {
            get
            {
                return _serviceProvider;
            }
        }

        private static ServiceProvider? _serviceProvider = null;

        static HttpServiceController()
        {
            _serviceCollection = new ServiceCollection();
        }

        public static void AddTransientService<TClient, THandler>() 
            where TClient : HttpClientBase
            where THandler : HttpHandler, new()
        {
            _serviceCollection
                .AddHttpClient<TClient>()
                .AddHttpMessageHandler(s =>
                {
                    return new THandler();
                });

            _serviceProvider = _serviceCollection.BuildServiceProvider();
        }

        public static void AddTransientService<TClient>()
            where TClient : HttpClientBase
        {
            _serviceCollection
                .AddHttpClient<TClient>();

            _serviceProvider = _serviceCollection.BuildServiceProvider();
        }

        public static void AddClient<TClient, THandler>()
            where TClient : HttpClientBase
            where THandler : HttpHandler, new()
        {
            _serviceCollection
                .AddHttpClient<TClient, TClient>("test", (httpclient) =>
                {
                    var myChient = (TClient)Activator.CreateInstance(typeof(TClient), new object?[] { httpclient })!;
                    return myChient;
                })
                .AddHttpMessageHandler(s =>
                {
                    return new THandler();
                });

            _serviceProvider = _serviceCollection.BuildServiceProvider();
        }

        public static void AddSingletonService<TClient, THandler>()
            where TClient : HttpClientBase
            where THandler : HttpHandler
        {
            _serviceCollection.AddSingleton<THandler>()
                .AddHttpClient<TClient>()
                .AddHttpMessageHandler<THandler>();

            _serviceCollection.BuildServiceProvider();
        }

        public static TClient GetService<TClient>() where TClient : HttpClientBase
        {
            var service = _serviceProvider?.GetRequiredService<TClient>();
            return service;
        }
    }
}
