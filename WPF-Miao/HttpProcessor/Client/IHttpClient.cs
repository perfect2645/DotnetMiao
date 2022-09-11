using HttpProcessor.Content;

namespace HttpProcessor.Client
{
    public interface IHttpClient
    {
        Task<HttpDicResponse> SearchAsync(HttpMessageContent content);
        void Search(HttpMessageContent content, Action<HttpDicResponse> callback);
    }
}