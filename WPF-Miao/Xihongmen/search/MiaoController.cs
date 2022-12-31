using HttpProcessor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Xihongmen.search
{
    internal class MiaoController : HttpClientBase
    {
        public MiaoController(HttpClient httpClient) : base(httpClient)
        {
        }
    }
}
