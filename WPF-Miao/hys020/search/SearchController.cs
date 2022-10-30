using HttpProcessor.Client;
using HttpProcessor.Container;
using HttpProcessor.ExceptionManager;
using HttpProcessor.HtmlAnalysis;
using HttpProcessor.Response;
using hys020.session;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Utils;

namespace hys020.search
{
    internal class SearchController : HttpClientBase
    {
        public SearchController(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task SearchAsync()
        {
            var miaoController = HttpServiceController.GetService<MiaoController>();
            await miaoController.SearchMiaoAsync();
        }
    }
}
