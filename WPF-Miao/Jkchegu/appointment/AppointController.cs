using HttpProcessor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Jkchegu.appointment
{
    internal class AppointController : HttpClientBase
    {
        public AppointController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void Appoint()
        {

        }
    }
}
