using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Miao.Platform.DianYiTong.Services
{
    public static class HttpServiceController
    {
        public static ServiceCollection ServiceCollection { get; private set; }

        static HttpServiceController()
        {
            ServiceCollection = new ServiceCollection();
        }

        public static void AddService<T>(T httpClient) where T : HttpClient
        {
            ServiceCollection.AddHttpClient<T>();
        }
    }
}
