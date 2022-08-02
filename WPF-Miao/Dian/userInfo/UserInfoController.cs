using HttpProcessor.Client;
using System.Net.Http;

namespace Dian.userInfo
{
    internal class UserInfoController : HttpClientBase
    {
        public UserInfoController(HttpClient httpClient) : base(httpClient)
        {
        }
    }
}
