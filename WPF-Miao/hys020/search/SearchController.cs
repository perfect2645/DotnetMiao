using Base.viewmodel.status;
using HttpProcessor.Client;
using HttpProcessor.Container;
using System.Net.Http;
using System.Threading.Tasks;
using Utils.timerUtil;

namespace hys020.search
{
    internal class SearchController : HttpClientBase
    {
        public SearchController(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task SearchAsync()
        {
            await Task.Factory.StartNew(() => Search());
        }

        private void Search()
        {

        }
    }
}
