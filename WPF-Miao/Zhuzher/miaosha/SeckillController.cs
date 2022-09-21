using HttpProcessor.Client;
using System.Net.Http;

namespace Zhuzher.miaosha
{
    internal class SeckillController : HttpClientBase
    {
        public SeckillController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void Seckill()
        {

        }
    }
}
