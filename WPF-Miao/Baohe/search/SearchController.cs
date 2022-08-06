using HttpProcessor.Client;
using System.Net.Http;

namespace Baohe.search
{
    internal class SearchController : HttpClientBase
    {
        public SearchController(HttpClient httpClient) : base(httpClient)
        {

            
        }
    }
}
