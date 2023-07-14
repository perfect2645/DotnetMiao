using Dalian.login;
using HttpProcessor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Utils;
using Utils.stringBuilder;

namespace Dalian.common
{
    internal class DalianController : HttpClientBase
    {
        public DalianController(HttpClient httpClient) : base(httpClient)
        {
        }
    }
}
