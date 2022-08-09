using HttpProcessor.Content;
using HttpProcessor.Request;
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

        #region Search

        public virtual async Task<HttpDicResponse> SearchAsync(HttpMessageContent content)
        {
            try
            {
                var response = await Client.SearchAsync(content);
                return response;
            } 
            catch (Exception ex)
            {
                GLog.Logger.Error("Search Failed", ex);
                return null;
            }
        }

        public virtual async Task<HttpDicResponse> SearchAsync(string url)
        {
            try
            {
                var response = await Client.SearchAsync(url);
                return response;
            }
            catch (Exception ex)
            {
                GLog.Logger.Error("Search Failed", ex);
                return null;
            }
        }

        public virtual void Search(HttpMessageContent content, Action<HttpDicResponse> callback)
        {
            try
            {
                Client.Search(content, callback);
            }
            catch (Exception ex)
            {
                GLog.Logger.Error("Search Failed", ex);
            }
        }

        #endregion Search

        #region Post String

        public virtual async Task<HttpDicResponse> PostStringAsync(HttpStringContent content)
        {
            try
            {
                var response = await Client.PostStringAsync(content);
                return response;
            }
            catch (Exception ex)
            {
                GLog.Logger.Error("Search Failed", ex);
                return null;
            }
        }

        #endregion Post String
    }
}
