using HttpProcessor.Client;
using System.Net.Http;

namespace Zhuzher.Exchange
{
    internal class ExchangeController : HttpClientBase
    {
        public ExchangeController(HttpClient httpClient) : base(httpClient)
        {
        }



    }
}
