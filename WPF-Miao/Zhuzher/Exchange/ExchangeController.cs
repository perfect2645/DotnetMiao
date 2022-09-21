using HttpProcessor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Zhuzher.Exchange
{
    internal class ExchangeController : HttpClientBase
    {
        public ExchangeController(HttpClient httpClient) : base(httpClient)
        {
        }



    }
}
