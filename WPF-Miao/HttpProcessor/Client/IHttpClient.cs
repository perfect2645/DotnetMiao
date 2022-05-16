namespace HttpProcessor.Client
{
    public interface IHttpClient
    {
        Task<HttpDicResponse> SearchAsync(HttpClientContentBase content);
        void Search(HttpClientContentBase content, Action<HttpDicResponse> callback);
    }
}