using Logging;

namespace HttpProcessor.Client
{
    public class HttpClientBase : IHttpClient
    {
        protected readonly HttpClient Client;

        private static readonly string _uuid;

        static HttpClientBase()
        {
            _uuid = Guid.NewGuid().ToString();
            GLog.Logger.Info($"HttpClient Created uuid={_uuid}");
        }

        public HttpClientBase(HttpClient httpClient)
        {
            Client = httpClient;
        }

        public virtual async Task<string> Search(HttpClientContentBase content)
        {
            var response = await Client.GetAsync(content.RequestUrl);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
