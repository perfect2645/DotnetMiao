using HttpProcessor.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpProcessor.Container
{
    public class HttpServiceController
    {
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
