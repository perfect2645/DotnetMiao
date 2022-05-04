namespace HttpProcessor.Client
{
    public abstract class HttpClientBase : IHttpClient
    {
        private readonly HttpClient _httpClient;

        public HttpClientBase(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public abstract void Search();
    }
}
