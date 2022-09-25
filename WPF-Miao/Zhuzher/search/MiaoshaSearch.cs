using HttpProcessor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Zhuzher.search
{
    internal class MiaoshaSearch : HttpClientBase
    {
        public MiaoshaSearch(HttpClient httpClient) : base(httpClient)
        {
        }
    }
}
