using HttpProcessor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Tianhe.cancel
{
    internal class CancelController : HttpClientBase
    {
        public CancelController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void CancelAsync()
        {
            Task.Factory.StartNew(() =>
            {
                Cancel();
            });
        }

        private void Cancel()
        {

        }
    }
}
