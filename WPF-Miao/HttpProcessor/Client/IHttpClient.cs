namespace HttpProcessor.Client
{
    public interface IHttpClient
    {
        Task<string> Search(HttpClientContentBase content);
    }
}