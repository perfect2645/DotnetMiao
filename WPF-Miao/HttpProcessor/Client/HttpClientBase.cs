namespace HttpProcessor.Client
{
    public class HttpClientBase : IHttpClient
    {
        protected readonly HttpClient Client;

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
