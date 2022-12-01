using Baohe.session;
using HttpProcessor.Client;
using System.Net.Http;
using System.Threading.Tasks;
using Utils;
using Utils.stringBuilder;

namespace Baohe.search.cookie
{
    internal class CookieController : HttpClientBase
    {
        public CookieController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void GetCookieAdvance(string cookie)
        {
            if (string.IsNullOrWhiteSpace(cookie))
            {
                return;
            }

            var dic = cookie.CookieToDic();

            BaoheSession.PlatformSesstion.AddOrUpdate(dic);
        }
    }
}
