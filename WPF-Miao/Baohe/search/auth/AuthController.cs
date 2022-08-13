using HttpProcessor.Client;
using System.Net.Http;

namespace Baohe.search.auth
{
    internal class AuthController : HttpClientBase
    {
        public AuthController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void GetAuth()
        {
            //Search
        }
    }
}
