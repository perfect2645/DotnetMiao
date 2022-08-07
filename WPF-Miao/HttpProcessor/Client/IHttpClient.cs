namespace HttpProcessor.Client
{
    public interface IHttpClient
    {
        Task<HttpDicResponse> SearchAsync(HttpMessageContentBase content);
        void Search(HttpMessageContentBase content, Action<HttpDicResponse> callback);
    }
}