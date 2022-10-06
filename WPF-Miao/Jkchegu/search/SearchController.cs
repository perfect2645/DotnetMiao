using HttpProcessor.Client;
using HttpProcessor.Content;
using Jkchegu.session;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Jkchegu.search
{
    internal class SearchController : HttpClientBase
    {
        public SearchController(HttpClient httpClient) : base(httpClient)
        {
        }

        public void SearchAsync()
        {
            Task.Factory.StartNew(() => Search());
        }

        private void Search()
        {
            var url = "https://chaos.4009515151.com/market/api/activity/good/exchange";

            var content = new SearchContent(url);


            content.BuildDefaultHeaders(Client);

            HttpDicResponse response = PostStringAsync(content, ContentType.Json).Result;
            if (response == null)
            {
                JkSession.PrintLogEvent.Publish(this, $"登录过期了");
            }
            var code = response.Body.FirstOrDefault(x => x.Key == "code").Value?.ToString();
            var msg = response.Body.FirstOrDefault(x => x.Key == "message").Value?.ToString();

            //PrintLog(user, good, msg);
        }
    }
}
